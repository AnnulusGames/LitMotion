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