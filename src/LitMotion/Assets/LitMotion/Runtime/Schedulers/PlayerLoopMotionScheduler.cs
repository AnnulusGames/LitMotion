using System;
using UnityTime = UnityEngine.Time;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal sealed class PlayerLoopMotionScheduler : IMotionScheduler
    {
        readonly UpdateMode updateMode;
        readonly MotionTimeKind timeKind;

        internal PlayerLoopMotionScheduler(UpdateMode updateMode, MotionTimeKind timeKind)
        {
            this.updateMode = updateMode;
            this.timeKind = timeKind;
        }

        public double Time
        {
            get
            {
                if (updateMode == UpdateMode.FixedUpdate)
                {
                    return timeKind switch
                    {
                        MotionTimeKind.Time => UnityTime.fixedTimeAsDouble,
                        MotionTimeKind.UnscaledTime => UnityTime.fixedUnscaledTimeAsDouble,
                        MotionTimeKind.Realtime => UnityTime.realtimeSinceStartupAsDouble,
                        _ => throw new NotSupportedException("Invalid TimeKind")
                    };
                }

                return timeKind switch
                {
                    MotionTimeKind.Time => UnityTime.timeAsDouble,
                    MotionTimeKind.UnscaledTime => UnityTime.unscaledTimeAsDouble,
                    MotionTimeKind.Realtime => UnityTime.realtimeSinceStartupAsDouble,
                    _ => throw new NotSupportedException("Invalid TimeKind")
                };
            }
        }

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionData<TValue, TOptions> data, ref MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            data.TimeKind = timeKind;
#if UNITY_EDITOR
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, updateMode);
            }
            else
            {
                return EditorMotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData);
            }
#else
            return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, updateMode);
#endif
        }
    }
}