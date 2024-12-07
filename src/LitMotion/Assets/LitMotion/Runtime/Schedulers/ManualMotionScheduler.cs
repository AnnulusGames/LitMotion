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

        public double Time { get; }

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var storage = Cache<TValue, TOptions, TAdapter>.GetOrCreate();
            return storage.Create(ref builder);
        }

        /// <summary>
        /// Ensures the storage capacity until it reaches at least `capacity`.
        /// </summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        public static void EnsureStorageCapacity<TValue, TOptions, TAdapter>(int capacity)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Cache<TValue, TOptions, TAdapter>.GetOrCreate().EnsureCapacity(capacity);
        }
    }
}