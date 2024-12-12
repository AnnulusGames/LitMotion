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

        public void Play()
        {
            if (handle.IsActive())
            {
                handle.PlaybackSpeed = 1f;
                return;
            }

            var builder = LSequence.Create();

            foreach (var component in components)
            {
                var handle = component.Play();
                component.TrackedHandle = handle;
                builder.Join(handle);
            }

            handle = builder.Schedule().Preserve();
        }

        public void Stop()
        {
            if (!handle.IsActive()) return;
            handle.PlaybackSpeed = 0f;
        }

        public void Reset()
        {
            handle.TryCancel();
        }

        void OnDestroy()
        {
            handle.TryCancel();
        }
    }
}