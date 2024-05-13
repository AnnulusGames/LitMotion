using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Sprite Renderer/Color")]
    public sealed class SpriteRendererColorComponent : PropertyComponentBase<Color, NoOptions, ColorMotionAdapter, SpriteRenderer>
    {
        protected override string GetDefaultDisplayName()
        {
            return "Color";
        }

        protected override Color GetRelativeValue(Color start, Color end) => start + end;
        protected override Color GetValue(Object obj) => ((SpriteRenderer)obj).color;
        protected override void SetValue(Object obj, Color value) => ((SpriteRenderer)obj).color = value;
    }
}
