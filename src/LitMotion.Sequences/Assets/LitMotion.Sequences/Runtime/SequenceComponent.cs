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
                Component.InternalConfigure(SequencePropertyTable, builder);
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

        internal void InternalConfigure(ISequencePropertyTable propertyTable, MotionSequenceItemBuilder builder)
        {
            if (!enabled) return;
            Configure(propertyTable, builder);
        }

        internal IMotionSequenceItem CreateSequenceItem(ISequencePropertyTable propertyTable)
        {
            return new SequenceItem()
            {
                Component = this,
                SequencePropertyTable = propertyTable
            };
        }

        public abstract void Configure(ISequencePropertyTable propertyTable, MotionSequenceItemBuilder builder);
        public abstract void RestoreValues(ISequencePropertyTable propertyTable);
    }
}