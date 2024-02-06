using System.Collections.Generic;

namespace LitMotion.Sequences
{
    public interface IMotionSequenceBuilder
    {
        ICollection<IMotionSequenceConfiguration> Factories { get; }
        MotionSequence Build();
    }
}