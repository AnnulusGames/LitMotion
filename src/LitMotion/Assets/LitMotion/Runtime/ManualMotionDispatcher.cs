using System;

namespace LitMotion
{
    /// <summary>
    /// Manually updatable MotionDispatcher
    /// </summary>
    public static class ManualMotionDispatcher
    {
        static class Cache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static MotionStorage<TValue, TOptions, TAdapter> updateStorage;
            public static UpdateRunner<TValue, TOptions, TAdapter> updateRunner;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreate()
            {
                if (updateStorage == null)
                {
                    var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                    MotionStorageManager.AddStorage(storage);
                    updateStorage = storage;
                }
                return updateStorage;
            }
        }

        static readonly MinimumList<IUpdateRunner> updateRunners = new();

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

        /// <summary>
        /// Update all scheduled motions with MotionScheduler.Manual
        /// </summary>
        /// <param name="deltaTime">Delta time</param>
        public static void Update(float deltaTime)
        {
            if (deltaTime < 0f) throw new ArgumentException("deltaTime must be 0 or higher.");
            
            var array = updateRunners.AsArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i]?.Update(deltaTime, deltaTime);
            }
        }

        /// <summary>
        /// Cancel all motions and reset data.
        /// </summary>
        public static void Reset()
        {
            var array = updateRunners.AsArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i]?.Reset();
            }
        }

        internal static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
           where TValue : unmanaged
           where TOptions : unmanaged, IMotionOptions
           where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            MotionStorage<TValue, TOptions, TAdapter> storage = Cache<TValue, TOptions, TAdapter>.GetOrCreate();
            if (Cache<TValue, TOptions, TAdapter>.updateRunner == null)
            {
                var runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                updateRunners.Add(runner);
                Cache<TValue, TOptions, TAdapter>.updateRunner = runner;
            }

            var (EntryIndex, Version) = storage.Append(data, callbackData);
            return new MotionHandle()
            {
                StorageId = storage.StorageId,
                Index = EntryIndex,
                Version = Version
            };
        }
    }
}