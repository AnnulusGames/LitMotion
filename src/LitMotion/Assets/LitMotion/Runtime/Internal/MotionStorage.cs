using System;
using System.Runtime.CompilerServices;
using LitMotion.Collections;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

// TODO: Constantize the exception message

namespace LitMotion
{
    internal struct StorageEntry : IEquatable<StorageEntry>
    {
        public int? Next;
        public int DenseIndex;
        public int Version;

        public readonly bool Equals(StorageEntry other)
        {
            return other.Next == Next && other.DenseIndex == DenseIndex && other.Version == Version;
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is StorageEntry entry) return Equals(entry);
            return false;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(Next, DenseIndex, Version);
        }
    }

    internal unsafe interface IMotionStorage
    {
        bool IsActive(MotionHandle handle);
        void Cancel(MotionHandle handle);
        void Complete(MotionHandle handle);
        ref MotionDataCore GetDataRef(MotionHandle handle);
        ref MotionCallbackData GetCallbackDataRef(MotionHandle handle);
        void Reset();
    }

    internal sealed class StorageEntryList
    {
        public StorageEntryList(int initialCapacity = 32)
        {
            entries = new StorageEntry[initialCapacity];
            Reset();
        }

        StorageEntry[] entries;
        int? freeEntry;

        public StorageEntry this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => entries[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => entries[index] = value;
        }

        public void EnsureCapacity(int capacity)
        {
            var currentLength = entries.Length;
            if (currentLength >= capacity) return;

            Array.Resize(ref entries, capacity);
            for (int i = currentLength; i < entries.Length; i++)
            {
                entries[i] = new() { Next = i == capacity - 1 ? freeEntry : i + 1, DenseIndex = -1, Version = 1 };
            }
            freeEntry = currentLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public StorageEntry Alloc(int denseIndex, out int entryIndex)
        {
            // Ensure array capacity
            if (freeEntry == null)
            {
                var currentLength = entries.Length;
                EnsureCapacity(entries.Length * 2);
                freeEntry = currentLength;
            }

            // Find free entry
            entryIndex = freeEntry.Value;
            var entry = entries[entryIndex];
            freeEntry = entry.Next;
            entry.Next = null;
            entry.DenseIndex = denseIndex;
            entries[entryIndex] = entry;

            return entry;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free(int index)
        {
            var entry = entries[index];
            entry.Next = freeEntry;
            entry.Version++;
            entries[index] = entry;
            freeEntry = index;
        }

        public void Reset()
        {
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i] = new() { Next = i == entries.Length - 1 ? null : i + 1, DenseIndex = -1, Version = 1 };
            }
            freeEntry = 0;
        }
    }

    internal sealed class MotionStorage<TValue, TOptions, TAdapter> : IMotionStorage
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        public MotionStorage(int id)
        {
            StorageId = id;
            AllocatorHelper = RewindableAllocatorFactory.CreateAllocator();
        }

        // Entry
        readonly StorageEntryList entries = new(InitialCapacity);

        // Data
        public int?[] toEntryIndex = new int?[InitialCapacity];
        public MotionData<TValue, TOptions>[] dataArray = new MotionData<TValue, TOptions>[InitialCapacity];
        public MotionCallbackData[] callbacksArray = new MotionCallbackData[InitialCapacity];

        // Allocator
        AllocatorHelper<RewindableAllocator> AllocatorHelper;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<MotionData<TValue, TOptions>> GetDataSpan() => dataArray.AsSpan(0, tail);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<MotionCallbackData> GetCallbacksSpan() => callbacksArray.AsSpan(0, tail);

        int tail;

        const int InitialCapacity = 8;

        public int StorageId { get; }
        public int Count => tail;

        public (int EntryIndex, int Version) Append(in MotionData<TValue, TOptions> data, in MotionCallbackData callbacks)
        {
            if (tail == dataArray.Length)
            {
                var newLength = tail * 2;
                Array.Resize(ref toEntryIndex, newLength);
                Array.Resize(ref dataArray, newLength);
                Array.Resize(ref callbacksArray, newLength);
            }

            var entry = entries.Alloc(tail, out var entryIndex);
#if LITMOTION_ENABLE_MOTION_LOG
            UnityEngine.Debug.Log("[Add] Entry:" + entryIndex + " Dense:" + entry.DenseIndex + " Version:" + entry.Version);
#endif

            var prevAnimationCurve = dataArray[tail].Core.AnimationCurve;

            toEntryIndex[tail] = entryIndex;
            dataArray[tail] = data;
            callbacksArray[tail] = callbacks;

            if (data.Core.Ease == Ease.CustomAnimationCurve)
            {
                if (!prevAnimationCurve.IsCreated)
                {
#if LITMOTION_COLLECTIONS_2_0_OR_NEWER
                    prevAnimationCurve = new NativeAnimationCurve(AllocatorHelper.Allocator.Handle);
#else
                    prevAnimationCurve = new UnsafeAnimationCurve(AllocatorHelper.Allocator.Handle);
#endif
                }

                prevAnimationCurve.CopyFrom(data.Core.AnimationCurve);
                dataArray[tail].Core.AnimationCurve = prevAnimationCurve;
            }

            tail++;

            return (entryIndex, entry.Version);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RemoveAt(int denseIndex)
        {
            tail--;

            // swap elements
            dataArray[denseIndex] = dataArray[tail];
            // dataArray[tail] = default;
            callbacksArray[denseIndex] = callbacksArray[tail];
            // callbacksArray[tail] = default;

            // swap entry indexes
            var prevEntryIndex = toEntryIndex[denseIndex];
            var currentEntryIndex = toEntryIndex[denseIndex] = toEntryIndex[tail];
            // toEntryIndex[tail] = default;

            // update entry
            if (currentEntryIndex != null)
            {
                var index = (int)currentEntryIndex;
                var entry = entries[index];
                entry.DenseIndex = denseIndex;
                entries[index] = entry;
            }

            // free entry
            if (prevEntryIndex != null)
            {
                entries.Free((int)prevEntryIndex);
            }

#if LITMOTION_ENABLE_MOTION_LOG
            var v = entries[(int)prevEntryIndex].Version - 1;
            UnityEngine.Debug.Log("[Remove] Entry:" + prevEntryIndex + " Dense:" + denseIndex + " Version:" + v);
#endif
        }

        public void RemoveAll(NativeList<int> indexes)
        {
            var entryIndexes = new NativeArray<int>(indexes.Length, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
            var lastCallbacksSpan = GetCallbacksSpan();
            for (int i = 0; i < entryIndexes.Length; i++)
            {
                entryIndexes[i] = (int)toEntryIndex[indexes[i]];
            }

            for (int i = 0; i < entryIndexes.Length; i++)
            {
                RemoveAt(entries[entryIndexes[i]].DenseIndex);
            }

            // Avoid Memory leak
            lastCallbacksSpan[tail..].Clear();
            entryIndexes.Dispose();
        }

        public void EnsureCapacity(int capacity)
        {
            if (capacity > dataArray.Length)
            {
                Array.Resize(ref toEntryIndex, capacity);
                Array.Resize(ref dataArray, capacity);
                Array.Resize(ref callbacksArray, capacity);
                entries.EnsureCapacity(capacity);
            }
        }

        public void Cancel(MotionHandle handle)
        {
            var entry = entries[handle.Index];
            var denseIndex = entry.DenseIndex;
            if (denseIndex < 0 || denseIndex >= dataArray.Length)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }

            ref var motion = ref GetDataSpan()[denseIndex];
            var version = entry.Version;
            if (version <= 0 || version != handle.Version || motion.Core.Status == MotionStatus.None)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }

            motion.Core.Status = MotionStatus.Canceled;

            ref var callbackData = ref GetCallbacksSpan()[denseIndex];
            try
            {
                callbackData.OnCancelAction?.Invoke();
            }
            catch (Exception ex)
            {
                MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
            }
        }

        public void Complete(MotionHandle handle)
        {
            var entry = entries[handle.Index];
            var denseIndex = entry.DenseIndex;
            if (denseIndex < 0 || denseIndex >= tail)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }

            ref var motion = ref GetDataSpan()[denseIndex];
            var version = entry.Version;
            if (version <= 0 || version != handle.Version || motion.Core.Status == MotionStatus.None)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }

            if (motion.Core.Loops < 0)
            {
                UnityEngine.Debug.LogWarning("[LitMotion] Complete was ignored because it is not possible to complete a motion that loops infinitely. If you want to end the motion, call Cancel() instead.");
                return;
            }

            ref var callbackData = ref GetCallbacksSpan()[denseIndex];
            if (callbackData.IsCallbackRunning)
            {
                throw new InvalidOperationException("Recursion of Complete call was detected.");
            }
            callbackData.IsCallbackRunning = true;

            // To avoid duplication of Complete processing, it is treated as canceled internally.
            motion.Core.Status = MotionStatus.Canceled;

            var endProgress = motion.Core.LoopType switch
            {
                LoopType.Restart => 1f,
                LoopType.Yoyo => motion.Core.Loops % 2 == 0 ? 0f : 1f,
                LoopType.Incremental => motion.Core.Loops,
                _ => 1f
            };

            var easedEndProgress = motion.Core.Ease switch
            {
                Ease.CustomAnimationCurve => motion.Core.AnimationCurve.Evaluate(endProgress),
                _ => EaseUtility.Evaluate(endProgress, motion.Core.Ease),
            };

            var endValue = default(TAdapter).Evaluate(
                ref motion.StartValue,
                ref motion.EndValue,
                ref motion.Options,
                new() { Progress = easedEndProgress }
            );

            try
            {
                callbackData.InvokeUnsafe(endValue);
            }
            catch (Exception ex)
            {
                MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
            }

            try
            {
                callbackData.OnCompleteAction?.Invoke();
            }
            catch (Exception ex)
            {
                MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
            }

            callbackData.IsCallbackRunning = false;
        }

        public bool IsActive(MotionHandle handle)
        {
            var entry = entries[handle.Index];
            var denseIndex = entry.DenseIndex;
            if (denseIndex < 0 || denseIndex >= dataArray.Length) return false;

            var version = entry.Version;
            if (version <= 0 || version != handle.Version) return false;
            var motion = dataArray[denseIndex];
            return motion.Core.Status is MotionStatus.Scheduled or MotionStatus.Delayed or MotionStatus.Playing;
        }

        public ref MotionCallbackData GetCallbackDataRef(MotionHandle handle)
        {
            CheckIndex(handle);
            return ref callbacksArray[entries[handle.Index].DenseIndex];
        }

        public ref MotionDataCore GetDataRef(MotionHandle handle)
        {
            CheckIndex(handle);
            return ref UnsafeUtility.As<MotionData<TValue, TOptions>, MotionDataCore>(ref dataArray[entries[handle.Index].DenseIndex]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void CheckIndex(MotionHandle handle)
        {
            var entry = entries[handle.Index];
            var denseIndex = entry.DenseIndex;
            if (denseIndex < 0 || denseIndex >= dataArray.Length)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }

            var version = entry.Version;
            if (version <= 0 || version != handle.Version || dataArray[denseIndex].Core.Status == MotionStatus.None)
            {
                throw new ArgumentException("Motion has been destroyed or no longer exists.");
            }
        }

        public void Reset()
        {
            entries.Reset();

            toEntryIndex.AsSpan().Clear();
            dataArray.AsSpan().Clear();
            callbacksArray.AsSpan().Clear();
            tail = 0;

            AllocatorHelper.Allocator.Rewind();
        }
    }
}