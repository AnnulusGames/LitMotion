using Unity.Jobs;
using UnityEngine;
using LitMotion;
using LitMotion.Adapters;
using Unity.Mathematics;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<float, ShakeOptions, FloatShakeMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector2, ShakeOptions, Vector2ShakeMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, ShakeOptions, Vector3ShakeMotionAdapter>))]

namespace LitMotion.Adapters
{
    // Note: Shake motion uses startValue as offset and endValue as vibration strength.

    public readonly struct FloatShakeMotionAdapter : IMotionAdapter<float, ShakeOptions>
    {
        public float Evaluate(ref float startValue, ref float endValue, ref ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            var multipliar = RandomHelper.NextFloat(options.RandomSeed, context.Time, -1f, 1f);
            return startValue + s * multipliar;
        }
    }

    public readonly struct Vector2ShakeMotionAdapter : IMotionAdapter<Vector2, ShakeOptions>
    {
        public Vector2 Evaluate(ref Vector2 startValue, ref Vector2 endValue, ref ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            var multipliar = RandomHelper.NextFloat2(options.RandomSeed, context.Time, new float2(-1f, -1f), new float2(1f, 1f));
            return startValue + new Vector2(s.x * multipliar.x, s.y * multipliar.y);
        }
    }

    public readonly struct Vector3ShakeMotionAdapter : IMotionAdapter<Vector3, ShakeOptions>
    {
        public Vector3 Evaluate(ref Vector3 startValue, ref Vector3 endValue, ref ShakeOptions options, in MotionEvaluationContext context)
        {
            VibrationHelper.EvaluateStrength(endValue, options.Frequency, options.DampingRatio, context.Progress, out var s);
            var multipliar = RandomHelper.NextFloat3(options.RandomSeed, context.Time, new float3(-1f, -1f, -1f), new float3(1f, 1f, 1f));
            return startValue + new Vector3(s.x * multipliar.x, s.y * multipliar.y, s.z * multipliar.z);
        }
    }
}