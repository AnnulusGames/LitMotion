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
        [SerializeField] bool expanded;

        public abstract void ResolveExposedReferences(IExposedPropertyTable exposedPropertyTable);
        public abstract void Configure(MotionSequenceItemBuilder builder);

        void IMotionSequenceItem.Configure(MotionSequenceItemBuilder builder)
        {
            if (!enabled) return;
            Configure(builder);
        }
    }
}