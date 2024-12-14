#if LITMOTION_ANIMATION_UGUI

using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [AddAnimationComponentMenu("UI/Text")]
    public sealed class TextValue : FixedString512BytesPropertyAnimationComponent<Text>
    {
        protected override FixedString512Bytes GetValue(Text target) => target.text;
        protected override void SetValue(Text target, in FixedString512Bytes value) => target.text = value.ToString();
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Text Color")]
    public sealed class TextColor : ColorPropertyAnimationComponent<Text>
    {
        protected override Color GetValue(Text target) => target.color;
        protected override void SetValue(Text target, in Color value) => target.color = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Text Font Size")]
    public sealed class TextFontSize : IntPropertyAnimationComponent<Text>
    {
        protected override int GetValue(Text target) => target.fontSize;
        protected override void SetValue(Text target, in int value) => target.fontSize = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Graphic Color")]
    public sealed class GraphicColor : ColorPropertyAnimationComponent<Graphic>
    {
        protected override Color GetValue(Graphic target) => target.color;
        protected override void SetValue(Graphic target, in Color value) => target.color = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Image Color")]
    public sealed class ImageColor : ColorPropertyAnimationComponent<Image>
    {
        protected override Color GetValue(Image target) => target.color;
        protected override void SetValue(Image target, in Color value) => target.color = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Image Color (Alpha)")]
    public sealed class ImageColorAlpha : FloatPropertyAnimationComponent<Image>
    {
        protected override float GetValue(Image target) => target.color.a;
        protected override void SetValue(Image target, in float value)
        {
            var c = target.color;
            c.a = value;
            target.color = c;
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Image Fill Amount")]
    public sealed class ImageFillAmount : FloatPropertyAnimationComponent<Image>
    {
        protected override float GetValue(Image target) => target.fillAmount;
        protected override void SetValue(Image target, in float value) => target.fillAmount = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Slider")]
    public sealed class SliderValue : FloatPropertyAnimationComponent<Slider>
    {
        protected override float GetValue(Slider target) => target.value;
        protected override void SetValue(Slider target, in float value) => target.value = value;
    }

    [Serializable]
    [AddAnimationComponentMenu("UI/Canvas Group Alpha")]
    public sealed class CanvasGroupAlpha : FloatPropertyAnimationComponent<CanvasGroup>
    {
        protected override float GetValue(CanvasGroup target) => target.alpha;
        protected override void SetValue(CanvasGroup target, in float value) => target.alpha = value;
    }
}

#endif