
using System;
using LitMotion.Extensions;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Transform/Rotation")]
    public sealed class Rotation : LitMotionAnimationComponent
    {
        [SerializeField] Transform target;
        [SerializeField] SerializableMotionSettings<Vector3, NoOptions> settings;

        Vector3 startRotation;
        readonly Action revertAction;

        public Rotation()
        {
            revertAction = Revert;
        }

        void Revert()
        {
            if (target == null) return;
            target.eulerAngles = startRotation;
        }

        public override MotionHandle Play()
        {
            startRotation = target.eulerAngles;

            return LMotion.Create(settings)
                .WithOnCancel(revertAction)
                .BindToEulerAngles(target);
        }
    }
}