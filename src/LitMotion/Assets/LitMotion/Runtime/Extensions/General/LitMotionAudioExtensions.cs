using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Assertions;

namespace LitMotion.Extensions
{
    public static class LitMotionAudioExtensions
    {
        public static MotionHandle BindToVolume<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioSource audioSource)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Assert.IsNotNull(audioSource);
            return builder.BindWithState(audioSource, (x, target) =>
            {
                if (target == null) return;
                target.volume = x;
            });
        }

        public static MotionHandle BindToPitch<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioSource audioSource)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Assert.IsNotNull(audioSource);
            return builder.BindWithState(audioSource, (x, target) =>
            {
                if (target == null) return;
                target.pitch = x;
            });
        }

        public static MotionHandle BindToAudioMixerFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AudioMixer audioMixer, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Assert.IsNotNull(audioMixer);
            return builder.BindWithState(audioMixer, (x, target) =>
            {
                if (target == null) return;
                target.SetFloat(name, x);
            });
        }
    }
}