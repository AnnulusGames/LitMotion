using System;
using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Rotation")]
    public sealed class TransformRotationComponent : SequenceComponentBase<Vector3, Transform>
    {
        static readonly Type componentType = typeof(TransformRotationComponent);

        [Header("Transform Settings")]
        public TransformScalingMode scalingMode;
        public bool useEulerAngles;

        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Rotation";
            scalingMode = default;
            useEulerAngles = default;
        }

        public override void Configure(ISequencePropertyTable sequencePropertyTable, MotionSequenceItemBuilder builder)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            if (!sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Quaternion>((target, componentType, TransformScalingMode.Local), out var initialLocalRotation))
            {
                initialLocalRotation = target.localRotation;
                sequencePropertyTable.SetInitialValue((target, componentType, TransformScalingMode.Local), initialLocalRotation);
            }
            if (!sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Quaternion>((target, componentType, TransformScalingMode.World), out var initialRotation))
            {
                initialRotation = target.rotation;
                sequencePropertyTable.SetInitialValue((target, componentType, TransformScalingMode.World), initialRotation);
            }

            var currentValue = Quaternion.identity;

            switch (MotionMode)
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
                        TransformScalingMode.Local => target.localRotation,
                        TransformScalingMode.World => target.rotation,
                        _ => default
                    };
                    break;
            }

            if (useEulerAngles)
            {
                var motionBuilder = LMotion.Create(currentValue.eulerAngles + StartValue, currentValue.eulerAngles + EndValue, Duration);
                ConfigureMotionBuilder(ref motionBuilder);

                var handle = scalingMode switch
                {
                    TransformScalingMode.Local => motionBuilder.BindToLocalEulerAngles(target),
                    TransformScalingMode.World => motionBuilder.BindToEulerAngles(target),
                    _ => default
                };
                builder.Add(handle);
            }
            else
            {
                var motionBuilder = LMotion.Create(currentValue * Quaternion.Euler(StartValue), currentValue * Quaternion.Euler(EndValue), Duration);
                ConfigureMotionBuilder(ref motionBuilder);

                var handle = scalingMode switch
                {
                    TransformScalingMode.Local => motionBuilder.BindToLocalRotation(target),
                    TransformScalingMode.World => motionBuilder.BindToRotation(target),
                    _ => default
                };
                builder.Add(handle);
            }

        }

        public override void RestoreValues(ISequencePropertyTable sequencePropertyTable)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            if (sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Quaternion>((target, componentType, TransformScalingMode.Local), out var initialLocalRotation))
            {
                target.localRotation = initialLocalRotation;
            }
            if (sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Quaternion>((target, componentType, TransformScalingMode.World), out var initialRotation))
            {
                target.rotation = initialRotation;
            }
        }
    }
}
