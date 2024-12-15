using System.Collections.Generic;
using LitMotion.Animation.Components;
using LitMotion.Collections;
using UnityEngine;

namespace LitMotion.Animation
{
    [AddComponentMenu("LitMotion Animation")]
    public sealed class LitMotionAnimation : MonoBehaviour
    {
        [SerializeField] bool playOnAwake;

        [SerializeReference]
        LitMotionAnimationComponent[] components = new LitMotionAnimationComponent[]
        {
            new PositionAnimation(),
            new PositionAnimation(),
        };

        FastListCore<MotionHandle> handles;

        public IReadOnlyList<LitMotionAnimationComponent> Components => components;

        void Start()
        {
            if (playOnAwake) Play();
        }

        public void Play()
        {
            var isPlaying = false;

            foreach (var handle in handles.AsSpan())
            {
                if (handle.IsActive())
                {
                    handle.PlaybackSpeed = 1f;
                    isPlaying = true;
                }
            }

            if (isPlaying) return;

            handles.Clear();

            foreach (var component in components)
            {
                if (component == null) continue;
                if (!component.Enabled) continue;

                var handle = component.Play().Preserve();
                component.TrackedHandle = handle;
                handles.Add(handle);
            }
        }

        public void Stop()
        {
            foreach (var handle in handles.AsSpan())
            {
                if (handle.IsActive()) handle.PlaybackSpeed = 0f;
            }
        }

        public void Reset()
        {
            foreach (var handle in handles.AsSpan())
            {
                handle.TryCancel();
            }

            handles.Clear();
        }

        public bool IsPlaying
        {
            get
            {
                foreach (var handle in handles.AsSpan())
                {
                    if (handle.IsActive()) return true;
                }

                handles.Clear();
                return false;
            }
        }

        void OnDestroy()
        {
            Reset();
        }
    }
}