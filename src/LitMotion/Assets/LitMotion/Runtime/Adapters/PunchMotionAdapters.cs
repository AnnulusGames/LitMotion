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
        public float Evaluate(ref float startValue, ref float endValue, ref PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    public readonly struct Vector2PunchMotionAdapter : IMotionAdapter<Vector2, PunchOptions>
    {
        public Vector2 Evaluate(ref Vector2 startValue, ref Vector2 endValue, ref PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }

    public readonly struct Vector3PunchMotionAdapter : IMotionAdapter<Vector3, PunchOptions>
    {
        public Vector3 Evaluate(ref Vector3 startValue, ref Vector3 endValue, ref PunchOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var result);
            return startValue + result;
        }
    }
}