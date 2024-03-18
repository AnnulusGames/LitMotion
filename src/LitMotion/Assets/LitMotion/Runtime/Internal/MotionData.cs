using System.Runtime.InteropServices;
using LitMotion.Collections;

namespace LitMotion
{
    /// <summary>
    /// A structure representing motion data.
    /// </summary>
    /// <typeparam name="TValue">The type of value to animate</typeparam>
    /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public struct MotionData<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        public MotionStatus Status;

        public double Time;
        public float PlaybackSpeed;
        public float Duration;

        public Ease Ease;
        public NativeAnimationCurve AnimationCurve;

        public MotionTimeKind TimeKind;
        public float Delay;
        public int Loops;
        public DelayType DelayType;
        public LoopType LoopType;

        public TValue StartValue;
        public TValue EndValue;
        public TOptions Options;
    }
}