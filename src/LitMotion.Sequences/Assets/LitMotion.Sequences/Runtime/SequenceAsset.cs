using System.Collections.Generic;
using UnityEngine;

namespace LitMotion.Sequences
{
    [CreateAssetMenu(fileName = "SequenceAsset", menuName = "LitMotion/Sequence Asset")]
    public sealed class SequenceAsset : ScriptableObject
    {
        [SerializeField] SequenceComponent[] components;
        public IReadOnlyCollection<SequenceComponent> Components => components;

        internal MotionSequence CreateSequence(ISequencePropertyTable sequencePropertyTable)
        {
            var builder = MotionSequence.CreateBuilder();
            foreach (var component in components)
            {
                builder.Items.Add(component.CreateSequenceItem(sequencePropertyTable));
            }

            return builder.Build();
        }
    }
}
