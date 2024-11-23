using UnityEngine;

namespace LitMotion
{
    public record MotionSettings<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        public TValue StartValue { get; set; }
        public TValue EndValue { get; set; }
        public float Duration { get; set; }
        public TOptions Options { get; set; }
        public Ease Ease { get; set; }
        public AnimationCurve CustomEaseCurve { get; set; }
        public float Delay { get; set; }
        public DelayType DelayType { get; set; }
        public int Loops { get; set; } = 1;
        public LoopType LoopType { get; set; }
        public bool CancelOnError { get; set; }
        public bool SkipValuesDuringDelay { get; set; }
        public bool BindOnSchedule { get; set; }
        public IMotionScheduler Scheduler { get; set; }

        public void CopyFrom(MotionSettings<TValue, TOptions> source)
        {
            StartValue = source.StartValue;
            EndValue = source.EndValue;
            Duration = source.Duration;
            Options = source.Options;
            Ease = source.Ease;
            CustomEaseCurve = source.CustomEaseCurve;
            Delay = source.Delay;
            DelayType = source.DelayType;
            Loops = source.Loops;
            LoopType = source.LoopType;
            CancelOnError = source.CancelOnError;
            SkipValuesDuringDelay = source.SkipValuesDuringDelay;
            Scheduler = source.Scheduler;
        }

        public void CopyFrom(SerializableMotionSettings<TValue, TOptions> source)
        {
            StartValue = source.StartValue;
            EndValue = source.EndValue;
            Duration = source.Duration;
            Options = source.Options;
            Ease = source.Ease;
            CustomEaseCurve = source.CustomEaseCurve;
            Delay = source.Delay;
            DelayType = source.DelayType;
            Loops = source.Loops;
            LoopType = source.LoopType;
            CancelOnError = source.CancelOnError;
            SkipValuesDuringDelay = source.SkipValuesDuringDelay;
        }

        internal void Reset()
        {
            StartValue = default;
            EndValue = default;
            Duration = default;
            Options = default;
            Ease = default;
            CustomEaseCurve = default;
            Delay = default;
            DelayType = default;
            Loops = 1;
            LoopType = default;
            CancelOnError = default;
            SkipValuesDuringDelay = default;
            Scheduler = default;
        }
    }
}