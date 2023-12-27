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
        public static readonly IMotionScheduler Update = new UpdateMotionScheduler();

        /// <summary>
        /// Scheduler that updates motion at LateUpdate.
        /// </summary>
        public static readonly IMotionScheduler LateUpdate = new LateUpdateMotionScheduler();

        /// <summary>
        /// Scheduler that updates motion at FixedUpdate.
        /// </summary>
        public static readonly IMotionScheduler FixedUpdate = new FixedUpdateMotionScheduler();

        /// <summary>
        /// Scheduler that updates motion with `ManualMotionDispatcher.Update()`
        /// </summary>
        public static readonly IMotionScheduler Manual = new ManualMotionScheduler();
    }
}