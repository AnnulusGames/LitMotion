using System;
using System.Threading;

namespace LitMotion.Sequences
{
    internal abstract class SequenceConfiguredSourceBase
    {
        public SequenceConfiguredSourceBase()
        {
            onCancelCallbackDelegate = OnCancelCallbackDelegate;
            onCompleteCallbackDelegate = OnCompleteCallbackDelegate;
        }

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionSequence motionSequence;
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

        protected static void OnCanceledTokenReceived(MotionSequence sequence, CancelBehaviour cancelBehaviour)
        {
            switch (cancelBehaviour)
            {
                case CancelBehaviour.Cancel:
                case CancelBehaviour.CancelAndCancelAwait:
                    sequence.Cancel();
                    break;
                case CancelBehaviour.Complete:
                case CancelBehaviour.CompleteAndCancelAwait:
                    sequence.Complete();
                    break;
            }
        }

        protected void Initialize(MotionSequence motionSequence, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken)
        {
            this.motionSequence = motionSequence;
            this.cancelBehaviour = cancelBehaviour;
            this.cancellationToken = cancellationToken;

            motionSequence.OnCanceled += onCancelCallbackDelegate;
            motionSequence.OnCompleted += onCompleteCallbackDelegate;

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
                    var source = (SequenceConfiguredSourceBase)x;
                    switch (source.cancelBehaviour)
                    {
                        default:
                        case CancelBehaviour.CancelAndCancelAwait:
                            source.canceled = true;
                            source.motionSequence.Cancel();
                            break;
                        case CancelBehaviour.Cancel:
                            source.motionSequence.Cancel();
                            break;
                        case CancelBehaviour.CompleteAndCancelAwait:
                            source.canceled = true;
                            source.motionSequence.Complete();
                            break;
                        case CancelBehaviour.Complete:
                            source.motionSequence.Complete();
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
            motionSequence = default;
            cancelBehaviour = default;
            cancellationToken = default;
            originalCompleteAction = default;
            originalCancelAction = default;
            canceled = default;
        }

        protected void RestoreOriginalCallback()
        {
            motionSequence.OnCanceled -= onCancelCallbackDelegate;
            motionSequence.OnCompleted -= onCompleteCallbackDelegate;
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