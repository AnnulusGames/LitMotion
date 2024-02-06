using System.Collections.Generic;

namespace LitMotion.Sequences
{
    internal sealed class MotionSequenceBuilder : IMotionSequenceBuilder
    {
        public ICollection<IMotionSequenceConfiguration> Factories => factories;
        readonly List<IMotionSequenceConfiguration> factories = new();

        public MotionSequence Build()
        {
            return new MotionSequence(factories);
        }
    }
}