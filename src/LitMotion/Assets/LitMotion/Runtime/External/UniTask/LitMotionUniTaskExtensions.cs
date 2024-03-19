#if LITMOTION_SUPPORT_UNITASK
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace LitMotion
{
    /// <summary>
    /// Provides extension methods for UniTask integration.
    /// </summary>
    public static class LitMotionUniTaskExtensions
    {
        /// <summary>
        /// Convert motion handle to UniTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static UniTask ToUniTask(this MotionHandle handle, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(MotionConfiguredSource.Create(handle, CancelBehaviour.CancelAndCancelAwait, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Convert motion handle to UniTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancelBahaviour">Behavior when canceling</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static UniTask ToUniTask(this MotionHandle handle, CancelBehaviour cancelBahaviour, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(MotionConfiguredSource.Create(handle, cancelBahaviour, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Create a motion data and bind it to AsyncReactiveProperty.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="progress">Target object that implements IProgress</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAsyncReactiveProperty<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, AsyncReactiveProperty<TValue> reactiveProperty)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Error.IsNull(reactiveProperty);
            return builder.BindWithState(reactiveProperty, static (x, target) =>
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

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionHandle motionHandle;
        CancelBehaviour cancelBehaviour;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;
        bool canceled;

        Action originalCompleteAction;
        Action originalCancelAction;
        UniTaskCompletionSourceCore<AsyncUnit> core;

        MotionConfiguredSource()
        {
            onCancelCallbackDelegate = new(OnCancelCallbackDelegate);
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static IUniTaskSource Create(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                switch (cancelBehaviour)
                {
                    case CancelBehaviour.Cancel:
                    case CancelBehaviour.CancelAndCancelAwait:
                        motionHandle.Cancel();
                        break;
                    case CancelBehaviour.Complete:
                    case CancelBehaviour.CompleteAndCancelAwait:
                        motionHandle.Complete();
                        break;
                }
                return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
            }

            if (!pool.TryPop(out var result))
            {
                result = new MotionConfiguredSource();
            }

            result.motionHandle = motionHandle;
            result.cancelBehaviour = cancelBehaviour;
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
                result.cancellationRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(static x =>
                {
                    var source = (MotionConfiguredSource)x;
                    switch (source.cancelBehaviour)
                    {
                        case CancelBehaviour.CancelAndCancelAwait:
                        default:
                            source.canceled = true;
                            source.motionHandle.Cancel();
                            break;
                        case CancelBehaviour.Cancel:
                            source.motionHandle.Cancel();
                            break;
                        case CancelBehaviour.CompleteAndCancelAwait:
                            source.canceled = true;
                            source.motionHandle.Complete();
                            break;
                        case CancelBehaviour.Complete:
                            source.motionHandle.Complete();
                            break;
                        case CancelBehaviour.CancelAwait:
                            source.RestoreOriginalCallback();
                            source.core.TrySetCanceled(source.cancellationToken);
                            break;
                    }
                }, result);
            }

            TaskTracker.TrackActiveTask(result, 3);

            token = result.core.Version;
            return result;
        }

        void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (cancelBehaviour is CancelBehaviour.CancelAndCancelAwait or CancelBehaviour.CompleteAndCancelAwait or CancelBehaviour.CancelAwait)
                {
                    canceled = true;
                }
            }

            originalCompleteAction?.Invoke();

            if (canceled)
            {
                core.TrySetCanceled(cancellationToken);
            }
            else
            {
                core.TrySetResult(AsyncUnit.Default);
            }
        }

        void OnCancelCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (cancelBehaviour is CancelBehaviour.CancelAndCancelAwait or CancelBehaviour.CompleteAndCancelAwait or CancelBehaviour.CancelAwait)
                {
                    canceled = true;
                }
            }

            originalCancelAction?.Invoke();
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
            cancelBehaviour = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
            canceled = default;

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
#endif