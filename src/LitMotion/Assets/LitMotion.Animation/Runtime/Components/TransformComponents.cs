
using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Transform/Position")]
    public sealed class Position : PropertyAnimationComponent<Transform, Vector3, NoOptions, Vector3MotionAdapter>
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
    [AddAnimationComponentMenu("Transform/Rotation")]
    public sealed class Rotation : PropertyAnimationComponent<Transform, Vector3, NoOptions, Vector3MotionAdapter>
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
    [AddAnimationComponentMenu("Transform/Scale")]
    public sealed class Scale : PropertyAnimationComponent<Transform, Vector3, NoOptions, Vector3MotionAdapter>
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
}