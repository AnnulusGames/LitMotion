namespace LitMotion
{
    internal sealed class ManualMotionScheduler : IMotionScheduler
    {
        public double Time => ManualMotionDispatcher.Time;

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionData<TValue, TOptions> data, ref MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return ManualMotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData);
        }
    }
}