using System;
using UnityEngine;

namespace LitMotion
{

    /// <summary>
    /// Provides additional Bind methods for MotionBuilder.
    /// </summary>
    public static class MotionBuilderExtensions
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
            return builder.BindWithState(progress, (x, progress) => progress.Report(x));
        }

        /// <summary>
        /// Create a motion data and bind it to Debug.unityLogger
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToUnityLogger<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return builder.Bind(x => Debug.unityLogger.Log(x));
        }

        /// <summary>
        /// Create a motion data and bind it to UnityEngine.ILogger
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToUnityLogger<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, ILogger logger)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return builder.BindWithState(logger, (x, logger) => logger.Log(x));
        }

        /// <summary>
        /// Specifies the rounding format for decimal values when animating integer types.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="roundingMode">Rounding mode</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, IntegerOptions, TAdapter> WithRoundingMode<TValue, TAdapter>(this MotionBuilder<TValue, IntegerOptions, TAdapter> builder, RoundingMode roundingMode)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, IntegerOptions>
        {
            builder.buffer.Options.RoundingMode = roundingMode;
            return builder;
        }
    }
}