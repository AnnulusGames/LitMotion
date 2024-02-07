using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponentBase<TValue, TObject> : SequenceComponent
        where TValue : unmanaged
        where TObject : Object
    {
        public ExposedReference<TObject> target;

        public TValue startValue;
        public TValue endValue;
        public float duration = 1f;

        public Ease ease;
        public float delay;
        public DelayType delayType;
        public bool skipValuesDuringDelay = true;
        public int loops = 1;
        public LoopType loopType;
        public bool useRelativeValue;

        public TObject Target { get; private set; }

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            Target = target.Resolve(exposedPropertyTable);
        }
    }
}
