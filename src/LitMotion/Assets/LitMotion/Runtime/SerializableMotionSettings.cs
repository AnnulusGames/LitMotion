using System;
using UnityEngine;

namespace LitMotion
{
    [Serializable]
    public record SerializableMotionSettings<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        [SerializeField] TValue startValue;
        [SerializeField] TValue endValue;
        [SerializeField] float duration;
        [SerializeField] TOptions options;
        [SerializeField] Ease ease;
        [SerializeField] AnimationCurve customEaseCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        [SerializeField] float delay;
        [SerializeField] DelayType delayType;
        [SerializeField] int loops = 1;
        [SerializeField] LoopType loopType;
        [SerializeField] bool cancelOnError;
        [SerializeField] bool skipValuesDuringDelay;
        [SerializeField] bool bindOnSchedule;

        public TValue StartValue
        {
            get => startValue;
            set => startValue = value;
        }

        public TValue EndValue
        {
            get => endValue;
            set => endValue = value;
        }

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public TOptions Options
        {
            get => options;
            set => options = value;
        }

        public Ease Ease
        {
            get => ease;
            set => ease = value;
        }

        public AnimationCurve CustomEaseCurve
        {
            get => customEaseCurve;
            set => customEaseCurve = value;
        }

        public float Delay
        {
            get => delay;
            set => delay = value;
        }

        public DelayType DelayType
        {
            get => delayType;
            set => delayType = value;
        }

        public int Loops
        {
            get => loops;
            set => loops = value;
        }

        public LoopType LoopType
        {
            get => loopType;
            set => loopType = value;
        }

        public bool CancelOnError
        {
            get => cancelOnError;
            set => cancelOnError = value;
        }

        public bool SkipValuesDuringDelay
        {
            get => skipValuesDuringDelay;
            set => skipValuesDuringDelay = value;
        }

        public bool BindOnSchedule
        {
            get => bindOnSchedule;
            set => bindOnSchedule = value;
        }
    }
}