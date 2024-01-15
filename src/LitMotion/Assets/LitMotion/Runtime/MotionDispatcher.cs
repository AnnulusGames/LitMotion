using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal enum UpdateMode
    {
        Update = 0,
        LateUpdate = 1,
        FixedUpdate = 2
    }

    /// <summary>
    /// Motion dispatcher.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    [DefaultExecutionOrder(-1000)]
    public sealed class MotionDispatcher : MonoBehaviour
    {
        internal static MotionDispatcher Instance { get; private set; }

        static class StorageCache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static MotionStorage<TValue, TOptions, TAdapter> update;
            public static MotionStorage<TValue, TOptions, TAdapter> lateUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> fixedUpdate;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreate(UpdateMode updateMode)
            {
                switch (updateMode)
                {
                    default: return null;
                    case UpdateMode.Update:
                        if (update == null)
                        {
                            var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                            MotionStorageManager.AddStorage(storage);
                            update = storage;
                        }
                        return update;
                    case UpdateMode.LateUpdate:
                        if (lateUpdate == null)
                        {
                            var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                            MotionStorageManager.AddStorage(storage);
                            lateUpdate = storage;
                        }
                        return lateUpdate;
                    case UpdateMode.FixedUpdate:
                        if (fixedUpdate == null)
                        {
                            var storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                            MotionStorageManager.AddStorage(storage);
                            fixedUpdate = storage;
                        }
                        return fixedUpdate;
                }
            }
        }

        static class RunnerCache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static UpdateRunner<TValue, TOptions, TAdapter> update;
            public static UpdateRunner<TValue, TOptions, TAdapter> lateUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> fixedUpdate;
        }

        static readonly MinimumList<IUpdateRunner> updateRunners = new();
        static readonly MinimumList<IUpdateRunner> lateUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> fixedUpdateRunners = new();
        
        /// <summary>
        /// Ensures the storage capacity until it reaches at least `capacity`.
        /// </summary>
        /// <param name="capacity">The minimum capacity to ensure.</param>
        public static void EnsureStorageCapacity<TValue, TOptions, TAdapter>(int capacity)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(UpdateMode.Update).EnsureCapacity(capacity);
            StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(UpdateMode.LateUpdate).EnsureCapacity(capacity);
            StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(UpdateMode.FixedUpdate).EnsureCapacity(capacity);
#if UNITY_EDITOR
            EditorMotionDispatcher.EnsureStorageCapacity<TValue, TOptions, TAdapter>(capacity);
#endif
        }

        internal static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData, UpdateMode updateMode)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var storage = StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(updateMode);
            switch (updateMode)
            {
                default:
                case UpdateMode.Update:
                    if (RunnerCache<TValue, TOptions, TAdapter>.update == null)
                    {
                        var runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                        updateRunners.Add(runner);
                        RunnerCache<TValue, TOptions, TAdapter>.update = runner;
                    }
                    break;
                case UpdateMode.LateUpdate:
                    if (RunnerCache<TValue, TOptions, TAdapter>.lateUpdate == null)
                    {
                        var runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                        lateUpdateRunners.Add(runner);
                        RunnerCache<TValue, TOptions, TAdapter>.lateUpdate = runner;
                    }
                    break;
                case UpdateMode.FixedUpdate:
                    if (RunnerCache<TValue, TOptions, TAdapter>.fixedUpdate == null)
                    {
                        var runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                        fixedUpdateRunners.Add(runner);
                        RunnerCache<TValue, TOptions, TAdapter>.fixedUpdate = runner;
                    }
                    break;
            }

            var (EntryIndex, Version) = storage.Append(data, callbackData);
            return new MotionHandle()
            {
                StorageId = storage.StorageId,
                Index = EntryIndex,
                Version = Version
            };
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            Instance = new GameObject(nameof(MotionDispatcher)).AddComponent<MotionDispatcher>();
            DontDestroyOnLoad(Instance);
        }

        void Update()
        {
            var span = updateRunners.AsSpan();
            for (int i = 0; i < span.Length; i++) span[i].Update(Time.timeAsDouble, Time.unscaledTimeAsDouble, Time.realtimeSinceStartupAsDouble);
        }

        void LateUpdate()
        {
            var span = lateUpdateRunners.AsSpan();
            for (int i = 0; i < span.Length; i++) span[i].Update(Time.timeAsDouble, Time.unscaledTimeAsDouble, Time.realtimeSinceStartupAsDouble);
        }

        void FixedUpdate()
        {
            var span = fixedUpdateRunners.AsSpan();
            for (int i = 0; i < span.Length; i++) span[i].Update(Time.fixedTimeAsDouble, Time.fixedUnscaledTimeAsDouble, Time.realtimeSinceStartupAsDouble);
        }

        void OnDestroy()
        {
            ResetAll(updateRunners);
            ResetAll(lateUpdateRunners);
            ResetAll(fixedUpdateRunners);
        }

        void ResetAll(MinimumList<IUpdateRunner> list)
        {
            var span = list.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                span[i].Reset();
            }
        }
    }

#if UNITY_EDITOR
    internal static class EditorMotionDispatcher
    {
        static class Cache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            static MotionStorage<TValue, TOptions, TAdapter> storage;
            static UpdateRunner<TValue, TOptions, TAdapter> updateRunner;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreateStorage()
            {
                if (storage == null)
                {
                    storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                    MotionStorageManager.AddStorage(storage);
                }
                return storage;
            }

            public static void InitUpdateRunner()
            {
                if (updateRunner == null)
                {
                    updateRunner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                    updateRunners.Add(updateRunner);
                }
            }
        }
        
        static readonly MinimumList<IUpdateRunner> updateRunners = new();

        public static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var storage = Cache<TValue, TOptions, TAdapter>.GetOrCreateStorage();
            Cache<TValue, TOptions, TAdapter>.InitUpdateRunner();

            var (EntryIndex, Version) = storage.Append(data, callbackData);
            return new MotionHandle()
            {
                StorageId = storage.StorageId,
                Index = EntryIndex,
                Version = Version
            };
        }

        public static void EnsureStorageCapacity<TValue, TOptions, TAdapter>(int capacity)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Cache<TValue, TOptions, TAdapter>.GetOrCreateStorage().EnsureCapacity(capacity);
        }

        [InitializeOnLoadMethod]
        static void Init()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            var span = updateRunners.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                span[i].Update(EditorApplication.timeSinceStartup, EditorApplication.timeSinceStartup, Time.realtimeSinceStartupAsDouble);
            }
        }
    }
#endif
}