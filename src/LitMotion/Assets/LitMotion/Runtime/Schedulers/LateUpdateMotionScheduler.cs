namespace LitMotion
{
    internal sealed class LateUpdateMotionScheduler : IMotionScheduler
    {
        public MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, UpdateMode.LateUpdate);
        }
    }
}