
using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    public abstract class PositionBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
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

        protected override void SetRelativeValue(Transform target, in Vector3 startValue, in Vector3 relativeValue)
        {
            if (useWorldSpace) target.position = startValue + relativeValue;
            else target.localPosition = startValue + relativeValue;
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Position")]
    public sealed class Position : PositionBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Position (Punch)")]
    public sealed class PositionPunch : PositionBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Position (Shake)")]
    public sealed class PositionShake : PositionBase<ShakeOptions, Vector3ShakeMotionAdapter> { }

    public abstract class RotationBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
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

        protected override void SetRelativeValue(Transform target, in Vector3 startValue, in Vector3 relativeValue)
        {
            if (useWorldSpace) target.eulerAngles = startValue + relativeValue;
            else target.localEulerAngles = startValue + relativeValue;
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Rotation")]
    public sealed class Rotation : RotationBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Rotation (Punch)")]
    public sealed class RotationPunch : RotationBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Rotation (Shake)")]
    public sealed class RotationShake : RotationBase<ShakeOptions, Vector3ShakeMotionAdapter> { }

    public abstract class ScaleBase<TOptions, TAdapter> : PropertyAnimationComponent<Transform, Vector3, TOptions, TAdapter>
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

        protected override void SetRelativeValue(Transform target, in Vector3 startValue, in Vector3 relativeValue)
        {
            target.localScale = startValue + relativeValue;
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Scale")]
    public sealed class Scale : ScaleBase<NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Scale (Punch)")]
    public sealed class ScalePunch : ScaleBase<PunchOptions, Vector3PunchMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Transform/Scale (Shake)")]
    public sealed class ScaleShake : ScaleBase<ShakeOptions, Vector3ShakeMotionAdapter> { }
}