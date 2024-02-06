using System.Collections.Generic;

namespace LitMotion.Sequences
{
    internal sealed class MotionSequenceBuilder : IMotionSequenceBuilder
    {
        public ICollection<IMotionFactory> Factories => factories;
        readonly List<IMotionFactory> factories = new();

        public MotionSequence Build()
        {
            return new MotionSequence(factories);
        }
    }
}