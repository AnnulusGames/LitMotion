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
        [SerializeField] UnityEvent onRevert;

        public override MotionHandle Play()
        {
            onPlay.Invoke();
            return LMotion.Create(0f, 1f, 0f)
                .RunWithoutBinding();
        }

        public override void Revert()
        {
            onRevert.Invoke();
        }
    }
}