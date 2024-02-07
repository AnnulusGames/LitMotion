using System.Collections.Generic;
using UnityEngine;

namespace LitMotion.Sequences
{
    [CreateAssetMenu(fileName = "SequenceAsset", menuName = "LitMotion/Sequence Asset")]
    public sealed class SequenceAsset : ScriptableObject
    {
        [SerializeField] SequenceComponent[] components;
        public IReadOnlyCollection<SequenceComponent> Components => components;

        internal void ResolveExposedPropeties(IExposedPropertyTable exposedPropertyTable)
        {
            foreach (var component in components) component.ResolveExposedReferences(exposedPropertyTable);
        }

        internal MotionSequence CreateSequence()
        {
            var builder = MotionSequence.CreateBuilder();
            foreach (var component in components)
            {
                builder.Items.Add(component);
            }

            return builder.Build();
        }
    }
}
