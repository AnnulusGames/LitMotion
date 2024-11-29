using UnityEngine;

namespace LitMotion
{
    /// <summary>
    /// An object that holds motion settings.
    /// </summary>
    /// <typeparam name="TValue">The type of value to animate</typeparam>
    /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
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
    }
}