using System;

namespace LitMotion.Sequences
{
    public static class MotionSequenceBuilderExtensions
    {
        public static IMotionSequenceBuilder Add(this IMotionSequenceBuilder builder, Func<MotionHandle> factoryDelegate)
        {
            builder.Factories.Add(new AnonymousMotionFactory(factoryDelegate));
            return builder;
        }

        public static IMotionSequenceBuilder Add<TState>(this IMotionSequenceBuilder builder, TState state, Func<TState, MotionHandle> factoryDelegate)
        {
            builder.Factories.Add(new AnonymousMotionFactoryWithState<TState>(state, factoryDelegate));
            return builder;
        }
    }
}