using System.Collections.Generic;

namespace LitMotion.Sequences
{
    public interface IMotionSequenceBuilder
    {
        ICollection<IMotionFactory> Factories { get; }
        MotionSequence Build();
    }
}