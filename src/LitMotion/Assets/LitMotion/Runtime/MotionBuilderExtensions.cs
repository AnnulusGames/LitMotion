using System;
using Unity.Collections;
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
            Error.IsNull(progress);
            return builder.BindWithState(progress, static (x, progress) => progress.Report(x));
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
            return builder.Bind(static x => Debug.unityLogger.Log(x));
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
            Error.IsNull(logger);
            return builder.BindWithState(logger, static (x, logger) => logger.Log(x));
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
            builder.buffer.Data.Options.RoundingMode = roundingMode;
            return builder;
        }

        /// <summary>
        /// Specify the frequency of vibration.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="frequency">Frequency</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, PunchOptions, TAdapter> WithFrequency<TValue, TAdapter>(this MotionBuilder<TValue, PunchOptions, TAdapter> builder, int frequency)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, PunchOptions>
        {
            builder.buffer.Data.Options.Frequency = frequency;
            return builder;
        }

        /// <summary>
        /// Specify the vibration damping ratio.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="dampingRatio">Damping ratio</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, PunchOptions, TAdapter> WithDampingRatio<TValue, TAdapter>(this MotionBuilder<TValue, PunchOptions, TAdapter> builder, float dampingRatio)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, PunchOptions>
        {
            builder.buffer.Data.Options.DampingRatio = dampingRatio;
            return builder;
        }

        /// <summary>
        /// Specify the frequency of vibration.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="frequency">Frequency</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, ShakeOptions, TAdapter> WithFrequency<TValue, TAdapter>(this MotionBuilder<TValue, ShakeOptions, TAdapter> builder, int frequency)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, ShakeOptions>
        {
            builder.buffer.Data.Options.Frequency = frequency;
            return builder;
        }

        /// <summary>
        /// Specify the vibration damping ratio.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="dampingRatio">Damping ratio</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, ShakeOptions, TAdapter> WithDampingRatio<TValue, TAdapter>(this MotionBuilder<TValue, ShakeOptions, TAdapter> builder, float dampingRatio)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, ShakeOptions>
        {
            builder.buffer.Data.Options.DampingRatio = dampingRatio;
            return builder;
        }

        /// <summary>
        /// Specify the random number seed that determines the shake motion value.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="seed">Random number seed</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, ShakeOptions, TAdapter> WithRandomSeed<TValue, TAdapter>(this MotionBuilder<TValue, ShakeOptions, TAdapter> builder, uint seed)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, ShakeOptions>
        {
            builder.buffer.Data.Options.RandomState = new Unity.Mathematics.Random(seed);
            return builder;
        }

        /// <summary>
        /// Enable support for Rich Text tags.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="richTextEnabled">Whether to support Rich Text tags</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, StringOptions, TAdapter> WithRichText<TValue, TAdapter>(this MotionBuilder<TValue, StringOptions, TAdapter> builder, bool richTextEnabled = true)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, StringOptions>
        {
            builder.buffer.Data.Options.RichTextEnabled = richTextEnabled;
            return builder;
        }

        /// <summary>
        /// Specify the random number seed used to display scramble characters.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="seed">Rrandom number seed</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, StringOptions, TAdapter> WithRandomSeed<TValue, TAdapter>(this MotionBuilder<TValue, StringOptions, TAdapter> builder, uint seed)
           where TValue : unmanaged
           where TAdapter : unmanaged, IMotionAdapter<TValue, StringOptions>
        {
            builder.buffer.Data.Options.RandomState = new Unity.Mathematics.Random(seed);
            return builder;
        }

        /// <summary>
        /// Fill in the parts that are not yet displayed with random strings.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="scrambleMode">Type of characters used for blank padding</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, StringOptions, TAdapter> WithScrambleChars<TValue, TAdapter>(this MotionBuilder<TValue, StringOptions, TAdapter> builder, ScrambleMode scrambleMode)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, StringOptions>
        {
            if (scrambleMode == ScrambleMode.Custom) throw new ArgumentException("ScrambleMode.Custom cannot be specified explicitly. Use WithScrambleMode(FixedString64Bytes) instead.");
            builder.buffer.Data.Options.ScrambleMode = scrambleMode;
            return builder;
        }

        /// <summary>
        /// Fill in the parts that are not yet displayed with random strings.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="customScrambleChars">Characters used for blank padding</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        public static MotionBuilder<TValue, StringOptions, TAdapter> WithScrambleChars<TValue, TAdapter>(this MotionBuilder<TValue, StringOptions, TAdapter> builder, FixedString64Bytes customScrambleChars)
            where TValue : unmanaged
            where TAdapter : unmanaged, IMotionAdapter<TValue, StringOptions>
        {
            builder.buffer.Data.Options.ScrambleMode = ScrambleMode.Custom;
            builder.buffer.Data.Options.CustomScrambleChars = customScrambleChars;
            return builder;
        }
    }
}