using System;
using UnityEngine;

namespace LitMotion.Sequences
{
    [Serializable]
    public abstract class SequenceComponent : ScriptableObject
    {
        sealed class SequenceItem : IMotionSequenceItem
        {
            public SequenceComponent Component { get; set; }
            public ISequencePropertyTable SequencePropertyTable { get; set; }

            public void Configure(MotionSequenceItemBuilder builder)
            {
                Component.Configure(SequencePropertyTable, builder);
            }
        }

        public bool enabled;
        public string displayName;
        [SerializeField] bool expanded;

        protected void Reset()
        {
            ResetComponent();
        }

        public virtual void ResetComponent()
        {
            enabled = true;
            displayName = GetType().Name;
        }

        internal void InternalConfigure(ISequencePropertyTable sequencePropertyTable, MotionSequenceItemBuilder builder)
        {
            if (!enabled) return;
            Configure(sequencePropertyTable, builder);
        }

        internal IMotionSequenceItem CreateSequenceItem(ISequencePropertyTable sequencePropertyTable)
        {
            return new SequenceItem()
            {
                Component = this,
                SequencePropertyTable = sequencePropertyTable
            };
        }

        public abstract void Configure(ISequencePropertyTable sequencePropertyTable, MotionSequenceItemBuilder builder);
        public abstract void RestoreValues(ISequencePropertyTable sequencePropertyTable);
    }
}