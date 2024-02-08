using UnityEngine;
using LitMotion.Extensions;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Transform/Scale")]
    public sealed class TransformScaleComponent : SequenceComponentBase<Vector3, Transform>
    {
        Vector3 initialLocalScale;

        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Scale";
        }

        public override void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable)
        {
            base.ResolveExposedReferences(exposedPropertyTable);
            initialLocalScale = Target.localScale;
        }

        public override void Configure(MotionSequenceItemBuilder builder)
        {
            if (Target == null) return;

            var currentValue = Vector3.zero;

            switch (motionMode)
            {
                case MotionMode.Relative:
                    currentValue = initialLocalScale;
                    break;
                case MotionMode.Additive:
                    currentValue = Target.localScale;
                    break;
            }

            var motionBuilder = LMotion.Create(currentValue + startValue, currentValue + endValue, duration);
            ConfigureMotionBuilder(ref motionBuilder);

            var handle = motionBuilder.BindToLocalScale(Target);
            builder.Add(handle);
        }
    }
}
