using System;

namespace LitMotion.Sequences
{
    public sealed class SequenceComponentMenuAttribute : Attribute
    {
        public string MenuName { get; }

        public SequenceComponentMenuAttribute(string menuName)
        {
            this.MenuName = menuName;
        }
    }
}
