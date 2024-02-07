using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Position")]
    public sealed class TransformPosition : SequenceComponentBase<Vector3, Transform>
    {
        [Header("Transform Settings")]
        public TransformScalingMode scalingMode;

        public override void Configure(MotionSequenceItemBuilder builder)
        {
            if (Target == null) return;

            var currentValue = Vector3.zero;
            if (useRelativeValue)
            {
                currentValue = scalingMode switch
                {
                    TransformScalingMode.Local => Target.localPosition,
                    TransformScalingMode.World => Target.position,
                    _ => default
                };
            }

            var motionBuilder = LMotion.Create(currentValue + startValue, currentValue + endValue, duration)
                .WithEase(ease)
                .WithDelay(delay, delayType, skipValuesDuringDelay)
                .WithLoops(loops, loopType);

            var handle = scalingMode switch
            {
                TransformScalingMode.Local => motionBuilder.BindToLocalPosition(Target),
                TransformScalingMode.World => motionBuilder.BindToPosition(Target),
                _ => default
            };

            builder.Add(handle);
        }
    }
}
