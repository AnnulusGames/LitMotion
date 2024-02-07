using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponentBase<TValue, TObject> : SequenceComponent
        where TValue : unmanaged
        where TObject : Object
    {
        public ExposedReference<TObject> target;

        [Header("Moiton Settings")]
        public TValue startValue;
        public TValue endValue;
        public MotionMode motionMode;
        public float duration = 1f;
        public Ease ease;

        [Header("Delay Settings")]
        public float delay;
        public DelayType delayType;
        public bool skipValuesDuringDelay = true;

        [Header("Loop Settings")]
        public int loops = 1;
        public LoopType loopType;

        public TObject Target { get; private set; }

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            Target = target.Resolve(exposedPropertyTable);
        }
    }
}
