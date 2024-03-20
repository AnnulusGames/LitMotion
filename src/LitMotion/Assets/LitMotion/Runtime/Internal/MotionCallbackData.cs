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
        public byte StateCount;
        public bool IsCallbackRunning;
        public bool CancelOnError;
        public bool SkipValuesDuringDelay;
        public object State1;
        public object State2;
        public object State3;

        public object UpdateAction;
        public Action OnCompleteAction;
        public Action OnCancelAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InvokeUnsafe<TValue>(in TValue value) where TValue : unmanaged
        {
            switch (StateCount)
            {
                case 0:
                    UnsafeUtility.As<object, Action<TValue>>(ref UpdateAction)?.Invoke(value);
                    break;
                case 1:
                    UnsafeUtility.As<object, Action<TValue, object>>(ref UpdateAction)?.Invoke(value, State1);
                    break;
                case 2:
                    UnsafeUtility.As<object, Action<TValue, object, object>>(ref UpdateAction)?.Invoke(value, State1, State2);
                    break;
                case 3:
                    UnsafeUtility.As<object, Action<TValue, object, object, object>>(ref UpdateAction)?.Invoke(value, State1, State2, State3);
                    break;
            }
        }

        public readonly static MotionCallbackData Default = new()
        {
            SkipValuesDuringDelay = true,
        };
    }
}