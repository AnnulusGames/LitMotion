using System;
using UnityEngine;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("Rendering/Material Property (Float)")]
    public sealed class MaterialFloatAnimation : FloatPropertyAnimationComponent<Material>
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
    [AddAnimationComponentMenu("Rendering/Material Property (Int)")]
    public sealed class MaterialIntAnimation : IntPropertyAnimationComponent<Material>
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
    [AddAnimationComponentMenu("Rendering/Material Property (Vector)")]
    public sealed class MaterialVectorAnimation : Vector4PropertyAnimationComponent<Material>
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
    [AddAnimationComponentMenu("Rendering/Material Property (Color)")]
    public sealed class MaterialColorAnimation : ColorPropertyAnimationComponent<Material>
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
    public sealed class SpriteRendererColorAnimation : ColorPropertyAnimationComponent<SpriteRenderer>
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