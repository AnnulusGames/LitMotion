using System.Collections.Generic;
using LitMotion.Animation.Components;
using LitMotion.Sequences;
using UnityEngine;

namespace LitMotion.Animation
{
    [AddComponentMenu("LitMotion Animation")]
    public sealed class LitMotionAnimation : MonoBehaviour
    {
        [SerializeReference]
        LitMotionAnimationComponent[] components = new LitMotionAnimationComponent[]
        {
            new Position(),
            new Position(),
        };

        MotionHandle handle;

        public IReadOnlyList<LitMotionAnimationComponent> Components => components;
        public MotionHandle Handle => handle;

        public MotionHandle Play()
        {
            handle.TryCancel();

            var builder = LSequence.Create();

            foreach (var component in components)
            {
                var handle = component.Play();
#if UNITY_EDITOR
                component.TrackedHandle = handle;
#endif
                builder.Join(handle);
            }

            handle = builder.Schedule();
            return handle;
        }
    }
}