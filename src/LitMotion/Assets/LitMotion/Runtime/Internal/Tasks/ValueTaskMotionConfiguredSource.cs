using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using LitMotion.Collections;

namespace LitMotion
{
    internal sealed class ValueTaskMotionConfiguredSource : IValueTaskSource, ILinkedPoolNode<ValueTaskMotionConfiguredSource>
    {
        static LinkedPool<ValueTaskMotionConfiguredSource> pool;

        ValueTaskMotionConfiguredSource nextNode;
        public ref ValueTaskMotionConfiguredSource NextNode => ref nextNode;

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionHandle motionHandle;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        Action originalCompleteAction;
        Action originalCancelAction;
        ManualResetValueTaskSourceCore<object> core;

        static ValueTaskMotionConfiguredSource FromCanceled(out short token)
        {
            if (canceledSource == null)
            {
                canceledSource = new();
                canceledSource.core.SetException(new OperationCanceledException());
            }

            token = canceledSource.Version;
            return canceledSource;
        }
        static ValueTaskMotionConfiguredSource canceledSource;

        ValueTaskMotionConfiguredSource()
        {
            onCancelCallbackDelegate = new(OnCancelCallbackDelegate);
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static IValueTaskSource Create(MotionHandle motionHandle, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                motionHandle.Cancel();
                return FromCanceled(out token);
            }

            if (!pool.TryPop(out var result))
            {
                result = new ValueTaskMotionConfiguredSource();
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
                    var source = (ValueTaskMotionConfiguredSource)x;
                    var motionHandle = source.motionHandle;
                    if (motionHandle.IsActive())
                    {
                        motionHandle.Cancel();
                    }
                    else
                    {
                        source.core.SetException(new OperationCanceledException());
                    }
                }, result);
            }

            token = result.core.Version;
            return result;
        }

        public short Version => core.Version;

        void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                core.SetException(new OperationCanceledException());
            }
            else
            {
                originalCompleteAction?.Invoke();
                core.SetResult(null);
            }
        }

        void OnCancelCallbackDelegate()
        {
            originalCancelAction?.Invoke();
            core.SetException(new OperationCanceledException());
        }

        public void GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        public ValueTaskSourceStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            core.OnCompleted(continuation, state, token, flags);
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
}