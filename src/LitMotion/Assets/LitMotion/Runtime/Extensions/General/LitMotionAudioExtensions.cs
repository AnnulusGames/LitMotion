#if LITMOTION_SUPPORT_UNITY_AUDIO
using UnityEngine;
using UnityEngine.Audio;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for AudioSource and AudioMixer.
    /// </summary>
    public static class LitMotionAudioExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to AudioSource.volume
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVolume<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioSource audioSource)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(audioSource);
            return builder.Bind(audioSource, static (x, target) =>
            {
                target.volume = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to AudioSource.pitch
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPitch<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioSource audioSource)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(audioSource);
            return builder.Bind(audioSource, static (x, target) =>
            {
                target.pitch = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to AudioMixer exposed parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAudioMixerFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioMixer audioMixer, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(audioMixer);
            return builder.Bind(audioMixer, name, static (x, audioMixer, name) =>
            {
                audioMixer.SetFloat(name, x);
            });
        }
    }
}
#endif
