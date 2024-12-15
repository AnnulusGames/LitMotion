using System;
using UnityEngine;
using UnityEngine.Events;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [LitMotionAnimationComponentMenu("Control/Delay")]
    public sealed class DelayComponent : LitMotionAnimationComponent
    {
        [SerializeField] float delay;

        public override MotionHandle Play()
        {
            return LMotion.Create(0f, 1f, delay)
                .RunWithoutBinding();
        }

        public override void Revert() { }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Control/Event")]
    public sealed class EventComponent : LitMotionAnimationComponent
    {
        [Space(5f)]
        [SerializeField] UnityEvent onPlay;

        public override MotionHandle Play()
        {
            return LMotion.Create(0f, 1f, 0f)
                .WithOnComplete(() => onPlay.Invoke())
                .RunWithoutBinding();
        }

        public override void Revert() { }
    }
}