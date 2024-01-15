namespace LitMotion
{
    internal sealed class ManualMotionScheduler : IMotionScheduler
    {
        public double Time => ManualMotionDispatcher.Time;

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return ManualMotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData);
        }
    }
}