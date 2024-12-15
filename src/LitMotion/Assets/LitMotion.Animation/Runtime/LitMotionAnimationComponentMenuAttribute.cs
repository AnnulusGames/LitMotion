using System;

namespace LitMotion.Animation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LitMotionAnimationComponentMenuAttribute : Attribute
    {
        public LitMotionAnimationComponentMenuAttribute(string menuName)
        {
            MenuName = menuName;
        }

        public string MenuName { get; }
    }
}