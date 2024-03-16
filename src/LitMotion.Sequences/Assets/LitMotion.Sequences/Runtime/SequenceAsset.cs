using System.Collections.Generic;
using UnityEngine;

namespace LitMotion.Sequences
{
    [CreateAssetMenu(fileName = "SequenceAsset", menuName = "LitMotion/Sequence Asset")]
    public sealed class SequenceAsset : ScriptableObject
    {
        internal enum PlayMode
        {
            Sequential,
            Parallel
        }

        sealed class SequenceGroupItem : IMotionSequenceItem
        {
            public SequenceAsset Asset { get; set; }
            public ISequencePropertyTable SequencePropertyTable { get; set; }

            public void Configure(MotionSequenceItemBuilder builder)
            {
                foreach (var component in Asset.components)
                {
                    component.InternalConfigure(SequencePropertyTable, builder);
                }
            }
        }

        [SerializeField] PlayMode playMode;
        [SerializeField] SequenceComponent[] components;
        public IReadOnlyCollection<SequenceComponent> Components => components;

        internal MotionSequence CreateSequence(ISequencePropertyTable sequencePropertyTable)
        {
            var builder = MotionSequence.CreateBuilder();

            switch (playMode)
            {
                case PlayMode.Sequential:
                    foreach (var component in components)
                    {
                        builder.Items.Add(component.CreateSequenceItem(sequencePropertyTable));
                    }
                    break;
                case PlayMode.Parallel:
                    builder.Items.Add(new SequenceGroupItem()
                    {
                        Asset = this,
                        SequencePropertyTable = sequencePropertyTable
                    });
                    break;
            }

            return builder.Build();
        }
    }
}
