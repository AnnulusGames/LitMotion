using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LitMotion.Sequences
{
    public sealed class MotionSequence
    {
        public static IMotionSequenceBuilder CreateBuilder() => new MotionSequenceBuilder();

        public MotionSequence(IEnumerable<IMotionFactory> factories)
        {
            this.factories = factories.ToArray();
            handles = new(this.factories.Length);
            factoryQueue = new(this.factories.Length);
        }

        readonly IMotionFactory[] factories;

        readonly List<MotionHandle> handles;
        readonly Queue<IMotionFactory> factoryQueue;

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
            for (int i = 0; i < handles.Count; i++)
            {
                var handle = handles[i];
                if (handle.IsActive()) handle.Complete();
            }
            handles.Clear();

            while (factoryQueue.TryDequeue(out var factory))
            {
                factory.CreateMotion().Complete();
            }
        }

        public void Cancel()
        {
            for (int i = 0; i < handles.Count; i++)
            {
                var handle = handles[i];
                if (handle.IsActive()) handle.Cancel();
            }
            handles.Clear();
            factoryQueue.Clear();
        }

        public bool IsPlaying()
        {
            if (factoryQueue.Count > 0) return true;
            for (int i = 0; i < handles.Count; i++)
            {
                var handle = handles[i];
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
                    var handle = factory.CreateMotion();
                    handles.Add(handle);
                    await handle.ToValueTask();
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
                    var handle = factory.CreateMotion();
                    handles.Add(handle);
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()(ex);
                }
            }
        }
    }
}