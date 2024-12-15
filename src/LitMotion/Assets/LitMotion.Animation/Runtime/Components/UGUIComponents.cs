#if LITMOTION_ANIMATION_UGUI

using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LitMotion.Animation.Components
{
    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Text/Text")]
    public sealed class TextAnimation : FixedString512BytesPropertyAnimationComponent<Text>
    {
        protected override FixedString512Bytes GetValue(Text target) => target.text;
        protected override void SetValue(Text target, in FixedString512Bytes value) => target.text = value.ToString();
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Text/Color")]
    public sealed class TextColorAnimation : ColorPropertyAnimationComponent<Text>
    {
        protected override Color GetValue(Text target) => target.color;
        protected override void SetValue(Text target, in Color value) => target.color = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Text/Font Size")]
    public sealed class TextFontSizeAnimation : IntPropertyAnimationComponent<Text>
    {
        protected override int GetValue(Text target) => target.fontSize;
        protected override void SetValue(Text target, in int value) => target.fontSize = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Graphic/Color")]
    public sealed class GraphicColorAnimation : ColorPropertyAnimationComponent<Graphic>
    {
        protected override Color GetValue(Graphic target) => target.color;
        protected override void SetValue(Graphic target, in Color value) => target.color = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Image/Color")]
    public sealed class ImageColorAnimation : ColorPropertyAnimationComponent<Image>
    {
        protected override Color GetValue(Image target) => target.color;
        protected override void SetValue(Image target, in Color value) => target.color = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Image/Color (Alpha)")]
    public sealed class ImageColorAlphaAnimation : FloatPropertyAnimationComponent<Image>
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
    [LitMotionAnimationComponentMenu("UI/Image/Fill Amount")]
    public sealed class ImageFillAmountAnimation : FloatPropertyAnimationComponent<Image>
    {
        protected override float GetValue(Image target) => target.fillAmount;
        protected override void SetValue(Image target, in float value) => target.fillAmount = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Slider/Value")]
    public sealed class SliderValueAnimation : FloatPropertyAnimationComponent<Slider>
    {
        protected override float GetValue(Slider target) => target.value;
        protected override void SetValue(Slider target, in float value) => target.value = value;
    }

    [Serializable]
    [LitMotionAnimationComponentMenu("UI/Canvas Group/Alpha")]
    public sealed class CanvasGroupAlphaAnimation : FloatPropertyAnimationComponent<CanvasGroup>
    {
        protected override float GetValue(CanvasGroup target) => target.alpha;
        protected override void SetValue(CanvasGroup target, in float value) => target.alpha = value;
    }
}

#endif