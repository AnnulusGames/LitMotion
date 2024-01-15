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
        readonly TimeKind timeKind;

        internal PlayerLoopMotionScheduler(UpdateMode updateMode, TimeKind timeKind)
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
                        TimeKind.Time => UnityTime.fixedTimeAsDouble,
                        TimeKind.UnscaledTime => UnityTime.fixedUnscaledTimeAsDouble,
                        TimeKind.Realtime => UnityTime.realtimeSinceStartupAsDouble,
                        _ => throw new NotSupportedException("Invalid TimeKind")
                    };
                }

                return timeKind switch
                {
                    TimeKind.Time => UnityTime.timeAsDouble,
                    TimeKind.UnscaledTime => UnityTime.unscaledTimeAsDouble,
                    TimeKind.Realtime => UnityTime.realtimeSinceStartupAsDouble,
                    _ => throw new NotSupportedException("Invalid TimeKind")
                };
            }
        }

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(in MotionData<TValue, TOptions> data, in MotionCallbackData callbackData)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
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