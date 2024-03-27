#if UNITY_2023_1_OR_NEWER
using System.Threading;
using UnityEngine;
using LitMotion.Collections;

namespace LitMotion.Sequences
{
    internal sealed class SequenceAwaitableSource : SequenceConfiguredSourceBase, ILinkedPoolNode<SequenceAwaitableSource>
    {
        static LinkedPool<SequenceAwaitableSource> pool;
        public ref SequenceAwaitableSource NextNode => ref nextNode;
        SequenceAwaitableSource nextNode;

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
                sequence.Cancel();
                return CanceledSource;
            }

            if (!pool.TryPop(out var source))
            {
                source = new SequenceAwaitableSource();
            }
            source.Initialize(sequence, cancelBehaviour, cancellationToken);

            return source;
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
