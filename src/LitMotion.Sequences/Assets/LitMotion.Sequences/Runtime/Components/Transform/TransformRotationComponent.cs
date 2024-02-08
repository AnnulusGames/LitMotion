using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Rotation")]
    public sealed class TransformRotationComponent : SequenceComponentBase<Vector3, Transform>
    {
        [Header("Transform Settings")]
        public TransformScalingMode scalingMode;
        public bool useEulerAngles;

        Quaternion initialRotation;
        Quaternion initialLocalRotation;

        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Rotation";
            scalingMode = default;
            useEulerAngles = default;
        }

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            base.ResolveExposedReferences(exposedPropertyTable);
            initialRotation = Target.rotation;
            initialLocalRotation = Target.localRotation;
        }

        public override void Configure(MotionSequenceItemBuilder builder)
        {
            if (Target == null) return;

            var currentValue = Quaternion.identity;

            switch (motionMode)
            {
                case MotionMode.Relative:
                    currentValue = scalingMode switch
                    {
                        TransformScalingMode.Local => initialLocalRotation,
                        TransformScalingMode.World => initialRotation,
                        _ => default
                    };
                    break;
                case MotionMode.Additive:
                    currentValue = scalingMode switch
                    {
                        TransformScalingMode.Local => Target.localRotation,
                        TransformScalingMode.World => Target.rotation,
                        _ => default
                    };
                    break;
            }

            if (useEulerAngles)
            {
                var motionBuilder = LMotion.Create(currentValue.eulerAngles + startValue, currentValue.eulerAngles + endValue, duration);
                ConfigureMotionBuilder(ref motionBuilder);

                var handle = scalingMode switch
                {
                    TransformScalingMode.Local => motionBuilder.BindToLocalEulerAngles(Target),
                    TransformScalingMode.World => motionBuilder.BindToEulerAngles(Target),
                    _ => default
                };

                builder.Add(handle);
            }
            else
            {
                var motionBuilder = LMotion.Create(currentValue * Quaternion.Euler(startValue), currentValue * Quaternion.Euler(endValue), duration);
                ConfigureMotionBuilder(ref motionBuilder);

                var handle = scalingMode switch
                {
                    TransformScalingMode.Local => motionBuilder.BindToLocalRotation(Target),
                    TransformScalingMode.World => motionBuilder.BindToRotation(Target),
                    _ => default
                };

                builder.Add(handle);
            }
        }
    }
}
