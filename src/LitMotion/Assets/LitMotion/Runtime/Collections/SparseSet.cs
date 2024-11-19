using System;
using System.Runtime.CompilerServices;

namespace LitMotion.Collections
{
    public readonly struct SparseIndex : IEquatable<SparseIndex>
    {
        public int Index { get; }
        public int Version { get; }

        public SparseIndex(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public override bool Equals(object obj)
        {
            return obj is SparseIndex other && Equals(other);
        }

        public bool Equals(SparseIndex other)
        {
            return Index == other.Index && Version == other.Version;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Version);
        }
    }

    internal sealed class SparseSetSlotAllocator
    {
        public struct Slot : IEquatable<Slot>
        {
            public int Next;
            public int DenseIndex;
            public int Version;

            public readonly bool Equals(Slot other)
            {
                return other.Next == Next && other.DenseIndex == DenseIndex && other.Version == Version;
            }

            public override readonly bool Equals(object obj)
            {
                return obj is Slot entry && Equals(entry);
            }

            public override readonly int GetHashCode()
            {
                return HashCode.Combine(Next, DenseIndex, Version);
            }
        }

        Slot[] slots;
        int freeSlot = -1;

        public int Capacity => slots.Length;

        public SparseSetSlotAllocator(int initialCapacity = 32)
        {
            EnsureCapacity(initialCapacity);
        }

        public void EnsureCapacity(int capacity)
        {
            int prevLength;

            if (slots == null)
            {
                slots = new Slot[capacity];
                prevLength = 0;
            }
            else
            {
                prevLength = slots.Length;
                if (prevLength >= capacity) return;

                var newLength = prevLength;
                while (newLength < capacity)
                {
                    newLength *= 2;
                }

                Array.Resize(ref slots, newLength);
            }

            var span = slots.AsSpan(prevLength);

            for (int i = 0; i < span.Length; i++)
            {
                var index = prevLength + i;
                span[i] = new()
                {
                    Next = index == capacity - 1 ? freeSlot : index + 1,
                    DenseIndex = -1,
                    Version = 1
                };
            }

            freeSlot = prevLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseIndex Alloc(int denseIndex)
        {
            if (freeSlot == -1)
            {
                EnsureCapacity(slots.Length * 2);
            }

            var slotIndex = freeSlot;

            ref var slot = ref slots[slotIndex];
            slot.Next = -1;
            slot.DenseIndex = denseIndex;

            freeSlot = slot.Next;

            return new SparseIndex(slotIndex, slot.Version);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FreeUnchecked(SparseIndex index)
        {
            ref var slot = ref slots[index.Index];

            slot.Next = freeSlot;
            slot.Version++;
            freeSlot = index.Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Slot GetSlotRefUnchecked(int index)
        {
            return ref slots[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = new()
                {
                    Next = i == slots.Length - 1 ? -1 : i + 1,
                    DenseIndex = -1,
                    Version = 1
                };
            }

            freeSlot = 0;
        }
    }

    public sealed class SparseSet<T0, T1, T2>
    {
        readonly SparseSetSlotAllocator allocator;
        
        SparseIndex[] sparseIndexLookup;
        T0[] array0;
        T1[] array1;
        T2[] array2;
        int tail;

        public int Count => tail;

        public SparseSet(int initialCapacity = 32)
        {
            allocator = new(initialCapacity);
            EnsureCapacity(initialCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int minimumCapacity)
        {
            ArrayHelper.EnsureCapacity(ref sparseIndexLookup, minimumCapacity);
            ArrayHelper.EnsureCapacity(ref array0, minimumCapacity);
            ArrayHelper.EnsureCapacity(ref array1, minimumCapacity);
            allocator.EnsureCapacity(minimumCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseIndex Add(T0 item0, T1 item1, T2 item2)
        {
            if (sparseIndexLookup.Length == tail)
            {
                EnsureCapacity(tail * 2);
            }

            var index = allocator.Alloc(tail);
            sparseIndexLookup[tail] = index;
            array0[tail] = item0;
            array1[tail] = item1;
            array2[tail] = item2;
            tail++;

            return index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsActive(SparseIndex index)
        {
            ref var slot = ref allocator.GetSlotRefUnchecked(index.Index);
            return slot.Version == index.Version;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int SparseToDense(SparseIndex sparseIndex)
        {
            return allocator.GetSlotRefUnchecked(sparseIndex.Index).DenseIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseIndex DenseToSparse(int denseIndex)
        {
            return sparseIndexLookup[denseIndex];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveUnchecked(SparseIndex index)
        {
            ref var slot = ref allocator.GetSlotRefUnchecked(index.Index);
            RemoveCore(ref slot);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void RemoveCore(ref SparseSetSlotAllocator.Slot slot)
        {
            tail--;
            var denseIndex = slot.DenseIndex;

            // remove (swap) elements
            array0[denseIndex] = array0[tail];
            array0[tail] = default;
            array1[denseIndex] = array1[tail];
            array1[tail] = default;
            array2[denseIndex] = array2[tail];
            array2[tail] = default;

            // remove (swap) sparse index lookup
            var prevSparseIndex = sparseIndexLookup[denseIndex];
            var currentSparseIndex = sparseIndexLookup[tail];
            sparseIndexLookup[denseIndex] = currentSparseIndex;
            sparseIndexLookup[tail] = default;

            // update and free slot
            if (currentSparseIndex.Version != 0)
            {
                slot.DenseIndex = denseIndex;
                allocator.FreeUnchecked(prevSparseIndex);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T0> GetSpan0()
        {
            return array0.AsSpan(0, tail);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T1> GetSpan1()
        {
            return array1.AsSpan(0, tail);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T2> GetSpan2()
        {
            return array2.AsSpan(0, tail);
        }
    }
}