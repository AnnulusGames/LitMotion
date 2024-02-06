using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        static readonly MinimumList<MotionHandle> buffer = new();
        static readonly MinimumList<ValueTask> taskBuffer = new();

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
            _ = PlaySequentialAsync();
        }

        public void Complete()
        {
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
        }

        public void Cancel()
        {
            var handleSpan = handles.AsSpan();
            for (int i = 0; i < handleSpan.Length; i++)
            {
                var handle = handleSpan[i];
                if (handle.IsActive()) handle.Cancel();
            }
            handles.Clear();
            factoryQueue.Clear();
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

        async ValueTask PlaySequentialAsync()
        {
            for (int i = 0; i < factories.Length; i++)
            {
                var factory = factories[i];
                factoryQueue.Enqueue(factory);
            }

            while (factoryQueue.TryDequeue(out var factory))
            {
                try
                {
                    buffer.Clear();
                    factory.Configure(new MotionSequenceBufferWriter(buffer));

                    if (buffer.Count == 0)
                    {
                        // do nothing
                    }
                    if (buffer.Count == 1)
                    {
                        var handle = buffer[0];
                        if (handle.IsActive())
                        {
                            handles.Add(handle);
                            handle.PlaybackSpeed = playbackSpeed;
                            await handle.ToValueTask();
                        }
                    }
                    else
                    {
                        taskBuffer.Clear();
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            var handle = buffer[i];
                            if (handle.IsActive())
                            {
                                handles.Add(handle);
                                handle.PlaybackSpeed = playbackSpeed;
                                taskBuffer.Add(handle.ToValueTask());
                            }
                        }
                        await ValueTaskHelper.WhenAll(taskBuffer);
                    }
                }
                catch (OperationCanceledException)
                {
                    // ignore cancellation
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()(ex);
                }
            }
        }
    }
}