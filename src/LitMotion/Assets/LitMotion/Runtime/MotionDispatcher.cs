using System;
using System.Runtime.CompilerServices;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    /// <summary>
    /// Motion dispatcher.
    /// </summary>
    public static class MotionDispatcher
    {
        static class StorageCache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static MotionStorage<TValue, TOptions, TAdapter> initialization;
            public static MotionStorage<TValue, TOptions, TAdapter> earlyUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> fixedUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> preUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> update;
            public static MotionStorage<TValue, TOptions, TAdapter> preLateUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> postLateUpdate;
            public static MotionStorage<TValue, TOptions, TAdapter> timeUpdate;

            public static MotionStorage<TValue, TOptions, TAdapter> GetOrCreate(PlayerLoopTiming playerLoopTiming)
            {
                return playerLoopTiming switch
                {
                    PlayerLoopTiming.Initialization => CreateIfNull(ref initialization),
                    PlayerLoopTiming.EarlyUpdate => CreateIfNull(ref earlyUpdate),
                    PlayerLoopTiming.FixedUpdate => CreateIfNull(ref fixedUpdate),
                    PlayerLoopTiming.PreUpdate => CreateIfNull(ref preUpdate),
                    PlayerLoopTiming.Update => CreateIfNull(ref update),
                    PlayerLoopTiming.PreLateUpdate => CreateIfNull(ref preLateUpdate),
                    PlayerLoopTiming.PostLateUpdate => CreateIfNull(ref postLateUpdate),
                    PlayerLoopTiming.TimeUpdate => CreateIfNull(ref timeUpdate),
                    _ => null,
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static MotionStorage<TValue, TOptions, TAdapter> CreateIfNull(ref MotionStorage<TValue, TOptions, TAdapter> storage)
            {
                if (storage == null)
                {
                    storage = new MotionStorage<TValue, TOptions, TAdapter>(MotionStorageManager.CurrentStorageId);
                    MotionStorageManager.AddStorage(storage);
                }
                return storage;
            }
        }

        static class RunnerCache<TValue, TOptions, TAdapter>
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            public static UpdateRunner<TValue, TOptions, TAdapter> initialization;
            public static UpdateRunner<TValue, TOptions, TAdapter> earlyUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> fixedUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> preUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> update;
            public static UpdateRunner<TValue, TOptions, TAdapter> preLateUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> postLateUpdate;
            public static UpdateRunner<TValue, TOptions, TAdapter> timeUpdate;

            public static (UpdateRunner<TValue, TOptions, TAdapter> runner, bool isCreated) GetOrCreate(PlayerLoopTiming playerLoopTiming, MotionStorage<TValue, TOptions, TAdapter> storage)
            {
                return playerLoopTiming switch
                {
                    PlayerLoopTiming.Initialization => CreateIfNull(ref initialization, storage),
                    PlayerLoopTiming.EarlyUpdate => CreateIfNull(ref earlyUpdate, storage),
                    PlayerLoopTiming.FixedUpdate => CreateIfNull(ref fixedUpdate, storage),
                    PlayerLoopTiming.PreUpdate => CreateIfNull(ref preUpdate, storage),
                    PlayerLoopTiming.Update => CreateIfNull(ref update, storage),
                    PlayerLoopTiming.PreLateUpdate => CreateIfNull(ref preLateUpdate, storage),
                    PlayerLoopTiming.PostLateUpdate => CreateIfNull(ref postLateUpdate, storage),
                    PlayerLoopTiming.TimeUpdate => CreateIfNull(ref timeUpdate, storage),
                    _ => default,
                };
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static (UpdateRunner<TValue, TOptions, TAdapter>, bool) CreateIfNull(ref UpdateRunner<TValue, TOptions, TAdapter> runner, MotionStorage<TValue, TOptions, TAdapter> storage)
            {
                if (runner == null)
                {
                    runner = new UpdateRunner<TValue, TOptions, TAdapter>(storage);
                    updateRunners.Add(runner);
                    return (runner, true);
                }
                return (runner, false);
            }
        }

        static readonly MinimumList<IUpdateRunner> initializationRunners = new();
        static readonly MinimumList<IUpdateRunner> earlyUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> fixedUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> preUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> updateRunners = new();
        static readonly MinimumList<IUpdateRunner> preLateUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> postLateUpdateRunners = new();
        static readonly MinimumList<IUpdateRunner> timeUpdateRunners = new();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static MinimumList<IUpdateRunner> GetRunnerList(PlayerLoopTiming playerLoopTiming)
        {
            return playerLoopTiming switch
            {
                PlayerLoopTiming.Initialization => initializationRunners,
                PlayerLoopTiming.EarlyUpdate => earlyUpdateRunners,
                PlayerLoopTiming.FixedUpdate => fixedUpdateRunners,
                PlayerLoopTiming.PreUpdate => preUpdateRunners,
                PlayerLoopTiming.Update => updateRunners,
                PlayerLoopTiming.PreLateUpdate => preLateUpdateRunners,
                PlayerLoopTiming.PostLateUpdate => postLateUpdateRunners,
                PlayerLoopTiming.TimeUpdate => timeUpdateRunners,
                _ => null
            };
        }

        static readonly PlayerLoopTiming[] playerLoopTimings = (PlayerLoopTiming[])Enum.GetValues(typeof(PlayerLoopTiming));

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            foreach (var playerLoopTiming in playerLoopTimings)
            {
                var span = GetRunnerList(playerLoopTiming).AsSpan();
                for (int i = 0; i < span.Length; i++)
                {
                    span[i].Reset();
                }
            }
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
            foreach (var playerLoopTiming in playerLoopTimings)
            {
                StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(playerLoopTiming).EnsureCapacity(capacity);
            }
#if UNITY_EDITOR
            EditorMotionDispatcher.EnsureStorageCapacity<TValue, TOptions, TAdapter>(capacity);
#endif
        }

        internal static MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData, PlayerLoopTiming playerLoopTiming)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var storage = StorageCache<TValue, TOptions, TAdapter>.GetOrCreate(playerLoopTiming);
            var (runner, isCreated) = RunnerCache<TValue, TOptions, TAdapter>.GetOrCreate(playerLoopTiming, storage);
            if (isCreated) GetRunnerList(playerLoopTiming).Add(runner);

            var (EntryIndex, Version) = storage.Append(data, callbackData);
            return new MotionHandle()
            {
                StorageId = storage.StorageId,
                Index = EntryIndex,
                Version = Version
            };
        }

        internal static void Update(PlayerLoopTiming playerLoopTiming)
        {
            var span = GetRunnerList(playerLoopTiming).AsSpan();
            if (playerLoopTiming == PlayerLoopTiming.FixedUpdate)
            {
                for (int i = 0; i < span.Length; i++) span[i].Update(Time.fixedTimeAsDouble, Time.fixedUnscaledTimeAsDouble, Time.realtimeSinceStartupAsDouble);
            }
            else
            {
                for (int i = 0; i < span.Length; i++) span[i].Update(Time.timeAsDouble, Time.unscaledTimeAsDouble, Time.realtimeSinceStartupAsDouble);
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