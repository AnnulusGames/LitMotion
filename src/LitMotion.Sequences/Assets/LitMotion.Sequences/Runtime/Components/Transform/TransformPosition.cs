using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Position")]
    public sealed class TransformPosition : SequenceComponentBase<Vector3, Transform>
    {
        [Header("Transform Settings")]
        public TransformScalingMode scalingMode;

        Vector3 initialPosition;
        Vector3 initialLocalPosition;

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            base.ResolveExposedReferences(exposedPropertyTable);
            initialPosition = Target.position;
            initialLocalPosition = Target.localPosition;
        }

        public override void Configure(MotionSequenceItemBuilder builder)
        {
            if (Target == null) return;

            var currentValue = Vector3.zero;

            switch (motionMode)
            {
                case MotionMode.Relative:
                    currentValue = scalingMode switch
                    {
                        TransformScalingMode.Local => initialLocalPosition,
                        TransformScalingMode.World => initialPosition,
                        _ => default
                    };
                    break;
                case MotionMode.Additive:
                    currentValue = scalingMode switch
                    {
                        TransformScalingMode.Local => Target.localPosition,
                        TransformScalingMode.World => Target.position,
                        _ => default
                    };
                    break;
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
