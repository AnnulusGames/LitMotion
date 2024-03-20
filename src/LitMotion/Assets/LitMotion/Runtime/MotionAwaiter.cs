using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    public readonly struct MotionAwaiter : ICriticalNotifyCompletion
    {
        readonly MotionHandle handle;
        public bool IsCompleted => !handle.IsActive();

        public MotionAwaiter(MotionHandle handle)
        {
            this.handle = handle;
        }

        public MotionAwaiter GetAwaiter()
        {
            return this;
        }

        public void GetResult()
        {
        }

        public void OnCompleted(Action continuation)
        {
            UnsafeOnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            if (continuation == null) return;

            ref var callbackData = ref MotionStorageManager.GetMotionCallbackDataRef(handle);
            callbackData.OnCompleteAction += continuation;
            callbackData.OnCancelAction += continuation;
        }
    }
}