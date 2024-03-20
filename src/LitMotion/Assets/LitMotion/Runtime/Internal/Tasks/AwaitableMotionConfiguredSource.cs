#if UNITY_2023_1_OR_NEWER
using System.Threading;
using UnityEngine;

namespace LitMotion
{
    // TODO: use object pool

    internal sealed class AwaitableMotionConfiguredSource : MotionConfiguredSourceBase
    {
        public static AwaitableMotionConfiguredSource CompletedSource
        {
            get
            {
                if (completedSource == null)
                {
                    completedSource = new();
                }
                completedSource.core.Reset();
                completedSource.core.SetResult();
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
                }
                canceledSource.core.Reset();
                canceledSource.core.SetCanceled();
                return canceledSource;
            }
        }
        static AwaitableMotionConfiguredSource canceledSource;

        readonly AwaitableCompletionSource core = new();

        public Awaitable Awaitable => core.Awaitable;

        AwaitableMotionConfiguredSource() : base() { }

        public static AwaitableMotionConfiguredSource Create(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                OnCanceledTokenReceived(motionHandle, cancelBehaviour);
                return CanceledSource;
            }

            var result = new AwaitableMotionConfiguredSource();
            result.Initialize(motionHandle, cancelBehaviour, cancellationToken);
            return result;
        }

        protected override void SetTaskCanceled(CancellationToken cancellationToken)
        {
            core.SetCanceled();
        }

        protected override void SetTaskCompleted()
        {
            core.SetResult();
        }
    }
}

#endif
