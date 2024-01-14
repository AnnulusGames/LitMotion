using UnityEditor;

namespace LitMotion.Editor
{
    /// <summary>
    /// Motion dispatcher for Editor.
    /// </summary>
    public static class EditorMotionDispatcher
    {
        static class StorageCache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static MotionStorage<TValue, TOptions, TAdapter> update;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreate()
            {
                if (update == null)
                {
                    var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                    MotionStorageManager.AddStorage(storage);
                    update = storage;
                }
                return update;
            }
        }

        static class RunnerCache<TValue, TOptions, TAdapter>
          where TValue : unmanaged
          where TOptions : unmanaged, IMotionOptions
          where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static UpdateRunner<TValue, TOptions, TAdapter> update;
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
            StorageCache<TValue, TOptions, TAdapter>.GetOrCreate().EnsureCapacity(capacity);
        }

        internal static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
           where TValue : unmanaged
           where TOptions : unmanaged, IMotionOptions
           where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            MotionStorage<TValue, TOptions, TAdapter> storage = StorageCache<TValue, TOptions, TAdapter>.GetOrCreate();
            if (RunnerCache<TValue, TOptions, TAdapter>.update == null)
            {
                var runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                updateRunners.Add(runner);
                RunnerCache<TValue, TOptions, TAdapter>.update = runner;
            }

            var (EntryIndex, Version) = storage.Append(data, callbackData);
            return new MotionHandle()
            {
                StorageId = storage.StorageId,
                Index = EntryIndex,
                Version = Version
            };
        }

        static double lastEditorTime;

        [InitializeOnLoadMethod]
        static void InitEditor()
        {
            lastEditorTime = 0f;
            EditorApplication.update += Update;
        }

        static void Update()
        {
            var deltaTime = (float)(EditorApplication.timeSinceStartup - lastEditorTime);
            var array = updateRunners.AsArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i]?.Update(deltaTime, deltaTime);
            }
            lastEditorTime = EditorApplication.timeSinceStartup;
        }
    }
}