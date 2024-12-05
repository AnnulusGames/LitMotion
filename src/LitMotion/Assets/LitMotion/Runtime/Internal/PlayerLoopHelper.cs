using System;
using System.Linq;
using UnityEngine;
using UnityEngine.LowLevel;
using PlayerLoopType = UnityEngine.PlayerLoop;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    /// <summary>
    /// Types of PlayerLoop inserted for motion updates
    /// </summary>
    public static class LitMotionLoopRunners
    {
        public struct LitMotionInitialization { };
        public struct LitMotionEarlyUpdate { };
        public struct LitMotionFixedUpdate { };
        public struct LitMotionPreUpdate { };
        public struct LitMotionUpdate { };
        public struct LitMotionPreLateUpdate { };
        public struct LitMotionPostLateUpdate { };
        public struct LitMotionTimeUpdate { };
    }

    internal enum PlayerLoopTiming
    {
        Initialization = 0,
        EarlyUpdate = 1,
        FixedUpdate = 2,
        PreUpdate = 3,
        Update = 4,
        PreLateUpdate = 5,
        PostLateUpdate = 6,
        TimeUpdate = 7,
    }

    internal static class PlayerLoopHelper
    {
        public static event Action OnInitialization;
        public static event Action OnEarlyUpdate;
        public static event Action OnFixedUpdate;
        public static event Action OnPreUpdate;
        public static event Action OnUpdate;
        public static event Action OnPreLateUpdate;
        public static event Action OnPostLateUpdate;
        public static event Action OnTimeUpdate;

        static bool initialized;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
#if UNITY_EDITOR
            var domainReloadDisabled = EditorSettings.enterPlayModeOptionsEnabled && EditorSettings.enterPlayModeOptions.HasFlag(EnterPlayModeOptions.DisableDomainReload);
            if (!domainReloadDisabled && initialized) return;
#else
            if (initialized) return;
#endif

            if (!initialized)
            {
                OnInitialization += static () => MotionDispatcher.Update(PlayerLoopTiming.Initialization);
                OnEarlyUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.EarlyUpdate);
                OnFixedUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.FixedUpdate);
                OnPreUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.PreUpdate);
                OnUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.Update);
                OnPreLateUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.PreLateUpdate);
                OnPostLateUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.PostLateUpdate);
                OnTimeUpdate += static () => MotionDispatcher.Update(PlayerLoopTiming.TimeUpdate);
            }

            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            Initialize(ref playerLoop);
        }

        public static void Initialize(ref PlayerLoopSystem playerLoop)
        {
            initialized = true;
            var newLoop = playerLoop.subSystemList.ToArray();

            InsertLoop(newLoop, typeof(PlayerLoopType.Initialization), typeof(LitMotionLoopRunners.LitMotionInitialization), static () => OnInitialization?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.EarlyUpdate), typeof(LitMotionLoopRunners.LitMotionEarlyUpdate), static () => OnEarlyUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.FixedUpdate), typeof(LitMotionLoopRunners.LitMotionFixedUpdate), static () => OnFixedUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.PreUpdate), typeof(LitMotionLoopRunners.LitMotionPreUpdate), static () => OnPreUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.Update), typeof(LitMotionLoopRunners.LitMotionUpdate), static () => OnUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.PreLateUpdate), typeof(LitMotionLoopRunners.LitMotionPreLateUpdate), static () => OnPreLateUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.PostLateUpdate), typeof(LitMotionLoopRunners.LitMotionPostLateUpdate), static () => OnPostLateUpdate?.Invoke());
            InsertLoop(newLoop, typeof(PlayerLoopType.TimeUpdate), typeof(LitMotionLoopRunners.LitMotionTimeUpdate), static () => OnTimeUpdate?.Invoke());

            playerLoop.subSystemList = newLoop;
            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        static void InsertLoop(PlayerLoopSystem[] loopSystems, Type loopType, Type loopRunnerType, PlayerLoopSystem.UpdateFunction updateDelegate)
        {
            var i = FindLoopSystemIndex(loopSystems, loopType);
            ref var loop = ref loopSystems[i];
            loop.subSystemList = InsertRunner(loop.subSystemList, loopRunnerType, updateDelegate);
        }

        static int FindLoopSystemIndex(PlayerLoopSystem[] playerLoopList, Type systemType)
        {
            for (int i = 0; i < playerLoopList.Length; i++)
            {
                if (playerLoopList[i].type == systemType)
                {
                    return i;
                }
            }

            throw new Exception("Target PlayerLoopSystem does not found. Type:" + systemType.FullName);
        }

        static PlayerLoopSystem[] InsertRunner(PlayerLoopSystem[] subSystemList, Type loopRunnerType, PlayerLoopSystem.UpdateFunction updateDelegate)
        {
            var source = subSystemList.Where(x => x.type != loopRunnerType).ToArray();
            var dest = new PlayerLoopSystem[source.Length + 1];

            Array.Copy(source, 0, dest, 1, source.Length);

            dest[0] = new PlayerLoopSystem
            {
                type = loopRunnerType,
                updateDelegate = updateDelegate
            };

            return dest;
        }
    }
}