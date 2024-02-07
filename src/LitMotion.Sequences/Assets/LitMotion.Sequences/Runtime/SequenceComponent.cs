using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponent : ScriptableObject, IMotionSequenceItem
    {
        public abstract void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable);
        public abstract void Configure(MotionSequenceItemBuilder builder);
    }
}