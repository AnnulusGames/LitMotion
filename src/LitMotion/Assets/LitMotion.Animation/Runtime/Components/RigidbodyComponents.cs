#if LITMOTION_ANIMATION_PHYSICS

using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    public abstract class RigidbodyPositionAnimationBase<TOptions, TAdapter> : PropertyAnimationComponent<Rigidbody, Vector3, TOptions, TAdapter>
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
    {
        [SerializeField] bool useMovePosition = true;

        protected override Vector3 GetValue(Rigidbody target)
        {
            return target.position;
        }

        protected override void SetValue(Rigidbody target, in Vector3 value)
        {
            if (useMovePosition) target.MovePosition(value);
            else target.position = value;
        }

        protected override Vector3 GetRelativeValue(in Vector3 startValue, in Vector3 relativeValue)
        {
            return startValue + relativeValue;
        }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Position")]
    public sealed class RigidbodyPositionAnimation : RigidbodyPositionAnimationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Position (Punch)")]
    public sealed class RigidbodyPositionPunchAnimation : RigidbodyPositionAnimationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Position (Shake)")]
    public sealed class RigidbodyPositionShakeAnimation : RigidbodyPositionAnimationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }

    public abstract class RigidbodyRotationAnimationBase<TOptions, TAdapter> : PropertyAnimationComponent<Rigidbody, Vector3, TOptions, TAdapter>
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
    {
        [SerializeField] bool useMoveRotation;

        protected override Vector3 GetValue(Rigidbody target)
        {
            return target.rotation.eulerAngles;
        }

        protected override void SetValue(Rigidbody target, in Vector3 value)
        {
            if (useMoveRotation) target.MoveRotation(Quaternion.Euler(value));
            else target.rotation = Quaternion.Euler(value);
        }

        protected override Vector3 GetRelativeValue(in Vector3 startValue, in Vector3 relativeValue)
        {
            return startValue + relativeValue;
        }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Rotation")]
    public sealed class RigidbodyRotationAnimation : RigidbodyRotationAnimationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Rotation (Punch)")]
    public sealed class RigidbodyRotationPunchAnimation : RigidbodyRotationAnimationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Rigidbody/Rotation (Shake)")]
    public sealed class RigidbodyRotationShakeAnimation : RigidbodyRotationAnimationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }
}

#endif