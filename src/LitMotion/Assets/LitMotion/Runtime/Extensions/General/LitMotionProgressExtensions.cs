using System;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Progress.
    /// </summary>
    public static class LitMotionProgressExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to IProgress
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="progress">Target object that implements IProgress</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToProgress<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, IProgress<TValue> progress)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Error.IsNull(progress);
            return builder.Bind(progress, static (x, progress) => progress.Report(x));
        }
    }
}