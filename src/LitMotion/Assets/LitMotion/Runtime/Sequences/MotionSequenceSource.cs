using System;
using System.Buffers;
using LitMotion.Collections;

namespace LitMotion.Sequences
{
    internal struct MotionSequenceItem : IComparable<MotionSequenceItem>
    {
        public MotionSequenceItem(double position, MotionHandle handle)
        {
            Position = position;
            Handle = handle;
        }

        public double Position;
        public MotionHandle Handle;

        public int CompareTo(MotionSequenceItem other)
        {
            return Position.CompareTo(other.Position);
        }
    }

    internal sealed class MotionSequenceSource : ILinkedPoolNode<MotionSequenceSource>
    {
        static LinkedPool<MotionSequenceSource> pool;

        public static MotionSequenceSource Rent()
        {
            if (!pool.TryPop(out var result)) result = new();
            return result;
        }

        public static void Return(MotionSequenceSource source)
        {
            ArrayPool<MotionSequenceItem>.Shared.Return(source.itemBuffer);
            source.itemBuffer = null;
            pool.TryPush(source);
        }

        public void Initialize(MotionHandle handle, MotionSequenceItem[] itemBuffer, int itemCount, double duration)
        {
            this.handle = handle;
            this.itemCount = itemCount;
            this.itemBuffer = itemBuffer;
            this.duration = duration;
            this.time = 0;

            Array.Sort(itemBuffer, 0, itemCount);
        }

        MotionSequenceSource()
        {
            onCompleteDelegate = () =>
            {
                if (MotionManager.IsActive(handle) && !MotionManager.GetDataRef(handle).IsPreserved)
                {
                    Return(this);
                }
            };
            onCancelDelegate = () => Return(this);
        }

        readonly Action onCompleteDelegate;
        readonly Action onCancelDelegate;
        MotionSequenceSource next;

        MotionHandle handle;
        MotionSequenceItem[] itemBuffer;
        int itemCount;
        double duration;
        double time;

        public ref MotionSequenceSource NextNode => ref next;

        public Action OnCompleteDelegate => onCompleteDelegate;
        public Action OnCancelDelegate => onCancelDelegate;

        public double Time
        {
            get => time;
            set
            {
                time = value;

                var span = itemBuffer.AsSpan(0, itemCount);

                var index = span.Length - 1;
                while (index >= 0)
                {
                    var item = span[index];
                    if (item.Position < time) break;
                    item.Handle.Time = time - item.Position;
                    index--;
                }

                foreach (var item in span[..(index + 1)])
                {
                    item.Handle.Time = time - item.Position;
                }
            }
        }

        public double Duration => duration;
    }
}