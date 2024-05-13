using System;
using System.Collections.Generic;
using System.Linq;
using LitMotion.Collections;

namespace LitMotion.Sequences
{
    public sealed class MotionSequence
    {
        public static IMotionSequenceBuilder CreateBuilder() => new MotionSequenceBuilder();

        public MotionSequence(IEnumerable<IMotionSequenceItem> items)
        {
            this.items = items.ToArray();
            itemQueue = new(this.items.Length);
        }

        readonly IMotionSequenceItem[] items;

        FastListCore<MotionHandle> handles;
        readonly FastQueue<IMotionSequenceItem> itemQueue;

        float playbackSpeed = 1f;
        bool canceledOrCompletedManually;

        static readonly FastList<MotionHandle> buffer = new();

        public event Action OnCompleted;
        public event Action OnCanceled;

        public float PlaybackSpeed
        {
            get => playbackSpeed;
            set
            {
                playbackSpeed = value;
                var handleSpan = handles.AsSpan();
                for (int i = 0; i < handleSpan.Length; i++)
                {
                    var handle = handleSpan[i];
                    if (handle.IsActive())
                    {
                        handle.PlaybackSpeed = playbackSpeed;
                    }
                }
            }
        }

        public void Play()
        {
            if (IsActive()) throw new InvalidOperationException("Play cannot be called because the sequence is playing.");
            InternalPlay();
        }

        public void Complete()
        {
            canceledOrCompletedManually = true;

            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) handle.Complete();
            }
            handles.Clear();

            while (itemQueue.TryDequeue(out var item))
            {
                buffer.Clear();
                item.Configure(new SequenceItemBuilder(buffer));
                var bufferSpan = buffer.AsSpan();
                for (int i = 0; i < bufferSpan.Length; i++) bufferSpan[i].Complete();
            }

            OnCompleted?.Invoke();
        }

        public void Cancel()
        {
            canceledOrCompletedManually = true;

            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) handle.Cancel();
            }
            handles.Clear();
            itemQueue.Clear();

            OnCanceled?.Invoke();
        }

        public bool IsActive()
        {
            if (itemQueue.Count > 0) return true;
            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handles.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) return true;
            }
            return false;
        }

        void InternalPlay()
        {
            canceledOrCompletedManually = false;

            for (int i = 0; i < items.Length; i++)
            {
                var factory = items[i];
                itemQueue.Enqueue(factory);
            }

            Run();
        }

        void Run()
        {
            if (!itemQueue.TryDequeue(out var item))
            {
                OnCompleted?.Invoke();
                return;
            }

            buffer.Clear();
            item.Configure(new SequenceItemBuilder(buffer));

            if (buffer.Length > 0)
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    var handle = buffer[i];
                    if (handle.IsActive())
                    {
                        handles.Add(handle);
                        handle.PlaybackSpeed = playbackSpeed;
                    }
                }

                MotionSequencePromise.Create(buffer.AsSpan(), this, state =>
                {
                    var sequence = (MotionSequence)state;
                    if (sequence.canceledOrCompletedManually) return;
                    sequence.Run();
                });

                return;
            }

            if (canceledOrCompletedManually) return;
            Run();
        }
    }
}