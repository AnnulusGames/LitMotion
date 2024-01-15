namespace LitMotion
{
    /// <summary>
    /// Schedulers available in Runtime.
    /// </summary>
    public static class MotionScheduler
    {
        /// <summary>
        /// Scheduler that updates motion at Update.
        /// </summary>
        public static readonly IMotionScheduler Update = new PlayerLoopMotionScheduler(UpdateMode.Update, MotionTimeKind.Time);

        /// <summary>
        /// Scheduler that updates motion at LateUpdate.
        /// </summary>
        public static readonly IMotionScheduler LateUpdate = new PlayerLoopMotionScheduler(UpdateMode.LateUpdate, MotionTimeKind.Time);

        /// <summary>
        /// Scheduler that updates motion at FixedUpdate.
        /// </summary>
        public static readonly IMotionScheduler FixedUpdate = new PlayerLoopMotionScheduler(UpdateMode.FixedUpdate, MotionTimeKind.Time);

        /// <summary>
        /// Scheduler that updates motion with `ManualMotionDispatcher.Update()`
        /// </summary>
        public static readonly IMotionScheduler Manual = new ManualMotionScheduler();
    }
}