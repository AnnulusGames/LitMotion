using System;
using System.Collections.Generic;
using LitMotion.Animation.Components;
using LitMotion.Collections;
using UnityEngine;

namespace LitMotion.Animation
{
    [AddComponentMenu("LitMotion Animation")]
    public sealed class LitMotionAnimation : MonoBehaviour
    {
        enum AnimationMode
        {
            Parallel,
            Sequential
        }

        [SerializeField] bool playOnAwake = true;
        [SerializeField] AnimationMode animationMode;

        [SerializeReference]
        LitMotionAnimationComponent[] components;

        Queue<LitMotionAnimationComponent> queue = new();
        FastListCore<LitMotionAnimationComponent> playingComponents;

        public IReadOnlyList<LitMotionAnimationComponent> Components => components;

        void Start()
        {
            if (playOnAwake) Play();
        }

        void MoveNextMotion()
        {
            if (queue.TryDequeue(out var queuedComponent))
            {
                try
                {
                    var handle = queuedComponent.Play().Preserve();
                    MotionManager.GetManagedDataRef(handle, false).OnCompleteAction += MoveNextMotion;
                    queuedComponent.TrackedHandle = handle;
                    playingComponents.Add(queuedComponent);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }
        }

        public void Play()
        {
            var isPlaying = false;

            foreach (var component in playingComponents.AsSpan())
            {
                var handle = component.TrackedHandle;
                if (handle.IsActive())
                {
                    handle.PlaybackSpeed = 1f;
                    isPlaying = true;
                }
            }

            if (isPlaying) return;

            playingComponents.Clear();

            switch (animationMode)
            {
                case AnimationMode.Sequential:
                    foreach (var component in components)
                    {
                        if (component == null) continue;
                        if (!component.Enabled) continue;
                        queue.Enqueue(component);
                    }

                    MoveNextMotion();
                    break;
                case AnimationMode.Parallel:
                    foreach (var component in components)
                    {
                        if (component == null) continue;
                        if (!component.Enabled) continue;

                        try
                        {
                            var handle = component.Play().Preserve();
                            component.TrackedHandle = handle;
                            playingComponents.Add(component);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                        }
                    }
                    break;
            }
        }

        public void Stop()
        {
            foreach (var component in playingComponents.AsSpan())
            {
                var handle = component.TrackedHandle;
                if (handle.IsActive()) handle.PlaybackSpeed = 0f;
            }
        }

        public void Reset()
        {
            var span = playingComponents.AsSpan();
            span.Reverse();
            foreach (var component in span)
            {
                var handle = component.TrackedHandle;
                handle.TryCancel();
                component.Revert();
            }

            playingComponents.Clear();
            queue.Clear();
        }

        public bool IsPlaying
        {
            get
            {
                if (queue.Count > 0) return true;

                foreach (var component in playingComponents.AsSpan())
                {
                    var handle = component.TrackedHandle;
                    if (handle.IsActive()) return true;
                }

                return false;
            }
        }

        void OnDestroy()
        {
            Reset();
        }
    }
}