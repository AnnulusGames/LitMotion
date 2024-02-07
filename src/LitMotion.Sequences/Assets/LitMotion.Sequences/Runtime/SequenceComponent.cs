using UnityEngine;

namespace LitMotion.Sequences
{
    public abstract class SequenceComponent : ScriptableObject, IMotionSequenceItem
    {
        protected void Reset()
        {
            enabled = true;
            displayName = GetType().Name;
        }

        public bool enabled;
        public string displayName;

        public abstract void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable);
        public abstract void Configure(MotionSequenceItemBuilder builder);
    }
}