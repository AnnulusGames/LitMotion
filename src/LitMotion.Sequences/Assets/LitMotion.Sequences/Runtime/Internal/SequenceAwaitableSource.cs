#if UNITY_2023_1_OR_NEWER
using System.Threading;
using UnityEngine;

namespace LitMotion.Sequences
{
    internal sealed class SequenceAwaitableSource : SequenceConfiguredSourceBase
    {
        public static SequenceAwaitableSource CompletedSource
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
        static SequenceAwaitableSource completedSource;

        public static SequenceAwaitableSource CanceledSource
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
        static SequenceAwaitableSource canceledSource;

        readonly AwaitableCompletionSource core = new();

        public Awaitable Awaitable => core.Awaitable;

        public static SequenceAwaitableSource Create(MotionSequence sequence, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                OnCanceledTokenReceived(sequence, cancelBehaviour);
                return CanceledSource;
            }

            var source = new SequenceAwaitableSource();
            source.Initialize(sequence, cancelBehaviour, cancellationToken);

            return source;
        }

        protected override void SetTaskCanceled(CancellationToken cancellationToken)
        {
            core.SetCanceled();
            RestoreOriginalCallback();
        }

        protected override void SetTaskCompleted()
        {
            core.SetResult();
            RestoreOriginalCallback();
        }
    }
}

#endif
