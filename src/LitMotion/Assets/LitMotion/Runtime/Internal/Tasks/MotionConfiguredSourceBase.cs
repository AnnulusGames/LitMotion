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
        MotionCanceledBehavior cancelBehavior;
        bool cancelAwaitOnMotionCanceled;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        Action originalCompleteAction;
        Action originalCancelAction;

        protected abstract void SetTaskCanceled(CancellationToken cancellationToken);
        protected abstract void SetTaskCompleted();

        protected void OnCancelCallbackDelegate()
        {
            originalCancelAction?.Invoke();

            if (cancellationToken.IsCancellationRequested || cancelAwaitOnMotionCanceled)
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

            if (cancellationToken.IsCancellationRequested)
            {
                SetTaskCanceled(cancellationToken);
            }
            else
            {
                SetTaskCompleted();
            }
        }

        protected static void OnCanceledTokenReceived(MotionHandle motionHandle, MotionCanceledBehavior cancelBehavior)
        {
            switch (cancelBehavior)
            {
                case MotionCanceledBehavior.Cancel:
                    motionHandle.Cancel();
                    break;
                case MotionCanceledBehavior.Complete:
                    motionHandle.Complete();
                    break;
            }
        }

        protected void Initialize(MotionHandle motionHandle, MotionCanceledBehavior cancelBehavior, bool cancelAwaitOnMotionCanceled, CancellationToken cancellationToken)
        {
            this.motionHandle = motionHandle;
            this.cancelBehavior = cancelBehavior;
            this.cancelAwaitOnMotionCanceled = cancelAwaitOnMotionCanceled;
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

                    source.RestoreOriginalCallback(false);

                    switch (source.cancelBehavior)
                    {
                        default:
                            break;
                        case MotionCanceledBehavior.Cancel:
                            source.motionHandle.Cancel();
                            break;
                        case MotionCanceledBehavior.Complete:
                            source.motionHandle.Complete();
                            break;
                    }

                    source.SetTaskCanceled(source.cancellationToken);
                }, this);
            }
        }

        protected void ResetFields()
        {
            motionHandle = default;
            cancelBehavior = default;
            cancelAwaitOnMotionCanceled = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
        }

        protected void RestoreOriginalCallback(bool checkIsActive = true)
        {
            if (checkIsActive && !motionHandle.IsActive()) return;

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