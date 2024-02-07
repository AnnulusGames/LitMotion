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

        protected void ConfigureMotionBuilder<T, TOptions, TAdapter>(ref MotionBuilder<T, TOptions, TAdapter> motionBuilder)
            where T : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<T, TOptions>
        {
            motionBuilder.WithEase(ease)
                .WithDelay(delay, delayType, skipValuesDuringDelay)
                .WithLoops(loops, loopType);
        }
    }
}
