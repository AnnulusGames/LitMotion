namespace LitMotion.Sequences
{
    internal struct MotionSequenceItem
    {
        public MotionSequenceItem(double position, MotionHandle handle)
        {
            Position = position;
            Handle = handle;
        }

        public double Position;
        public MotionHandle Handle;
    }

    internal sealed class MotionSequenceSource
    {
        internal MotionSequenceSource(MotionSequenceItem[] items, double duration)
        {
            this.items = items;
            this.duration = duration;
        }

        readonly MotionSequenceItem[] items;
        double duration;
        double time;

        public double Time
        {
            get => time;
            set
            {
                time = value;

                foreach (var item in items)
                {
                    item.Handle.Time = time;
                }
            }
        }

        public double Duration => duration;
    }
}