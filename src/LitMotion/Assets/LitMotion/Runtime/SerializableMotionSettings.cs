using System;
using UnityEngine;

namespace LitMotion
{
    /// <summary>
    /// Serializable(editable from the Inspector) MotionSettings.
    /// </summary>
    /// <typeparam name="TValue">The type of value to animate</typeparam>
    /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
    [Serializable]
    public sealed record SerializableMotionSettings<TValue, TOptions> : MotionSettings<TValue, TOptions>, ISerializationCallbackReceiver
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        enum SchedulerType : byte
        {
            Default,
            Initialization,
            InitializationIgnoreTimeScale,
            InitializationRealtime,
            EarlyUpdate,
            EarlyUpdateIgnoreTimeScale,
            EarlyUpdateRealtime,
            FixedUpdate,
            PreUpdate,
            PreUpdateIgnoreTimeScale,
            PreUpdateRealtime,
            Update,
            UpdateIgnoreTimeScale,
            UpdateRealtime,
            PreLateUpdate,
            PreLateUpdateIgnoreTimeScale,
            PreLateUpdateRealtime,
            PostLateUpdate,
            PostLateUpdateIgnoreTimeScale,
            PostLateUpdateRealtime,
            TimeUpdate,
            TimeUpdateIgnoreTimeScale,
            TimeUpdateRealtime,
            Manual,
        }

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
        [SerializeField] SchedulerType scheduler;

        public void OnBeforeSerialize()
        {
            startValue = StartValue;
            endValue = EndValue;
            duration = Duration;
            options = Options;
            ease = Ease;
            customEaseCurve = CustomEaseCurve;
            delay = Delay;
            delayType = DelayType;
            loops = Loops;
            loopType = LoopType;
            cancelOnError = CancelOnError;
            skipValuesDuringDelay = SkipValuesDuringDelay;
            bindOnSchedule = BindOnSchedule;
            SetScheduler(Scheduler);
        }

        public void OnAfterDeserialize()
        {
            StartValue = startValue;
            EndValue = endValue;
            Duration = duration;
            Options = options;
            Ease = ease;
            CustomEaseCurve = customEaseCurve;
            Delay = delay;
            DelayType = delayType;
            Loops = loops;
            LoopType = loopType;
            CancelOnError = cancelOnError;
            SkipValuesDuringDelay = skipValuesDuringDelay;
            BindOnSchedule = bindOnSchedule;
            Scheduler = scheduler switch
            {
                SchedulerType.Initialization => MotionScheduler.Initialization,
                SchedulerType.InitializationIgnoreTimeScale => MotionScheduler.InitializationIgnoreTimeScale,
                SchedulerType.InitializationRealtime => MotionScheduler.InitializationRealtime,
                SchedulerType.EarlyUpdate => MotionScheduler.EarlyUpdate,
                SchedulerType.EarlyUpdateIgnoreTimeScale => MotionScheduler.EarlyUpdateIgnoreTimeScale,
                SchedulerType.EarlyUpdateRealtime => MotionScheduler.EarlyUpdateRealtime,
                SchedulerType.FixedUpdate => MotionScheduler.FixedUpdate,
                SchedulerType.PreUpdate => MotionScheduler.PreUpdate,
                SchedulerType.PreUpdateIgnoreTimeScale => MotionScheduler.PreUpdateIgnoreTimeScale,
                SchedulerType.PreUpdateRealtime => MotionScheduler.PreUpdateRealtime,
                SchedulerType.Update => MotionScheduler.Update,
                SchedulerType.UpdateIgnoreTimeScale => MotionScheduler.UpdateIgnoreTimeScale,
                SchedulerType.UpdateRealtime => MotionScheduler.UpdateRealtime,
                SchedulerType.PreLateUpdate => MotionScheduler.PreLateUpdate,
                SchedulerType.PreLateUpdateIgnoreTimeScale => MotionScheduler.PreLateUpdateIgnoreTimeScale,
                SchedulerType.PreLateUpdateRealtime => MotionScheduler.PreLateUpdateRealtime,
                SchedulerType.PostLateUpdate => MotionScheduler.PostLateUpdate,
                SchedulerType.PostLateUpdateIgnoreTimeScale => MotionScheduler.PostLateUpdateIgnoreTimeScale,
                SchedulerType.PostLateUpdateRealtime => MotionScheduler.PostLateUpdateRealtime,
                SchedulerType.TimeUpdate => MotionScheduler.TimeUpdate,
                SchedulerType.TimeUpdateIgnoreTimeScale => MotionScheduler.TimeUpdateIgnoreTimeScale,
                SchedulerType.TimeUpdateRealtime => MotionScheduler.TimeUpdateRealtime,
                SchedulerType.Manual => MotionScheduler.Manual,
                _ => null,
            };
        }

        void SetScheduler(IMotionScheduler value)
        {
            if (value == null) scheduler = SchedulerType.Default;
            else if (value == MotionScheduler.Update) scheduler = SchedulerType.Update;
            else if (value == MotionScheduler.UpdateIgnoreTimeScale) scheduler = SchedulerType.UpdateIgnoreTimeScale;
            else if (value == MotionScheduler.UpdateRealtime) scheduler = SchedulerType.UpdateRealtime;
            else if (value == MotionScheduler.FixedUpdate) scheduler = SchedulerType.FixedUpdate;
            else if (value == MotionScheduler.Manual) scheduler = SchedulerType.Manual;
            else if (value == MotionScheduler.Initialization) scheduler = SchedulerType.Initialization;
            else if (value == MotionScheduler.InitializationIgnoreTimeScale) scheduler = SchedulerType.InitializationIgnoreTimeScale;
            else if (value == MotionScheduler.InitializationRealtime) scheduler = SchedulerType.InitializationRealtime;
            else if (value == MotionScheduler.EarlyUpdate) scheduler = SchedulerType.EarlyUpdate;
            else if (value == MotionScheduler.EarlyUpdateIgnoreTimeScale) scheduler = SchedulerType.EarlyUpdateIgnoreTimeScale;
            else if (value == MotionScheduler.EarlyUpdateRealtime) scheduler = SchedulerType.EarlyUpdateRealtime;
            else if (value == MotionScheduler.PreUpdate) scheduler = SchedulerType.PreUpdate;
            else if (value == MotionScheduler.PreUpdateIgnoreTimeScale) scheduler = SchedulerType.PreUpdateIgnoreTimeScale;
            else if (value == MotionScheduler.PreUpdateRealtime) scheduler = SchedulerType.PreUpdateRealtime;
            else if (value == MotionScheduler.PreLateUpdate) scheduler = SchedulerType.PreLateUpdate;
            else if (value == MotionScheduler.PreLateUpdateIgnoreTimeScale) scheduler = SchedulerType.PreLateUpdateIgnoreTimeScale;
            else if (value == MotionScheduler.PreLateUpdateRealtime) scheduler = SchedulerType.PreLateUpdateRealtime;
            else if (value == MotionScheduler.PostLateUpdate) scheduler = SchedulerType.PostLateUpdate;
            else if (value == MotionScheduler.PostLateUpdateIgnoreTimeScale) scheduler = SchedulerType.PostLateUpdateIgnoreTimeScale;
            else if (value == MotionScheduler.PostLateUpdateRealtime) scheduler = SchedulerType.PostLateUpdateRealtime;
            else if (value == MotionScheduler.TimeUpdate) scheduler = SchedulerType.TimeUpdate;
            else if (value == MotionScheduler.TimeUpdateIgnoreTimeScale) scheduler = SchedulerType.TimeUpdateIgnoreTimeScale;
            else if (value == MotionScheduler.TimeUpdateRealtime) scheduler = SchedulerType.TimeUpdateRealtime;
            // else throw new ArgumentOutOfRangeException("SerializableMotionSettings does not support custom scheduler");
        }
    }
}