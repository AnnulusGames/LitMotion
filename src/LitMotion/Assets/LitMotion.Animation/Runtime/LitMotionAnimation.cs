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

        [SerializeField] bool playOnAwake;
        [SerializeField] AnimationMode animationMode;

        [SerializeReference]
        LitMotionAnimationComponent[] components = new LitMotionAnimationComponent[]
        {
            new PositionAnimation(),
            new PositionAnimation(),
        };

        Queue<LitMotionAnimationComponent> queue = new();
        FastListCore<MotionHandle> handles;

        public IReadOnlyList<LitMotionAnimationComponent> Components => components;

        void Start()
        {
            if (playOnAwake) Play();
        }

        void MoveNextMotion()
        {
            if (queue.TryDequeue(out var queuedComponent))
            {
                var handle = queuedComponent.Play().Preserve();
                MotionManager.GetManagedDataRef(handle, MotionStoragePermission.Admin).OnCompleteAction += MoveNextMotion;
                queuedComponent.TrackedHandle = handle;
                handles.Add(handle);
            }
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

                        var handle = component.Play().Preserve();
                        component.TrackedHandle = handle;
                        handles.Add(handle);
                    }
                    break;
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
            queue.Clear();
        }

        public bool IsPlaying
        {
            get
            {
                if (queue.Count > 0) return true;

                foreach (var handle in handles.AsSpan())
                {
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