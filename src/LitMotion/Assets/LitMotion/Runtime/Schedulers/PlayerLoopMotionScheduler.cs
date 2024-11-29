using System;
using UnityTime = UnityEngine.Time;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal sealed class PlayerLoopMotionScheduler : IMotionScheduler
    {
        public readonly PlayerLoopTiming playerLoopTiming;
        public readonly MotionTimeKind timeKind;

        internal PlayerLoopMotionScheduler(PlayerLoopTiming playerLoopTiming, MotionTimeKind timeKind)
        {
            this.playerLoopTiming = playerLoopTiming;
            this.timeKind = timeKind;
        }

        public double Time
        {
            get
            {
                if (playerLoopTiming == PlayerLoopTiming.FixedUpdate)
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

        public MotionHandle Schedule<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            builder.buffer.TimeKind = timeKind;

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                return MotionDispatcher.Schedule(ref builder, playerLoopTiming);
            }
            else
            {
                return EditorMotionDispatcher.Schedule(ref builder);
            }
#else
            return MotionDispatcher.Schedule(ref builder, playerLoopTiming);
#endif
        }
    }
}