
using System;
using LitMotion.Extensions;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    public sealed class PositionComponent : LitMotionAnimationComponent
    {
        [SerializeField] Transform target;
        [SerializeField] SerializableMotionSettings<Vector3, NoOptions> settings;

        public override MotionHandle Play()
        {
            return LMotion.Create(settings)
                .BindToPosition(target);
        }
    }
}