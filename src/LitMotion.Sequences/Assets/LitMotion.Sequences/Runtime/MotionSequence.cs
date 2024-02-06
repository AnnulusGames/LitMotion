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

        static readonly MinimumList<MotionHandle> buffer = new();

        public void Play(SequencePlayMode playMode = SequencePlayMode.Sequential)
        {
            if (IsPlaying()) throw new InvalidOperationException("Play cannot be called because the sequence is playing.");

            switch (playMode)
            {
                case SequencePlayMode.Sequential:
                    _ = PlaySequentialAsync();
                    break;
                case SequencePlayMode.Parallel:
                    PlayParallel();
                    break;
            }
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

        public bool IsPlaying()
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
                        handles.Add(buffer[0]);
                        await buffer[0].ToValueTask();
                    }
                    else
                    {
                        var tmpList = new TempList<ValueTask>(buffer.Count);
                        try
                        {
                            for (int i = 0; i < buffer.Count; i++)
                            {
                                var handle = buffer[i];
                                handles.Add(handle);
                                tmpList.Add(handle.ToValueTask());
                            }
                            await ValueTaskHelper.WhenAll(ref tmpList);
                        }
                        finally
                        {
                            tmpList.Dispose();
                        }
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

        void PlayParallel()
        {
            for (int i = 0; i < factories.Length; i++)
            {
                var factory = factories[i];

                try
                {
                    buffer.Clear();
                    factory.Configure(new MotionSequenceBufferWriter(buffer));
                    var bufferSpan = buffer.AsSpan();
                    for (int n = 0; n < bufferSpan.Length; n++) handles.Add(buffer[n]);
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()(ex);
                }
            }
        }
    }
}