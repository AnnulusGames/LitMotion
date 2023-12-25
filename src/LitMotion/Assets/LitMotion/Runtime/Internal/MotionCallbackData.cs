using System;
using System.Threading;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace LitMotion
{
    /// <summary>
    /// A structure that holds motion callbacks.
    /// </summary>
    public struct MotionCallbackData
    {
        public bool HasState;
        public object State;
        public object UpdateAction;
        public Action OnCompleteAction;
        public CancellationToken CancellationToken;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InvokeUnsafe<TValue>(in TValue value) where TValue : unmanaged
        {
            if (HasState)
            {
                UnsafeUtility.As<object, Action<TValue, object>>(ref UpdateAction)?.Invoke(value, State);
            }
            else
            {
                UnsafeUtility.As<object, Action<TValue>>(ref UpdateAction)?.Invoke(value);
            }
        }

        public static MotionCallbackData Create<T>(Action<T> action)
        {
            var callbacks = new MotionCallbackData
            {
                UpdateAction = action
            };
            return callbacks;
        }

        public static MotionCallbackData Create<TValue, TState>(TState state, Action<TValue, TState> action) where TState : class
        {
            var callbacks = new MotionCallbackData
            {
                HasState = true,
                State = state,
                UpdateAction = action
            };

            return callbacks;
        }
    }
}