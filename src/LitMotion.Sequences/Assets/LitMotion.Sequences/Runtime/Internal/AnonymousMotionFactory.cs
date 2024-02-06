using System;

namespace LitMotion.Sequences
{
    internal sealed class AnonymousMotionFactory : IMotionFactory
    {
        public AnonymousMotionFactory(Func<MotionHandle> factoryDelegate)
        {
            this.factoryDelegate = factoryDelegate;
        }

        readonly Func<MotionHandle> factoryDelegate;

        public MotionHandle CreateMotion()
        {
            return factoryDelegate();
        }
    }

    internal sealed class AnonymousMotionFactoryWithState<T> : IMotionFactory
    {
        public AnonymousMotionFactoryWithState(T state, Func<T, MotionHandle> factoryDelegate)
        {
            this.state = state;
            this.factoryDelegate = factoryDelegate;
        }

        readonly T state;
        readonly Func<T, MotionHandle> factoryDelegate;

        public MotionHandle CreateMotion()
        {
            return factoryDelegate(state);
        }
    }
}