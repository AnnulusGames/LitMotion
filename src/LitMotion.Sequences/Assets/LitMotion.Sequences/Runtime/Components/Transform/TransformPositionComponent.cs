using System;
using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Position")]
    public sealed class TransformPositionComponent : SequenceComponentBase<Vector3, Transform>
    {
        static readonly Type componentType = typeof(TransformPositionComponent);

        [Header("Transform Settings")]
        [SerializeField] TransformScalingMode scalingMode;

        public override void ResetComponent()
        {
            base.ResetComponent();
            scalingMode = default;
        }

        protected override string GetDefaultDisplayName()
        {
            return "Position";
        }

        public override void Configure(ISequencePropertyTable sequencePropertyTable, SequenceItemBuilder builder)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            if (!sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Vector3>((target, componentType, TransformScalingMode.Local), out var initialLocalPosition))
            {
                initialLocalPosition = target.localPosition;
                sequencePropertyTable.SetInitialValue((target, componentType, TransformScalingMode.Local), initialLocalPosition);
            }
            if (!sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Vector3>((target, componentType, TransformScalingMode.World), out var initialPosition))
            {
                initialPosition = target.position;
                sequencePropertyTable.SetInitialValue((target, componentType, TransformScalingMode.World), initialPosition);
            }

            var currentValue = Vector3.zero;

            switch (MotionMode)
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
                        TransformScalingMode.Local => target.localPosition,
                        TransformScalingMode.World => target.position,
                        _ => default
                    };
                    break;
            }

            var motionBuilder = LMotion.Create(currentValue + StartValue, currentValue + EndValue, Duration);
            ConfigureMotionBuilder(ref motionBuilder);

            var handle = scalingMode switch
            {
                TransformScalingMode.Local => motionBuilder.BindToLocalPosition(target),
                TransformScalingMode.World => motionBuilder.BindToPosition(target),
                _ => default
            };

            builder.Add(handle);
        }

        public override void RestoreValues(ISequencePropertyTable sequencePropertyTable)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            if (sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Vector3>((target, componentType, TransformScalingMode.Local), out var initialLocalPosition))
            {
                target.localPosition = initialLocalPosition;
            }
            if (sequencePropertyTable.TryGetInitialValue<(Transform, Type, TransformScalingMode), Vector3>((target, componentType, TransformScalingMode.World), out var initialPosition))
            {
                target.position = initialPosition;
            }
        }
    }
}
