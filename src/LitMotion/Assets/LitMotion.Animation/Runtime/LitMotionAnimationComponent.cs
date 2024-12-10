using System;
using UnityEngine;

namespace LitMotion.Animation
{
    [Serializable]
    public abstract class LitMotionAnimationComponent
    {
        public LitMotionAnimationComponent()
        {
            displayName = GetType().Name;
        }

        [SerializeField] string displayName;
        [SerializeField] bool enabled = true;

        public bool Enabled => enabled;
        public string DisplayName => displayName;

        public abstract MotionHandle Play();
    }
}