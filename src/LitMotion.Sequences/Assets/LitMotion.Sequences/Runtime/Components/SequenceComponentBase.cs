using System;
using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponentBase<TValue, TObject> : SequenceComponent
        where TValue : unmanaged
        where TObject : UnityEngine.Object
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

        public override void ResetComponent()
        {
            base.ResetComponent();
            startValue = default;
            endValue = default;
            motionMode = default;
            duration = 1f;
            ease = default;
            delay = default;
            delayType = default;
            skipValuesDuringDelay = true;
            loops = 1;
            loopType = default;
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

        protected bool TryGetInitialValue(ISequencePropertyTable propertyTable, out TValue result)
        {
            return propertyTable.TryGetInitialValue((target.Resolve(propertyTable), GetType()), out result);
        }

        protected void SetInitialValue(ISequencePropertyTable propertyTable, TValue value)
        {
            propertyTable.SetInitialValue((target.Resolve(propertyTable), GetType()), value);
        }
    }
}
