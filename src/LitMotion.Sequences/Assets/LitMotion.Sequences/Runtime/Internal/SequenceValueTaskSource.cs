using System;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace LitMotion.Sequences
{
    internal sealed class SequenceValueTaskSource : IValueTaskSource, IMotionTaskSourcePoolNode<SequenceValueTaskSource>
    {
        static MotionTaskSourcePool<SequenceValueTaskSource> pool;

        SequenceValueTaskSource nextNode;
        public ref SequenceValueTaskSource NextNode => ref nextNode;

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionSequence sequence;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        ManualResetValueTaskSourceCore<object> core;

        static SequenceValueTaskSource FromCanceled(out short token)
        {
            if (canceledSource == null)
            {
                canceledSource = new();
                canceledSource.core.SetException(new OperationCanceledException());
            }

            token = canceledSource.Version;
            return canceledSource;
        }
        static SequenceValueTaskSource canceledSource;

        SequenceValueTaskSource()
        {
            onCancelCallbackDelegate = new(OnCancelCallbackDelegate);
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static IValueTaskSource Create(MotionSequence sequence, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                sequence.Cancel();
                return FromCanceled(out token);
            }

            if (!pool.TryPop(out var source))
            {
                source = new SequenceValueTaskSource();
            }

            source.sequence = sequence;
            source.cancellationToken = cancellationToken;

            sequence.OnCanceled += source.onCancelCallbackDelegate;
            sequence.OnCompleted += source.onCompleteCallbackDelegate;

            if (cancellationToken.CanBeCanceled)
            {
                source.cancellationRegistration = cancellationToken.Register(static x =>
                {
                    var source = (SequenceValueTaskSource)x;
                    var sequence = source.sequence;
                    if (sequence.IsActive())
                    {
                        sequence.Cancel();
                    }
                    else
                    {
                        source.core.SetException(new OperationCanceledException());
                    }
                }, source);
            }

            token = source.core.Version;
            return source;
        }

        public short Version => core.Version;

        void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                core.SetException(new OperationCanceledException());
            }
            else
            {
                core.SetResult(null);
            }
        }

        void OnCancelCallbackDelegate()
        {
            core.SetException(new OperationCanceledException());
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
            cancellationRegistration.Dispose();

            sequence.OnCanceled -= onCancelCallbackDelegate;
            sequence.OnCompleted -= onCompleteCallbackDelegate;

            sequence = default;
            cancellationToken = default;

            return pool.TryPush(this);
        }
    }
}