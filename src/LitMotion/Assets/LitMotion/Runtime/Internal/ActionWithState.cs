using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal sealed record ActionWithState<TValue, TState>
        where TValue : unmanaged
        where TState : struct
    {
        public ActionWithState(TState state, Action<TValue, TState> action)
        {
            this.state = state;
            this.action = action;
        }

        readonly TState state;
        readonly Action<TValue, TState> action;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(TValue value)
        {
            action(value, state);
        }
    }
}