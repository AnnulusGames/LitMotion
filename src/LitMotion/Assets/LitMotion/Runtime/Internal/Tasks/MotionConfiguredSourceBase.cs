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
        bool linkToMotionCancellation;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        Action originalCompleteAction;
        Action originalCancelAction;

        bool CancelAwait => cancelBehaviour is CancelBehaviour.CancelAndCancelAwait or CancelBehaviour.CompleteAndCancelAwait or CancelBehaviour.CancelAwait;

        protected abstract void SetTaskCanceled(CancellationToken cancellationToken);
        protected abstract void SetTaskCompleted();

        protected void OnCancelCallbackDelegate()
        {
            originalCancelAction?.Invoke();

            if ((cancellationToken.IsCancellationRequested && CancelAwait) || linkToMotionCancellation)
            {
                SetTaskCanceled(cancellationToken);
            }
            else
            {
                SetTaskCompleted();
            }
        }

        protected void OnCompleteCallbackDelegate()
        {
            originalCompleteAction?.Invoke();

            if (cancellationToken.IsCancellationRequested && CancelAwait)
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

        protected void Initialize(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, bool linkToMotionCancellation, CancellationToken cancellationToken)
        {
            this.motionHandle = motionHandle;
            this.cancelBehaviour = cancelBehaviour;
            this.linkToMotionCancellation = linkToMotionCancellation;
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
                    if (!source.motionHandle.IsActive()) return;

                    source.RestoreOriginalCallback();

                    switch (source.cancelBehaviour)
                    {
                        default:
                            break;
                        case CancelBehaviour.CancelAndCancelAwait:
                        case CancelBehaviour.Cancel:
                            source.motionHandle.Cancel();
                            break;
                        case CancelBehaviour.CompleteAndCancelAwait:
                        case CancelBehaviour.Complete:
                            source.motionHandle.Complete();
                            break;
                    }

                    if (source.CancelAwait)
                    {
                        source.SetTaskCanceled(source.cancellationToken);
                    }
                    else
                    {
                        source.SetTaskCompleted();
                    }
                }, this);
            }
        }

        protected void ResetFields()
        {
            motionHandle = default;
            cancelBehaviour = default;
            linkToMotionCancellation = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
        }

        protected void RestoreOriginalCallback()
        {
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