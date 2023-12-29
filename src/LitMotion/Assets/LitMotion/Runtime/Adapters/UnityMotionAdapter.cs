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
        public Vector2 Evaluate(ref Vector2 startValue, ref Vector2 endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            return Vector2.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct Vector3MotionAdapter : IMotionAdapter<Vector3, NoOptions>
    {
        public Vector3 Evaluate(ref Vector3 startValue, ref Vector3 endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            return Vector3.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct Vector4MotionAdapter : IMotionAdapter<Vector4, NoOptions>
    {
        public Vector4 Evaluate(ref Vector4 startValue, ref Vector4 endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            return Vector4.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct QuaternionMotionAdapter : IMotionAdapter<Quaternion, NoOptions>
    {
        public Quaternion Evaluate(ref Quaternion startValue, ref Quaternion endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            return Quaternion.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct ColorMotionAdapter : IMotionAdapter<Color, NoOptions>
    {
        public Color Evaluate(ref Color startValue, ref Color endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            return Color.LerpUnclamped(startValue, endValue, context.Progress);
        }
    }

    public readonly struct RectMotionAdapter : IMotionAdapter<Rect, NoOptions>
    {
        public Rect Evaluate(ref Rect startValue, ref Rect endValue, ref NoOptions options, in MotionEvaluationContext context)
        {
            var x = math.lerp(startValue.x, endValue.x, context.Progress);
            var y = math.lerp(startValue.y, endValue.y, context.Progress);
            var width = math.lerp(startValue.width, endValue.width, context.Progress);
            var height = math.lerp(startValue.height, endValue.height, context.Progress);

            return new Rect(x, y, width, height);
        }
    }
}