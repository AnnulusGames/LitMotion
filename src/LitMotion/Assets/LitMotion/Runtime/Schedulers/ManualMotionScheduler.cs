namespace LitMotion
{
    internal sealed class ManualMotionScheduler : IMotionScheduler
    {
        public double Time => ManualMotionDispatcher.Time;

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return ManualMotionDispatcher.Schedule(ref builder);
        }
    }
}