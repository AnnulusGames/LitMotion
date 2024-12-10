using System.Collections.Generic;
using LitMotion.Animation.Components;
using LitMotion.Sequences;
using UnityEngine;

namespace LitMotion.Animation
{
    [AddComponentMenu("LitMotion Animation")]
    public sealed class LitMotionAnimation : MonoBehaviour
    {
        [SerializeReference]
        LitMotionAnimationComponent[] components = new LitMotionAnimationComponent[]
        {
            new Position(),
            new Position(),
        };

        public MotionHandle Play()
        {
            var builder = LSequence.Create();

            foreach (var component in components)
            {
                builder.Join(component.Play());
            }

            return builder.Schedule();
        }
    }
}