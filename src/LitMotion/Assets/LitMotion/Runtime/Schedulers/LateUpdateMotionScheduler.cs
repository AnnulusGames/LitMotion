using UnityEngine;

namespace LitMotion
{
    internal sealed class LateUpdateMotionScheduler : IMotionScheduler
    {
        public MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, UpdateMode.EditorApplicationUpdate);
            }
#endif
            return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, UpdateMode.LateUpdate);
        }
    }
}