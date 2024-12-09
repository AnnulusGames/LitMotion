using System;

namespace LitMotion.Animation
{
    [Serializable]
    public abstract class LitMotionAnimationComponent
    {
        public abstract MotionHandle Play();
    }
}