using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using LitMotion.Collections;

namespace LitMotion.Sequences
{
    internal sealed class SequenceValueTaskSource : SequenceConfiguredSourceBase, ILinkedPoolNode<SequenceValueTaskSource>, IValueTaskSource
    {
        static LinkedPool<SequenceValueTaskSource> pool;
        public ref SequenceValueTaskSource NextNode => ref nextNode;
        SequenceValueTaskSource nextNode;

        ManualResetValueTaskSourceCore<object> core;

        static SequenceValueTaskSource FromCanceled(out short token)
        {
            if (canceledSource == null)
            {
                canceledSource = new();
            }

            canceledSource.core.Reset();
            canceledSource.core.SetException(new OperationCanceledException());

            token = canceledSource.Version;
            return canceledSource;
        }
        static SequenceValueTaskSource canceledSource;

        public static IValueTaskSource Create(MotionSequence sequence, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                OnCanceledTokenReceived(sequence, cancelBehaviour);
                return FromCanceled(out token);
            }

            if (!pool.TryPop(out var source))
            {
                source = new SequenceValueTaskSource();
            }
            source.Initialize(sequence, cancelBehaviour, cancellationToken);

            token = source.core.Version;
            return source;
        }

        public short Version => core.Version;

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

            DisposeRegistration();
            RestoreOriginalCallback();
            ResetFields();

            return pool.TryPush(this);
        }

        protected override void SetTaskCanceled(CancellationToken cancellationToken)
        {
            core.SetException(new OperationCanceledException());
        }

        protected override void SetTaskCompleted()
        {
            core.SetResult(null);
        }
    }
}