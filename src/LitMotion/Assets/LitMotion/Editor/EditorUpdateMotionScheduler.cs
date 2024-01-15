using UnityEditor;

namespace LitMotion.Editor
{
    internal sealed class EditorUpdateMotionScheduler : IMotionScheduler
    {
        public double Time => EditorApplication.timeSinceStartup;

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return EditorMotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData);
        }
    }
}