using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Assertions;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionAudioExtensions
    {
        public static MotionHandle BindToVolume(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, AudioSource audioSource)
        {
            Assert.IsNotNull(audioSource);
            return builder.BindWithState(audioSource, (x, target) =>
            {
                if (target == null) return;
                target.volume = x;
            });
        }

        public static MotionHandle BindToPitch(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, AudioSource audioSource)
        {
            Assert.IsNotNull(audioSource);
            return builder.BindWithState(audioSource, (x, target) =>
            {
                if (target == null) return;
                target.pitch = x;
            });
        }

        public static MotionHandle BindToAudioMixerFloat(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, AudioMixer audioMixer, string name)
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