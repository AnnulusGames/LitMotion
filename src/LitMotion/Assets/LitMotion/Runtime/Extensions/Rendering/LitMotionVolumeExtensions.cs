#if LITMOTION_SUPPORT_RENDER_PIPELINES

using UnityEngine.Rendering;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Volume.
    /// </summary>
    public static class LitMotionVolumeExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to Volume.weight
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="volume">Target component</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToWeight<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Volume volume)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(volume);
            return builder.Bind(volume, static (x, volume) => volume.weight = x);
        }
    }
}

#endif