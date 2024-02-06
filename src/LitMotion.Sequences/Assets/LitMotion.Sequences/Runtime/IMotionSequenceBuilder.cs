using System.Collections.Generic;

namespace LitMotion.Sequences
{
    public interface IMotionSequenceBuilder
    {
        ICollection<IMotionSequenceItem> Items { get; }
        MotionSequence Build();
    }
}