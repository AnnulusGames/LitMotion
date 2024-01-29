using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace LitMotion
{
#if UNITY_2023_1_OR_NEWER

    internal sealed class AwaitableMotionConfiguredSource
    {
        [StructLayout(LayoutKind.Auto)]
        public struct Pool
        {
            static readonly int MaxPoolSize = int.MaxValue;

            int gate;
            int size;
            AwaitableMotionConfiguredSource root;

            public readonly int Size => size;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryPop(out AwaitableMotionConfiguredSource result)
            {
                if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
                {
                    var v = root;
                    if (v != null)
                    {
                        ref var nextNode = ref v.NextNode;
                        root = nextNode;
                        nextNode = null;
                        size--;
                        result = v;
                        Volatile.Write(ref gate, 0);
                        return true;
                    }

                    Volatile.Write(ref gate, 0);
                }
                result = default;
                return false;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool TryPush(AwaitableMotionConfiguredSource item)
            {
                if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
                {
                    if (size < MaxPoolSize)
                    {
                        item.NextNode = root;
                        root = item;
                        size++;
                        Volatile.Write(ref gate, 0);
                        return true;
                    }
                    else
                    {
                        Volatile.Write(ref gate, 0);
                    }
                }
                return false;
            }
        }

        static Pool pool;
        AwaitableMotionConfiguredSource nextNode;
        public ref AwaitableMotionConfiguredSource NextNode => ref nextNode;

        public static AwaitableMotionConfiguredSource CompletedSource
        {
            get
            {
                if (completedSource == null)
                {
                    completedSource = new();
                    completedSource.core.SetResult();
                }
                return completedSource;
            }
        }
        static AwaitableMotionConfiguredSource completedSource;

        public static AwaitableMotionConfiguredSource CanceledSource
        {
            get
            {
                if (canceledSource == null)
                {
                    canceledSource = new();
                    canceledSource.core.SetCanceled();
                }
                return canceledSource;
            }
        }
        static AwaitableMotionConfiguredSource canceledSource;

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionHandle motionHandle;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        Action originalCompleteAction;
        Action originalCancelAction;
        readonly AwaitableCompletionSource core = new();

        public Awaitable Awaitable => core.Awaitable;

        AwaitableMotionConfiguredSource()
        {
            onCancelCallbackDelegate = new(OnCancelCallbackDelegate);
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static AwaitableMotionConfiguredSource Create(MotionHandle motionHandle, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                motionHandle.Cancel();
                return CanceledSource;
            }

            if (!pool.TryPop(out var result))
            {
                result = new AwaitableMotionConfiguredSource();
            }

            result.motionHandle = motionHandle;
            result.cancellationToken = cancellationToken;

            var callbacks = MotionStorageManager.GetMotionCallbacks(motionHandle);
            result.originalCancelAction = callbacks.OnCancelAction;
            result.originalCompleteAction = callbacks.OnCompleteAction;
            callbacks.OnCancelAction = result.onCancelCallbackDelegate;
            callbacks.OnCompleteAction = result.onCompleteCallbackDelegate;
            MotionStorageManager.SetMotionCallbacks(motionHandle, callbacks);

            if (result.originalCancelAction == result.onCancelCallbackDelegate)
            {
                result.originalCancelAction = null;
            }
            if (result.originalCompleteAction == result.onCompleteCallbackDelegate)
            {
                result.originalCompleteAction = null;
            }

            if (cancellationToken.CanBeCanceled)
            {
                result.cancellationRegistration = cancellationToken.Register(static x =>
                {
                    var source = (AwaitableMotionConfiguredSource)x;
                    var motionHandle = source.motionHandle;
                    if (motionHandle.IsActive())
                    {
                        motionHandle.Cancel();
                    }
                    else
                    {
                        source.core.SetCanceled();
                        source.TryReturn();
                    }
                }, result);
            }

            return result;
        }

        void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                core.SetCanceled();
            }
            else
            {
                originalCompleteAction?.Invoke();
                core.SetResult();
            }

            TryReturn();
        }

        void OnCancelCallbackDelegate()
        {
            originalCancelAction?.Invoke();
            core.SetCanceled();
            TryReturn();
        }

        bool TryReturn()
        {
            core.Reset();
            cancellationRegistration.Dispose();

            RestoreOriginalCallback();

            motionHandle = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
            return pool.TryPush(this);
        }

        void RestoreOriginalCallback()
        {
            if (!motionHandle.IsActive()) return;
            var callbacks = MotionStorageManager.GetMotionCallbacks(motionHandle);
            callbacks.OnCancelAction = originalCancelAction;
            callbacks.OnCompleteAction = originalCompleteAction;
            MotionStorageManager.SetMotionCallbacks(motionHandle, callbacks);
        }
    }

#endif
}
