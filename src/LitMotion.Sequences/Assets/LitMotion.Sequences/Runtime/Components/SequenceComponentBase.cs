using System;
using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponentBase<TValue, TObject> : SequenceComponent
        where TValue : unmanaged
        where TObject : UnityEngine.Object
    {
        [SerializeField] ExposedReference<TObject> target;

        [Header("Moiton Settings")]
        [SerializeField] TValue startValue;
        [SerializeField] TValue endValue;
        [SerializeField] MotionMode motionMode;
        [SerializeField] float duration = 1f;
        [SerializeField] Ease ease;
        [SerializeField] AnimationCurve easeAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        [Header("Delay Settings")]
        [SerializeField] float delay;
        [SerializeField] DelayType delayType;
        [SerializeField] bool skipValuesDuringDelay = true;

        [Header("Loop Settings")]
        [SerializeField] int loops = 1;
        [SerializeField] LoopType loopType;

        public TValue StartValue => startValue;
        public TValue EndValue => endValue;
        public MotionMode MotionMode => motionMode;
        public float Duration => duration;
        public Ease Ease => ease;
        public float Delay => delay;
        public DelayType DelayType => delayType;
        public bool SkipValuesDuringDelay => skipValuesDuringDelay;
        public int Loops => loops;
        public LoopType LoopType => loopType;

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

        protected virtual void ConfigureMotionBuilder<T, TOptions, TAdapter>(ref MotionBuilder<T, TOptions, TAdapter> motionBuilder)
            where T : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<T, TOptions>
        {
            if (ease == Ease.CustomAnimationCurve)
            {
                motionBuilder.WithEase(easeAnimationCurve);
            }
            else
            {
                motionBuilder.WithEase(ease);
            }
            
            motionBuilder.WithDelay(delay, delayType, skipValuesDuringDelay)
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

        protected TObject ResolveTarget(ISequencePropertyTable propertyTable)
        {
            return target.Resolve(propertyTable);
        }
    }
}
