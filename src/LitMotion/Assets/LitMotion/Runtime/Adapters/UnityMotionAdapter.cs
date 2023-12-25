using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using LitMotion;
using LitMotion.Adapters;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector2, NoOptions, Vector2MotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, NoOptions, Vector3MotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector4, NoOptions, Vector4MotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Quaternion, NoOptions, QuaternionMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Color, NoOptions, ColorMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Rect, NoOptions, RectMotionAdapter>))]

namespace LitMotion.Adapters
{
    public readonly struct Vector2MotionAdapter : IMotionAdapter<Vector2, NoOptions>
    {
        public Vector2 Evaluate(in Vector2 startValue, in Vector2 endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return Vector2.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct Vector3MotionAdapter : IMotionAdapter<Vector3, NoOptions>
    {
        public Vector3 Evaluate(in Vector3 startValue, in Vector3 endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return Vector3.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct Vector4MotionAdapter : IMotionAdapter<Vector4, NoOptions>
    {
        public Vector4 Evaluate(in Vector4 startValue, in Vector4 endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return Vector4.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct QuaternionMotionAdapter : IMotionAdapter<Quaternion, NoOptions>
    {
        public Quaternion Evaluate(in Quaternion startValue, in Quaternion endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return Quaternion.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct ColorMotionAdapter : IMotionAdapter<Color, NoOptions>
    {
        public Color Evaluate(in Color startValue, in Color endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return Color.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct RectMotionAdapter : IMotionAdapter<Rect, NoOptions>
    {
        public Rect Evaluate(in Rect startValue, in Rect endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            var x = math.lerp(startValue.x, endValue.x, context.Progress);
            var y = math.lerp(startValue.y, endValue.y, context.Progress);
            var width = math.lerp(startValue.width, endValue.width, context.Progress);
            var height = math.lerp(startValue.height, endValue.height, context.Progress);

            return new Rect(x, y, width, height);
        }
    }
}