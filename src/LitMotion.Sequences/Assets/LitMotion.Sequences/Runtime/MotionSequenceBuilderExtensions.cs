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

        public static IMotionSequenceBuilder AppendGroup(this IMotionSequenceBuilder builder, Action<MotionSequenceBufferWriter> factoryDelegate)
        {
            builder.Items.Add(new _AppendGroup(factoryDelegate));
            return builder;
        }

        public static IMotionSequenceBuilder AppendGroup<TState>(this IMotionSequenceBuilder builder, TState state, Action<TState, MotionSequenceBufferWriter> factoryDelegate)
        {
            builder.Items.Add(new _AppendGroupWithState<TState>(state, factoryDelegate));
            return builder;
        }

        sealed class _Append : IMotionSequenceItem
        {
            public _Append(Func<MotionHandle> factoryDelegate)
            {
                this.factoryDelegate = factoryDelegate;
            }

            readonly Func<MotionHandle> factoryDelegate;

            public void Configure(MotionSequenceBufferWriter writer)
            {
                writer.Add(factoryDelegate());
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

            public void Configure(MotionSequenceBufferWriter writer)
            {
                writer.Add(factoryDelegate(state));
            }
        }


        sealed class _AppendCallback : IMotionSequenceItem
        {
            public _AppendCallback(Action callback)
            {
                this.callback = callback;
            }

            readonly Action callback;

            public void Configure(MotionSequenceBufferWriter writer)
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

            public void Configure(MotionSequenceBufferWriter writer)
            {
                callback.Invoke(state);
            }
        }

        sealed class _AppendGroup : IMotionSequenceItem
        {
            public _AppendGroup(Action<MotionSequenceBufferWriter> Delegate)
            {
                this.factoryDelegate = Delegate;
            }

            readonly Action<MotionSequenceBufferWriter> factoryDelegate;

            public void Configure(MotionSequenceBufferWriter writer)
            {
                factoryDelegate(writer);
            }
        }

        sealed class _AppendGroupWithState<T> : IMotionSequenceItem
        {
            public _AppendGroupWithState(T state, Action<T, MotionSequenceBufferWriter> Delegate)
            {
                this.state = state;
                this.factoryDelegate = Delegate;
            }

            readonly T state;
            readonly Action<T, MotionSequenceBufferWriter> factoryDelegate;

            public void Configure(MotionSequenceBufferWriter writer)
            {
                factoryDelegate(state, writer);
            }
        }
    }
}