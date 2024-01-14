using UnityEngine;

namespace LitMotion
{
    internal enum UpdateMode
    {
        Update,
        LateUpdate,
        FixedUpdate
    }

    /// <summary>
    /// Motion dispatcher for Runtime.
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
        }

        internal static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData, UpdateMode updateMode)
           where TValue : unmanaged
           where TOptions : unmanaged, IMotionOptions
           where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            MotionStorage<TValue, TOptions, TAdapter> storage = StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(updateMode);
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
            Instance.hideFlags = HideFlags.HideInHierarchy;
        }

        void Update()
        {
            var array = updateRunners.AsArray();
            for (int i = 0; i < array.Length; i++) array[i]?.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        void LateUpdate()
        {
            var array = lateUpdateRunners.AsArray();
            for (int i = 0; i < array.Length; i++) array[i]?.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        void FixedUpdate()
        {
            var array = fixedUpdateRunners.AsArray();
            for (int i = 0; i < array.Length; i++) array[i]?.Update(Time.fixedDeltaTime, Time.fixedUnscaledDeltaTime);
        }

        void OnDestroy()
        {
            ResetAll(updateRunners);
            ResetAll(lateUpdateRunners);
            ResetAll(fixedUpdateRunners);
        }

        void ResetAll(MinimumList<IUpdateRunner> list)
        {
            var array = list.AsArray();
            for (int i = 0; i < array.Length; i++)
            {
                array[i]?.Reset();
            }
        }
    }
}