using UnityEngine;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Group")]
    public sealed class SequenceGroupComponent : SequenceComponent
    {
        [SerializeField] SequenceComponent[] components;

        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Group";
        }

        public override void Configure(ISequencePropertyTable sequencePropertyTable, MotionSequenceItemBuilder builder)
        {
            foreach (var component in components)
            {
                component.Configure(sequencePropertyTable, builder);
            }
        }

        public override void RestoreValues(ISequencePropertyTable sequencePropertyTable)
        {
            foreach (var component in components)
            {
                component.RestoreValues(sequencePropertyTable);
            }
        }
    }
}
