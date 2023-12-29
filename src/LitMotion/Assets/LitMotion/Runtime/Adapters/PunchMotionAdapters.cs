using Unity.Jobs;
using UnityEngine;
using LitMotion;
using LitMotion.Adapters;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<float, PunchOptions, FloatPunchMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector2, PunchOptions, Vector2PunchMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, PunchOptions, Vector3PunchMotionAdapter>))]

namespace LitMotion.Adapters
{
    // Note: Punch motion uses startValue as offset and endValue as vibration strength.

    public readonly struct FloatPunchMotionAdapter : IMotionAdapter<float, PunchOptions>
    {
        public float Evaluate(in float startValue, in float endValue, in PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    public readonly struct Vector2PunchMotionAdapter : IMotionAdapter<Vector2, PunchOptions>
    {
        public Vector2 Evaluate(in Vector2 startValue, in Vector2 endValue, in PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    public readonly struct Vector3PunchMotionAdapter : IMotionAdapter<Vector3, PunchOptions>
    {
        public Vector3 Evaluate(in Vector3 startValue, in Vector3 endValue, in PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }
}