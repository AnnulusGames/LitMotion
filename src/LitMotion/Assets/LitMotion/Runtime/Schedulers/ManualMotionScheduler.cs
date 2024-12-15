namespace LitMotion
{
    internal sealed class ManualMotionScheduler : IMotionScheduler
    {
        static class Cache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static MotionStorage<TValue, TOptions, TAdapter> updateStorage;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreate()
            {
                if (updateStorage == null)
                {
                    var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionManager.MotionTypeCount);
                    MotionManager.Register(storage);
                    updateStorage = storage;
                }
                return updateStorage;
            }
        }

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var storage = Cache<TValue, TOptions, TAdapter>.GetOrCreate();
            return storage.Create(ref builder);
        }
    }
}