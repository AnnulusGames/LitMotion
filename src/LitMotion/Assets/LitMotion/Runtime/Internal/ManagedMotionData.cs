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
    public unsafe struct ManagedMotionData
    {
        public bool CancelOnError;
        public bool SkipValuesDuringDelay;
        public byte StateCount;
        public object State0;
        public object State1;
        public object State2;
        public object UpdateAction;
        public Action OnCompleteAction;
        public Action OnCancelAction;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateUnsafe<TValue>(in TValue value) where TValue : unmanaged
        {
            switch (StateCount)
            {
                case 0:
                    UnsafeUtility.As<object, Action<TValue>>(ref UpdateAction)?.Invoke(value);
                    break;
                case 1:
                    UnsafeUtility.As<object, Action<TValue, object>>(ref UpdateAction)?.Invoke(value, State0);
                    break;
                case 2:
                    UnsafeUtility.As<object, Action<TValue, object, object>>(ref UpdateAction)?.Invoke(value, State0, State1);
                    break;
                case 3:
                    UnsafeUtility.As<object, Action<TValue, object, object, object>>(ref UpdateAction)?.Invoke(value, State0, State1, State2);
                    break;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void InvokeCancel()
        {
            try
            {
                OnCancelAction?.Invoke();
            }
            catch (Exception ex)
            {
                MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
            }
        }

        public readonly static ManagedMotionData Default = new()
        {
            SkipValuesDuringDelay = true,
        };
    }
}