using UnityEngine;

namespace LitMotion.Sequences
{
    public static class MotionSequenceExtensions
    {
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