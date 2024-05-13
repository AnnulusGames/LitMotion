using System;

namespace LitMotion.Sequences
{
    public static class MotionSequenceBuilderExtensions
    {
        public static IMotionSequenceBuilder Append(this IMotionSequenceBuilder builder, Func<MotionHandle> factoryDelegate)
        {
            builder.Items.Add(new _Append(factoryDelegate));
            return builder;
        }

        public static IMotionSequenceBuilder Append<TState>(this IMotionSequenceBuilder builder, TState state, Func<TState, MotionHandle> factoryDelegate)
        {
            builder.Items.Add(new _AppendWithState<TState>(state, factoryDelegate));
            return builder;
        }

        public static IMotionSequenceBuilder AppendCallback(this IMotionSequenceBuilder builder, Action callback)
        {
            builder.Items.Add(new _AppendCallback(callback));
            return builder;
        }

        public static IMotionSequenceBuilder AppendCallback<TState>(this IMotionSequenceBuilder builder, TState state, Action<TState> callback)
        {
            builder.Items.Add(new _AppendCallbackWithState<TState>(state, callback));
            return builder;
        }

        public static IMotionSequenceBuilder AppendParallel(this IMotionSequenceBuilder builder, Action<SequenceItemBuilder> factoryDelegate)
        {
            builder.Items.Add(new _AppendParallel(factoryDelegate));
            return builder;
        }

        public static IMotionSequenceBuilder AppendParallel<TState>(this IMotionSequenceBuilder builder, TState state, Action<TState, SequenceItemBuilder> factoryDelegate)
        {
            builder.Items.Add(new _AppendParallelWithState<TState>(state, factoryDelegate));
            return builder;
        }

        sealed class _Append : IMotionSequenceItem
        {
            public _Append(Func<MotionHandle> factoryDelegate)
            {
                this.factoryDelegate = factoryDelegate;
            }

            readonly Func<MotionHandle> factoryDelegate;

            public void Configure(SequenceItemBuilder builder)
            {
                builder.Add(factoryDelegate());
            }
        }

        sealed class _AppendWithState<T> : IMotionSequenceItem
        {
            public _AppendWithState(T state, Func<T, MotionHandle> factoryDelegate)
            {
                this.state = state;
                this.factoryDelegate = factoryDelegate;
            }

            readonly T state;
            readonly Func<T, MotionHandle> factoryDelegate;

            public void Configure(SequenceItemBuilder builder)
            {
                builder.Add(factoryDelegate(state));
            }
        }


        sealed class _AppendCallback : IMotionSequenceItem
        {
            public _AppendCallback(Action callback)
            {
                this.callback = callback;
            }

            readonly Action callback;

            public void Configure(SequenceItemBuilder builder)
            {
                callback.Invoke();
            }
        }

        sealed class _AppendCallbackWithState<T> : IMotionSequenceItem
        {
            public _AppendCallbackWithState(T state, Action<T> callback)
            {
                this.state = state;
                this.callback = callback;
            }

            readonly T state;
            readonly Action<T> callback;

            public void Configure(SequenceItemBuilder builder)
            {
                callback.Invoke(state);
            }
        }

        sealed class _AppendParallel : IMotionSequenceItem
        {
            public _AppendParallel(Action<SequenceItemBuilder> Delegate)
            {
                this.factoryDelegate = Delegate;
            }

            readonly Action<SequenceItemBuilder> factoryDelegate;

            public void Configure(SequenceItemBuilder builder)
            {
                factoryDelegate(builder);
            }
        }

        sealed class _AppendParallelWithState<T> : IMotionSequenceItem
        {
            public _AppendParallelWithState(T state, Action<T, SequenceItemBuilder> Delegate)
            {
                this.state = state;
                this.factoryDelegate = Delegate;
            }

            readonly T state;
            readonly Action<T, SequenceItemBuilder> factoryDelegate;

            public void Configure(SequenceItemBuilder builder)
            {
                factoryDelegate(state, builder);
            }
        }
    }
}