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
            ArrayPool<MotionSequenceItem>.Shared.Return(source.itemsBuffer);
            source.itemsBuffer = null;
            pool.TryPush(source);
        }

        public void Initialize(MotionHandle handle, ReadOnlySpan<MotionSequenceItem> items, double duration)
        {
            this.handle = handle;
            this.itemCount = items.Length;
            this.itemsBuffer = ArrayPool<MotionSequenceItem>.Shared.Rent(itemCount);
            items.CopyTo(this.itemsBuffer);
            this.duration = duration;
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
        MotionSequenceItem[] itemsBuffer;
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

                foreach (var item in itemsBuffer.AsSpan(0, itemCount))
                {
                    item.Handle.Time = time - item.Position;
                }
            }
        }

        public double Duration => duration;
    }
}