using Unity.Collections;
using Unity.Jobs;
using LitMotion;
using LitMotion.Adapters;

[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<FixedString32Bytes, StringOptions, FixedString32BytesMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<FixedString64Bytes, StringOptions, FixedString64BytesMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<FixedString128Bytes, StringOptions, FixedString128BytesMotionAdapter>))]
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<FixedString4096Bytes, StringOptions, FixedString4096BytesMotionAdapter>))]

namespace LitMotion.Adapters
{
    public readonly struct FixedString32BytesMotionAdapter : IMotionAdapter<FixedString32Bytes, StringOptions>
    {
        public FixedString32Bytes Evaluate(in FixedString32Bytes startValue, in FixedString32Bytes endValue, in StringOptions options, in MotionEvaluationContext context)
        {
            var start = startValue;
            var end = endValue;
            var customScrambleChars = options.CustomScrambleChars;
            FixedStringHelper.Interpolate(ref start, ref end, context.Progress, options.ScrambleMode, options.RichTextEnabled, ref SharedRandom.Random.Data, ref customScrambleChars, out var result);
            return result;
        }
    }

    public readonly struct FixedString64BytesMotionAdapter : IMotionAdapter<FixedString64Bytes, StringOptions>
    {
        public FixedString64Bytes Evaluate(in FixedString64Bytes startValue, in FixedString64Bytes endValue, in StringOptions options, in MotionEvaluationContext context)
        {
            var start = startValue;
            var end = endValue;
            var customScrambleChars = options.CustomScrambleChars;
            FixedStringHelper.Interpolate(ref start, ref end, context.Progress, options.ScrambleMode, options.RichTextEnabled, ref SharedRandom.Random.Data, ref customScrambleChars, out var result);
            return result;
        }
    }

    public readonly struct FixedString128BytesMotionAdapter : IMotionAdapter<FixedString128Bytes, StringOptions>
    {
        public FixedString128Bytes Evaluate(in FixedString128Bytes startValue, in FixedString128Bytes endValue, in StringOptions options, in MotionEvaluationContext context)
        {
            var start = startValue;
            var end = endValue;
            var customScrambleChars = options.CustomScrambleChars;
            FixedStringHelper.Interpolate(ref start, ref end, context.Progress, options.ScrambleMode, options.RichTextEnabled, ref SharedRandom.Random.Data, ref customScrambleChars, out var result);
            return result;
        }
    }

    public readonly struct FixedString512BytesMotionAdapter : IMotionAdapter<FixedString512Bytes, StringOptions>
    {
        public FixedString512Bytes Evaluate(in FixedString512Bytes startValue, in FixedString512Bytes endValue, in StringOptions options, in MotionEvaluationContext context)
        {
            var start = startValue;
            var end = endValue;
            var customScrambleChars = options.CustomScrambleChars;
            FixedStringHelper.Interpolate(ref start, ref end, context.Progress, options.ScrambleMode, options.RichTextEnabled, ref SharedRandom.Random.Data, ref customScrambleChars, out var result);
            return result;
        }
    }

    public readonly struct FixedString4096BytesMotionAdapter : IMotionAdapter<FixedString4096Bytes, StringOptions>
    {
        public FixedString4096Bytes Evaluate(in FixedString4096Bytes startValue, in FixedString4096Bytes endValue, in StringOptions options, in MotionEvaluationContext context)
        {
            var start = startValue;
            var end = endValue;
            var customScrambleChars = options.CustomScrambleChars;
            FixedStringHelper.Interpolate(ref start, ref end, context.Progress, options.ScrambleMode, options.RichTextEnabled, ref SharedRandom.Random.Data, ref customScrambleChars, out var result);
            return result;
        }
    }
}