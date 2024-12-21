namespace LitMotion
{
    /// <summary>
    /// Provides the function to schedule the execution of a motion.
    /// </summary>
    public interface IMotionScheduler
    {
        /// <summary>
        /// Schedule the motion.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">Motion builder</param>
        /// <returns>Motion handle</returns>
        MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>;
    }

    /// <summary>
    /// Type of time used to play the motion
    /// </summary>
    public enum MotionTimeKind : byte
    {
        Time = 0,
        UnscaledTime = 1,
        Realtime = 2
    }
}