using System;
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
        public bool IsCallbackRunning;
        public bool CancelOnError;
        public object State;
        
        public object UpdateAction;
        public Action OnCompleteAction;
        public Action OnCancelAction;
        
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
    }
}