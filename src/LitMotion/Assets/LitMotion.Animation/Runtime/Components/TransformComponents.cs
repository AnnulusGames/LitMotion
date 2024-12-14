
using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    public abstract class PositionAnimationBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
    {
        [SerializeField] bool useWorldSpace;

        protected override Vector3 GetValue(Transform target)
        {
            return useWorldSpace ? target.position : target.localPosition;
        }

        protected override void SetValue(Transform target, in Vector3 value)
        {
            if (useWorldSpace) target.position = value;
            else target.localPosition = value;
        }

        protected override Vector3 GetRelativeValue(in Vector3 startValue, in Vector3 relativeValue)
        {
            return startValue + relativeValue;
        }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Position")]
    public sealed class PositionAnimation : PositionAnimationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Position (Punch)")]
    public sealed class PositionPunchAnimation : PositionAnimationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Position (Shake)")]
    public sealed class PositionShakeAnimation : PositionAnimationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }

    public abstract class RotationAnimationBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
    {
        [SerializeField] bool useWorldSpace;

        protected override Vector3 GetValue(Transform target)
        {
            return useWorldSpace ? target.eulerAngles : target.localEulerAngles;
        }

        protected override void SetValue(Transform target, in Vector3 value)
        {
            if (useWorldSpace) target.eulerAngles = value;
            else target.localEulerAngles = value;
        }

        protected override Vector3 GetRelativeValue(in Vector3 startValue, in Vector3 relativeValue)
        {
            return startValue + relativeValue;
        }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Rotation")]
    public sealed class RotationAnimation : RotationAnimationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Rotation (Punch)")]
    public sealed class RotationPunchAnimation : RotationAnimationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Rotation (Shake)")]
    public sealed class RotationShakeAnimation : RotationAnimationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }

    public abstract class ScaleAnimationBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
    {
        protected override Vector3 GetValue(Transform target)
        {
            return target.localScale;
        }

        protected override void SetValue(Transform target, in Vector3 value)
        {
            target.localScale = value;
        }

        protected override Vector3 GetRelativeValue(in Vector3 startValue, in Vector3 relativeValue)
        {
            return startValue + relativeValue;
        }
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Scale")]
    public sealed class ScaleAnimation : ScaleAnimationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Scale (Punch)")]
    public sealed class ScalePunchAnimation : ScaleAnimationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [LitMotionAnimationComponentMenu("Transform/Scale (Shake)")]
    public sealed class ScaleShakeAnimation : ScaleAnimationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }
}