using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace LitMotion.Sequences
{
    public struct MotionSequenceBuilder : IDisposable
    {
        MotionSequenceItem[] buffer;
        int count;

        double tail;
        double duration;

        public MotionSequenceBuilder Append(MotionHandle handle)
        {
            handle.Preserve();
            handle.PlaybackSpeed = 0;

            var motionDuration = handle.Duration;
            if (double.IsInfinity(motionDuration))
            {
                throw new ArgumentException(); // TODO:
            }

            AddItem(new MotionSequenceItem(tail, handle));
            tail += motionDuration;
            duration += motionDuration;
            return this;
        }

        public MotionSequenceBuilder Insert(double position, MotionHandle handle)
        {
            handle.Preserve();
            handle.PlaybackSpeed = 0;

            var motionDuration = handle.Duration;
            if (double.IsInfinity(motionDuration))
            {
                throw new ArgumentException(); // TODO:
            }

            AddItem(new MotionSequenceItem(position, handle));
            duration = Math.Max(duration, position + motionDuration);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void AddItem(in MotionSequenceItem item)
        {
            if (buffer == null)
            {
                buffer = ArrayPool<MotionSequenceItem>.Shared.Rent(32);
            }
            else if (buffer.Length == count)
            {
                var newBuffer = ArrayPool<MotionSequenceItem>.Shared.Rent(count * 2);
                ArrayPool<MotionSequenceItem>.Shared.Return(buffer);
                buffer = newBuffer;
            }

            buffer[count] = item;
            count++;
        }

        public MotionHandle Run()
        {
            var source = MotionSequenceSource.Rent();
            var handle = LMotion.Create(0.0, duration, (float)duration)
                .WithOnComplete(source.OnCompleteDelegate)
                .WithOnCancel(source.OnCancelDelegate)
                .Bind(source, (x, source) => source.Time = x);

            source.Initialize(handle, buffer.AsSpan(0, count), duration);
            Dispose();
            return handle;
        }

        public void Dispose()
        {
            ArrayPool<MotionSequenceItem>.Shared.Return(buffer);
            buffer = null;
            tail = 0;
        }
    }
}