#if LITMOTION_ANIMATION_UNITY_AUDIO

using System;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [LitMotionAnimationComponentMenu("Audio/Audio Source/Volume")]
    public sealed class AudioSourceVolumeAnimation : FloatPropertyAnimationComponent<AudioSource>
    {
        protected override float GetValue(AudioSource target) => target.volume;
        protected override void SetValue(AudioSource target, in float value) => target.volume = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Audio/Audio Source/Pitch")]
    public sealed class AudioSourcePitchAnimation : FloatPropertyAnimationComponent<AudioSource>
    {
        protected override float GetValue(AudioSource target) => target.pitch;
        protected override void SetValue(AudioSource target, in float value) => target.pitch = value;
    }
}

#endif