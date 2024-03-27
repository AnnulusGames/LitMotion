using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LitMotion.Sequences
{
    public static class MotionSequenceExtensions
    {
        public static ValueTask ToValueTask(this MotionSequence sequence, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested) return default;
            var source = SequenceValueTaskSource.Create(sequence, CancelBehaviour.CancelAndCancelAwait, cancellationToken, out var token);
            return new ValueTask(source, token);
        }

#if UNITY_2023_1_OR_NEWER
        public static Awaitable ToAwaitable(this MotionSequence sequence, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested) return default;
            var source = SequenceAwaitableSource.Create(sequence, CancelBehaviour.CancelAndCancelAwait, cancellationToken);
            return source.Awaitable;
        }
#endif

#if UNITY_2022_2_OR_NEWER
        public static MotionSequence AddTo(this MotionSequence sequence, MonoBehaviour target)
        {
            target.destroyCancellationToken.Register(state =>
            {
                var sequence = (MotionSequence)state;
                if (sequence.IsActive()) sequence.Cancel();
            }, sequence, false);
            return sequence;
        }
#endif
    }
}