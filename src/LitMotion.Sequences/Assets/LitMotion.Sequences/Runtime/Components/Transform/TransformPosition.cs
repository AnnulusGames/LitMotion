using System;
using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Position")]
    public sealed class TransformPosition : SequenceComponentBase<Vector3, Transform>
    {
        public TransformScalingMode scalingMode;

        Transform transform;

        public override void Configure(MotionSequenceItemBuilder builder)
        {
            if (transform == null) return;

            var currentValue = Vector3.zero;
            if (useRelativeValue)
            {
                currentValue = scalingMode switch
                {
                    TransformScalingMode.Local => transform.localPosition,
                    TransformScalingMode.World => transform.position,
                    _ => default
                };
            }

            var motionBuilder = LMotion.Create(currentValue + startValue, currentValue + endValue, duration)
                .WithEase(ease)
                .WithDelay(delay, delayType, skipValuesDuringDelay)
                .WithLoops(loops, loopType);

            var handle = scalingMode switch
            {
                TransformScalingMode.Local => motionBuilder.BindToLocalPosition(transform),
                TransformScalingMode.World => motionBuilder.BindToPosition(transform),
                _ => default
            };

            builder.Add(handle);
        }

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            transform = target.Resolve(exposedPropertyTable);
        }
    }
}
