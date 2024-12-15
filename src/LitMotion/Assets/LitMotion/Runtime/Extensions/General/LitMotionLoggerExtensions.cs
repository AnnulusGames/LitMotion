using UnityEngine;

#if LITMOTION_SUPPORT_ZSTRING
using Cysharp.Text;
#endif

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Logger
    /// </summary>
    public static class LitMotionLoggerExtensions
    {
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
        /// Create a motion data and bind it to Debug.unityLogger
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="format">Log format</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToUnityLogger<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, string format)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return builder.Bind(format, static (x, format) => 
            {
#if LITMOTION_SUPPORT_ZSTRING
                var str = ZString.Format(format, x);
#else
                var str = string.Format(format, x);
#endif
                Debug.unityLogger.Log(str);
            });
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
            return builder.Bind(logger, static (x, logger) => logger.Log(x));
        }

        /// <summary>
        /// Create a motion data and bind it to UnityEngine.ILogger
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="logger">Logger</param>
        /// <param name="format">Log format</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToUnityLogger<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, ILogger logger, string format)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Error.IsNull(logger);
            return builder.Bind(logger, format, static (x, logger, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                var str = ZString.Format(format, x);
#else
                var str = string.Format(format, x);
#endif
                logger.Log(str);
            });
        }
    }
}