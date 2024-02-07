#if UNITY_2023_1_OR_NEWER
using System;
using System.Threading;
using UnityEngine;

namespace LitMotion.Sequences
{
    internal sealed class SequenceAwaitableSource : IMotionTaskSourcePoolNode<SequenceAwaitableSource>
    {
        static MotionTaskSourcePool<SequenceAwaitableSource> pool;

        SequenceAwaitableSource nextNode;
        public ref SequenceAwaitableSource NextNode => ref nextNode;

        public static SequenceAwaitableSource CompletedSource
        {
            get
            {
                if (completedSource == null)
                {
                    completedSource = new();
                    completedSource.core.SetResult();
                }
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
                    canceledSource.core.SetCanceled();
                }
                return canceledSource;
            }
        }
        static SequenceAwaitableSource canceledSource;

        readonly Action onCancelCallbackDelegate;
        readonly Action onCompleteCallbackDelegate;

        MotionSequence sequence;
        CancellationToken cancellationToken;
        CancellationTokenRegistration cancellationRegistration;

        readonly AwaitableCompletionSource core = new();

        public Awaitable Awaitable => core.Awaitable;

        SequenceAwaitableSource()
        {
            onCancelCallbackDelegate = new(OnCancelCallbackDelegate);
            onCompleteCallbackDelegate = new(OnCompleteCallbackDelegate);
        }

        public static SequenceAwaitableSource Create(MotionSequence sequence, CancellationToken cancellationToken)
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

            source.sequence = sequence;
            source.cancellationToken = cancellationToken;

            sequence.OnCanceled += source.onCancelCallbackDelegate;
            sequence.OnCompleted += source.onCompleteCallbackDelegate;

            if (cancellationToken.CanBeCanceled)
            {
                source.cancellationRegistration = cancellationToken.Register(static x =>
                {
                    var source = (SequenceAwaitableSource)x;
                    var sequence = source.sequence;
                    if (sequence.IsActive())
                    {
                        sequence.Cancel();
                    }
                    else
                    {
                        source.core.SetCanceled();
                        source.TryReturn();
                    }
                }, source);
            }

            return source;
        }

        void OnCompleteCallbackDelegate()
        {
            if (cancellationToken.IsCancellationRequested)
            {
                core.SetCanceled();
            }
            else
            {
                core.SetResult();
            }

            TryReturn();
        }

        void OnCancelCallbackDelegate()
        {
            core.SetCanceled();
            TryReturn();
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

#endif
