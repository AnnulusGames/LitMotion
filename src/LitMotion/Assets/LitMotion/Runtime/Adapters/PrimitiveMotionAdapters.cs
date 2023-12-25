using Unity.Jobs;
using Unity.Mathematics;
using LitMotion;
using LitMotion.Adapters;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<float, NoOptions, FloatMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<double, NoOptions, DoubleMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<int, IntegerOptions, IntMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<long, IntegerOptions, LongMotionAdapter>))]

namespace LitMotion.Adapters
{
    public readonly struct FloatMotionAdapter : IMotionAdapter<float, NoOptions>
    {
        public float Evaluate(in float startValue, in float endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return math.lerp(startValue, endValue, context.Progress);
        }
    }

    public readonly struct DoubleMotionAdapter : IMotionAdapter<double, NoOptions>
    {
        public double Evaluate(in double startValue, in double endValue, in NoOptions options, in MotionEvaluationContext context)
        {
            return math.lerp(startValue, endValue, context.Progress);
        }
    }

    public readonly struct IntMotionAdapter : IMotionAdapter<int, IntegerOptions>
    {
        public int Evaluate(in int startValue, in int endValue, in IntegerOptions options, in MotionEvaluationContext context)
        {
            var value = math.lerp(startValue, endValue, context.Progress);

            return options.RoundingMode switch
            {
                RoundingMode.AwayFromZero => value >= 0f ? (int)math.ceil(value) : (int)math.floor(value),
                RoundingMode.ToZero => (int)math.trunc(value),
                RoundingMode.ToPositiveInfinity => (int)math.ceil(value),
                RoundingMode.ToNegativeInfinity => (int)math.floor(value),
                _ => (int)math.round(value),
            };
        }
    }
    public readonly struct LongMotionAdapter : IMotionAdapter<long, IntegerOptions>
    {
        public long Evaluate(in long startValue, in long endValue, in IntegerOptions options, in MotionEvaluationContext context)
        {
            var value = math.lerp((double)startValue, endValue, context.Progress);

            return options.RoundingMode switch
            {
                RoundingMode.AwayFromZero => value >= 0f ? (long)math.ceil(value) : (long)math.floor(value),
                RoundingMode.ToZero => (long)math.trunc(value),
                RoundingMode.ToPositiveInfinity => (long)math.ceil(value),
                RoundingMode.ToNegativeInfinity => (long)math.floor(value),
                _ => (long)math.round(value),
            };
        }
    }
}