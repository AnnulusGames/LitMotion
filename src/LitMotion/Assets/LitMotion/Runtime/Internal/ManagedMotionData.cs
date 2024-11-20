using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace LitMotion
{
    /// <summary>
    /// A structure that holds motion managed data.
    /// </summary>
    [StructLayout(LayoutKind.Auto)]
    public struct ManagedMotionData
    {
        public bool IsCallbackRunning;
        public bool CancelOnError;
        public bool SkipValuesDuringDelay;
        public object State;

        public object UpdateAction;
        public Action OnCompleteAction;
        public Action OnCancelAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InvokeUnsafe<TValue>(in TValue value) where TValue : unmanaged
        {
            if (State != null)
            {
                UnsafeUtility.As<object, Action<TValue>>(ref UpdateAction)?.Invoke(value);
            }
            else
            {
                UnsafeUtility.As<object, Action<TValue, object>>(ref UpdateAction)?.Invoke(value, State);
            }
        }

        public readonly static ManagedMotionData Default = new()
        {
            SkipValuesDuringDelay = true,
        };
    }
}