#if LITMOTION_SUPPORT_UNITASK
using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.Assertions;

namespace LitMotion
{
    public static class LitMotionUniTaskExtensions
    {
        public static UniTask.Awaiter GetAwaiter(this MotionHandle handle)
        {
            return ToUniTask(handle).GetAwaiter();
        }

        public static UniTask ToUniTask(this MotionHandle handle, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(MotionConfiguredSource.Create(handle, cancellationToken, out var token), token);
        }

        public static MotionHandle BindToAsyncReactiveProperty<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, AsyncReactiveProperty<TValue> reactiveProperty)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Assert.IsNotNull(reactiveProperty);
            return builder.BindWithState(reactiveProperty, (x, target) =>
            {
                target.Value = x;
            });
        }
    }

    internal sealed class MotionConfiguredSource : IUniTaskSource, ITaskPoolNode<MotionConfiguredSource>
    {
        static TaskPool<MotionConfiguredSource> pool;
        MotionConfiguredSource nextNode;
        public ref MotionConfiguredSource NextNode => ref nextNode;

        static MotionConfiguredSource()
        {
            TaskPool.RegisterSizeGetter(typeof(MotionConfiguredSource), () => pool.Size);
        }

        readonly Action onCompleteCallbackDelegate;

        MotionHandle motionHandle;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;
        bool canceled;

        Action originalCompleteAction;
        UniTaskCompletionSourceCore<AsyncUnit> core;

        MotionConfiguredSource()
        {
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static IUniTaskSource Create(MotionHandle motionHandle, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                motionHandle.Cancel();
                return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
            }

            if (!pool.TryPop(out var result))
            {
                result = new MotionConfiguredSource();
            }

            result.motionHandle = motionHandle;
            result.cancellationToken = cancellationToken;
            result.canceled = false;

            var callbacks = MotionStorageManager.GetMotionCallbacks(motionHandle);
            result.originalCompleteAction = callbacks.OnCompleteAction;
            callbacks.OnCompleteAction = result.onCompleteCallbackDelegate;
            callbacks.UniTaskConfiguredSource = result;
            MotionStorageManager.SetMotionCallbacks(motionHandle, callbacks);

            if (result.originalCompleteAction == result.onCompleteCallbackDelegate)
            {
                result.originalCompleteAction = null;
            }

            if (cancellationToken.CanBeCanceled)
            {
                result.cancellationRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(x =>
                {
                    var source = (MotionConfiguredSource)x;
                    source.motionHandle.Cancel();
                    source.core.TrySetCanceled(source.cancellationToken);
                }, result);
            }

            TaskTracker.TrackActiveTask(result, 3);

            token = result.core.Version;
            return result;
        }

        public void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                canceled = true;
            }
            if (canceled)
            {
                core.TrySetCanceled(cancellationToken);
            }
            else
            {
                originalCompleteAction?.Invoke();
                core.TrySetResult(AsyncUnit.Default);
            }
        }

        // for MotionHandle.Cancel()
        public void OnMotionCanceled()
        {
            core.TrySetCanceled(cancellationToken);
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

        public UniTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public UniTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            cancellationRegistration.Dispose();

            RestoreOriginalCallback();

            motionHandle = default;
            cancellationToken = default;
            originalCompleteAction = default;
            return pool.TryPush(this);
        }

        void RestoreOriginalCallback()
        {
            if (!motionHandle.IsActive()) return;
            var callbacks = MotionStorageManager.GetMotionCallbacks(motionHandle);
            callbacks.OnCompleteAction = originalCompleteAction;
            MotionStorageManager.SetMotionCallbacks(motionHandle, callbacks);
        }
    }
}
#endif