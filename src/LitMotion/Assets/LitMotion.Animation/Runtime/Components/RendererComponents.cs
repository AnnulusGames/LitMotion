using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Float")]
    public sealed class MaterialFloat : PropertyAnimationComponent<Material, float, NoOptions, FloatMotionAdapter>
    {
        [SerializeField] string propertyName = "";

        protected override float GetValue(Material target)
        {
            return target.GetFloat(propertyName);
        }

        protected override void SetRelativeValue(Material target, in float startValue, in float relativeValue)
        {
            target.SetFloat(propertyName, startValue + relativeValue);
        }

        protected override void SetValue(Material target, in float value)
        {
            target.SetFloat(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Int")]
    public sealed class MaterialInt : PropertyAnimationComponent<Material, int, IntegerOptions, IntMotionAdapter>
    {
        [SerializeField] string propertyName = "";

        protected override int GetValue(Material target)
        {
            return target.GetInteger(propertyName);
        }

        protected override void SetRelativeValue(Material target, in int startValue, in int relativeValue)
        {
            target.SetInteger(propertyName, startValue + relativeValue);
        }

        protected override void SetValue(Material target, in int value)
        {
            target.SetInteger(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Vector")]
    public sealed class MaterialVector : PropertyAnimationComponent<Material, Vector4, NoOptions, Vector4MotionAdapter>
    {
        [SerializeField] string propertyName = "";

        protected override Vector4 GetValue(Material target)
        {
            return target.GetVector(propertyName);
        }

        protected override void SetRelativeValue(Material target, in Vector4 startValue, in Vector4 relativeValue)
        {
            target.SetVector(propertyName, startValue + relativeValue);
        }

        protected override void SetValue(Material target, in Vector4 value)
        {
            target.SetVector(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Color")]
    public sealed class MaterialColor : PropertyAnimationComponent<Material, Color, NoOptions, ColorMotionAdapter>
    {
        [SerializeField] string propertyName = "_Color";

        protected override Color GetValue(Material target)
        {
            return target.GetColor(propertyName);
        }

        protected override void SetRelativeValue(Material target, in Color startValue, in Color relativeValue)
        {
            target.SetColor(propertyName, startValue + relativeValue);
        }

        protected override void SetValue(Material target, in Color value)
        {
            target.SetColor(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Sprite Renderer Color")]
    public sealed class SpriteRendererColor : PropertyAnimationComponent<SpriteRenderer, Color, NoOptions, ColorMotionAdapter>
    {
        protected override Color GetValue(SpriteRenderer target)
        {
            return target.color;
        }

        protected override void SetRelativeValue(SpriteRenderer target, in Color startValue, in Color relativeValue)
        {
            target.color = startValue + relativeValue;
        }

        protected override void SetValue(SpriteRenderer target, in Color value)
        {
            target.color = value;
        }
    }
}