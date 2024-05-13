using System.Collections.Generic;

namespace LitMotion.Sequences
{
    internal sealed class MotionSequenceBuilder : IMotionSequenceBuilder
    {
        public ICollection<IMotionSequenceItem> Items => items;
        readonly List<IMotionSequenceItem> items = new();

        public MotionSequence Build()
        {
            return new MotionSequence(items);
        }
    }
}