using LitMotion.Adapters;
using UnityEngine;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("Sprite Renderer/Color")]
    public sealed class SpriteRendererColorComponent : PropertyComponentBase<Color, NoOptions, ColorMotionAdapter, SpriteRenderer>
    {
        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Color";
        }

        protected override Color GetRelativeValue(Color start, Color end) => start + end;
        protected override Color GetValue(SpriteRenderer obj) => obj.color;
        protected override void SetValue(SpriteRenderer obj, Color value) => obj.color = value;
    }
}
