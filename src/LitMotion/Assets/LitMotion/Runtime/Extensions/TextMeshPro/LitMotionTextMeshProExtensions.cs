#if LITMOTION_SUPPORT_TMP
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionTextMeshProExtensions
    {
        public static MotionHandle BindToFontSize(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.fontSize = x;
            });
        }

        public static MotionHandle BindToMaxVisibleCharacters(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleCharacters = x;
            });
        }

        public static MotionHandle BindToMaxVisibleLines(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleLines = x;
            });
        }

        public static MotionHandle BindToMaxVisibleWords(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleWords = x;
            });
        }

        public static MotionHandle BindToColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.color = x;
            });
        }

        public static MotionHandle BindToColorR(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.r = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorG(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.g = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorB(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.b = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorA(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.a = x;
                target.color = c;
            });
        }
    }
}
#endif