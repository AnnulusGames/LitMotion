using System;
using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Float")]
    public sealed class MaterialFloat : FloatPropertyAnimationComponent<Material>
    {
        [SerializeField] string propertyName = "";

        protected override float GetValue(Material target)
        {
            return target.GetFloat(propertyName);
        }

        protected override void SetValue(Material target, in float value)
        {
            target.SetFloat(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Int")]
    public sealed class MaterialInt : IntPropertyAnimationComponent<Material>
    {
        [SerializeField] string propertyName = "";

        protected override int GetValue(Material target)
        {
            return target.GetInteger(propertyName);
        }

        protected override void SetValue(Material target, in int value)
        {
            target.SetInteger(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Vector")]
    public sealed class MaterialVector : Vector4PropertyAnimationComponent<Material>
    {
        [SerializeField] string propertyName = "";

        protected override Vector4 GetValue(Material target)
        {
            return target.GetVector(propertyName);
        }

        protected override void SetValue(Material target, in Vector4 value)
        {
            target.SetVector(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Color")]
    public sealed class MaterialColor : ColorPropertyAnimationComponent<Material>
    {
        [SerializeField] string propertyName = "_Color";

        protected override Color GetValue(Material target)
        {
            return target.GetColor(propertyName);
        }

        protected override void SetValue(Material target, in Color value)
        {
            target.SetColor(propertyName, value);
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Rendering/Sprite Renderer Color")]
    public sealed class SpriteRendererColor : ColorPropertyAnimationComponent<SpriteRenderer>
    {
        protected override Color GetValue(SpriteRenderer target)
        {
            return target.color;
        }

        protected override void SetValue(SpriteRenderer target, in Color value)
        {
            target.color = value;
        }
    }
}