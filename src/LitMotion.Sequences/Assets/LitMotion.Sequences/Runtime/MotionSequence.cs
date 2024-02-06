using System;
using System.Collections.Generic;
using System.Linq;

namespace LitMotion.Sequences
{

    public sealed class MotionSequence
    {
        public static IMotionSequenceBuilder CreateBuilder() => new MotionSequenceBuilder();

        public MotionSequence(IEnumerable<IMotionSequenceConfiguration> factories)
        {
            this.factories = factories.ToArray();
            handles = new(this.factories.Length);
            factoryQueue = new(this.factories.Length);
        }

        readonly IMotionSequenceConfiguration[] factories;

        readonly MinimumList<MotionHandle> handles;
        readonly MinimumQueue<IMotionSequenceConfiguration> factoryQueue;

        float playbackSpeed = 1f;
        bool canceledOrCompletedMannually;

        static readonly MinimumList<MotionHandle> buffer = new();

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
            canceledOrCompletedMannually = true;

            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) handle.Complete();
            }
            handles.Clear();

            while (factoryQueue.TryDequeue(out var factory))
            {
                buffer.Clear();
                factory.Configure(new MotionSequenceBufferWriter(buffer));
                var bufferSpan = buffer.AsSpan();
                for (int i = 0; i < bufferSpan.Length; i++) bufferSpan[i].Complete();
            }

            OnCompleted?.Invoke();
        }

        public void Cancel()
        {
            canceledOrCompletedMannually = true;

            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) handle.Cancel();
            }
            handles.Clear();
            factoryQueue.Clear();

            OnCanceled?.Invoke();
        }

        public bool IsActive()
        {
            if (factoryQueue.Count > 0) return true;
            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) return true;
            }
            return false;
        }

        void InternalPlay()
        {
            canceledOrCompletedMannually = false;

            for (int i = 0; i < factories.Length; i++)
            {
                var factory = factories[i];
                factoryQueue.Enqueue(factory);
            }

            Run();

            OnCompleted?.Invoke();
        }

        void Run()
        {
            if (!factoryQueue.TryDequeue(out var factory)) return;

            buffer.Clear();
            factory.Configure(new MotionSequenceBufferWriter(buffer));

            if (buffer.Count > 0)
            {
                for (int i = 0; i < buffer.Count; i++)
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
                    if (sequence.canceledOrCompletedMannually) return;
                    sequence.Run();
                });

                return;
            }

            if (canceledOrCompletedMannually) return;
            Run();
        }
    }
}