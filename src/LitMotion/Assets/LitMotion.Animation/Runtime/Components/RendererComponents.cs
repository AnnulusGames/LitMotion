using System;
using UnityEngine;
#if LITMOTION_ANIMATION_RENDER_PIPELINES
using UnityEngine.Rendering;
#endif

namespace LitMotion.Animation.Components
{
    [Serializable]
    [LitMotionAnimationComponentMenu("Rendering/Material/Property (Float)")]
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
    [LitMotionAnimationComponentMenu("Rendering/Material/Property (Int)")]
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
    [LitMotionAnimationComponentMenu("Rendering/Material/Property (Vector)")]
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
    [LitMotionAnimationComponentMenu("Rendering/Material/Property (Color)")]
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
    [LitMotionAnimationComponentMenu("Rendering/Sprite Renderer/Color")]
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

#if LITMOTION_ANIMATION_RENDER_PIPELINES

    [Serializable]
    [LitMotionAnimationComponentMenu("Rendering/Volume/Weight")]
    public sealed class VolumeWeightAnimation : FloatPropertyAnimationComponent<Volume>
    {
        protected override float GetValue(Volume target) => target.weight;
        protected override void SetValue(Volume target, in float value) => target.weight = value;
    }

#endif
}