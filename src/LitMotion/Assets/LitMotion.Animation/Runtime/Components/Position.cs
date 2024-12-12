
using System;
using LitMotion.Extensions;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Transform/Position")]
    public sealed class Position : LitMotionAnimationComponent
    {
        [SerializeField] Transform target;
        [SerializeField] SerializableMotionSettings<Vector3, NoOptions> settings;

        Vector3 startPosition;
        readonly Action revertAction;

        public Position()
        {
            revertAction = Revert;
        }

        void Revert()
        {
            if (target == null) return;
            target.position = startPosition;
        }

        public override MotionHandle Play()
        {
            startPosition = target.position;

            return LMotion.Create(settings)
                .WithOnCancel(revertAction)
                .BindToPosition(target);
        }
    }
}