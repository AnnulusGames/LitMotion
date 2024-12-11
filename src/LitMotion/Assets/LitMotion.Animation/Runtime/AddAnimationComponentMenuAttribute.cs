using System;

namespace LitMotion.Animation
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class AddAnimationComponentMenuAttribute : Attribute
    {
        public AddAnimationComponentMenuAttribute(string menuName)
        {
            MenuName = menuName;
        }

        public string MenuName { get; }
    }
}