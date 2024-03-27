using System;
using System.Threading;

namespace LitMotion
{
    internal abstract class MotionConfiguredSourceBase
    {
        public MotionConfiguredSourceBase()
        {
            onCancelCallbackDelegate = OnCancelCallbackDelegate;
            onCompleteCallbackDelegate = OnCompleteCallbackDelegate;
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

        protected abstract void SetTaskCanceled(CancellationToken cancellationToken);
        protected abstract void SetTaskCompleted();

        protected void OnCancelCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                if (cancelBehaviour is CancelBehaviour.CancelAndCancelAwait or CancelBehaviour.CompleteAndCancelAwait or CancelBehaviour.CancelAwait)
                {
                    canceled = true;
                }
            }

            originalCancelAction?.Invoke();
            SetTaskCanceled(cancellationToken);
        }

        protected void OnCompleteCallbackDelegate()
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
                SetTaskCanceled(cancellationToken);
            }
            else
            {
                SetTaskCompleted();
            }
        }

        protected static void OnCanceledTokenReceived(MotionHandle motionHandle, CancelBehaviour cancelBehaviour)
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
        }

        protected void Initialize(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken)
        {
            this.motionHandle = motionHandle;
            this.cancelBehaviour = cancelBehaviour;
            this.cancellationToken = cancellationToken;

            ref var callbackData = ref MotionStorageManager.GetMotionCallbackDataRef(motionHandle);
            originalCancelAction = callbackData.OnCancelAction;
            originalCompleteAction = callbackData.OnCompleteAction;
            callbackData.OnCancelAction = onCancelCallbackDelegate;
            callbackData.OnCompleteAction = onCompleteCallbackDelegate;

            if (originalCancelAction == onCancelCallbackDelegate)
            {
                originalCancelAction = null;
            }
            if (originalCompleteAction == onCompleteCallbackDelegate)
            {
                originalCompleteAction = null;
            }

            if (cancellationToken.CanBeCanceled)
            {
                cancellationRegistration = RegisterWithoutCaptureExecutionContext(cancellationToken, static x =>
                {
                    var source = (MotionConfiguredSourceBase)x;
                    switch (source.cancelBehaviour)
                    {
                        default:
                        case CancelBehaviour.CancelAndCancelAwait:
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
                            source.SetTaskCanceled(source.cancellationToken);
                            break;
                    }
                }, this);
            }
        }

        protected void ResetFields()
        {
            motionHandle = default;
            cancelBehaviour = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
            canceled = default;
        }

        protected void RestoreOriginalCallback()
        {
            if (!motionHandle.IsActive()) return;
            ref var callbackData = ref MotionStorageManager.GetMotionCallbackDataRef(motionHandle);
            callbackData.OnCancelAction = originalCancelAction;
            callbackData.OnCompleteAction = originalCompleteAction;
        }

        protected void DisposeRegistration()
        {
            cancellationRegistration.Dispose();
        }

        static CancellationTokenRegistration RegisterWithoutCaptureExecutionContext(CancellationToken cancellationToken, Action<object> callback, object state)
        {
            bool flag = false;
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
                flag = true;
            }

            try
            {
                return cancellationToken.Register(callback, state, useSynchronizationContext: false);
            }
            finally
            {
                if (flag)
                {
                    ExecutionContext.RestoreFlow();
                }
            }
        }
    }
}