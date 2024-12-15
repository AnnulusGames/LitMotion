using System;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Aspect")]
    public sealed class CameraAspectAnimation : FloatPropertyAnimationComponent<Camera>
    {
        protected override float GetValue(Camera target) => target.aspect;
        protected override void SetValue(Camera target, in float value) => target.aspect = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Near Clip Plane")]
    public sealed class CameraNearClipPlaneAnimation : FloatPropertyAnimationComponent<Camera>
    {
        protected override float GetValue(Camera target) => target.nearClipPlane;
        protected override void SetValue(Camera target, in float value) => target.nearClipPlane = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Far Clip Plane")]
    public sealed class CameraFarClipPlaneAnimation : FloatPropertyAnimationComponent<Camera>
    {
        protected override float GetValue(Camera target) => target.farClipPlane;
        protected override void SetValue(Camera target, in float value) => target.farClipPlane = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Field Of View")]
    public sealed class CameraFieldOfViewAnimation : FloatPropertyAnimationComponent<Camera>
    {
        protected override float GetValue(Camera target) => target.fieldOfView;
        protected override void SetValue(Camera target, in float value) => target.fieldOfView = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Orthographic Size")]
    public sealed class CameraOrthographicSizeAnimation : FloatPropertyAnimationComponent<Camera>
    {
        protected override float GetValue(Camera target) => target.orthographicSize;
        protected override void SetValue(Camera target, in float value) => target.orthographicSize = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Rect")]
    public sealed class CameraRectAnimation : RectPropertyAnimationComponent<Camera>
    {
        protected override Rect GetValue(Camera target) => target.rect;
        protected override void SetValue(Camera target, in Rect value) => target.rect = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Pixel Rect")]
    public sealed class CameraPixelRectAnimation : RectPropertyAnimationComponent<Camera>
    {
        protected override Rect GetValue(Camera target) => target.pixelRect;
        protected override void SetValue(Camera target, in Rect value) => target.pixelRect = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("Camera/Background Color")]
    public sealed class CameraBackgroundColorAnimation : ColorPropertyAnimationComponent<Camera>
    {
        protected override Color GetValue(Camera target) => target.backgroundColor;
        protected override void SetValue(Camera target, in Color value) => target.backgroundColor = value;
    }
}