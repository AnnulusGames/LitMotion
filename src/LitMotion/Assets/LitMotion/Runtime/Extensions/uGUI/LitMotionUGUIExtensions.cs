using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Unity.Collections;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionUGUIExtensions
    {
        public static MotionHandle BindToColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, Graphic graphic)
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                target.color = x;
            });
        }

        public static MotionHandle BindToColorR(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Graphic graphic)
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.r = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorG(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Graphic graphic)
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.g = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorB(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Graphic graphic)
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.b = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorA(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Graphic graphic)
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.a = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToFillAmount(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Image image)
        {
            Assert.IsNotNull(image);
            return builder.BindWithState(image, (x, target) =>
            {
                if (target == null) return;
                target.fillAmount = x;
            });
        }

        public static MotionHandle BindToFontSize(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.fontSize = x;
            });
        }

        public static MotionHandle BindToText(this MotionBuilder<FixedString32Bytes, StringOptions, FixedString32BytesMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText(this MotionBuilder<FixedString64Bytes, StringOptions, FixedString64BytesMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText(this MotionBuilder<FixedString128Bytes, StringOptions, FixedString128BytesMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText(this MotionBuilder<FixedString512Bytes, StringOptions, FixedString512BytesMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText(this MotionBuilder<FixedString4096Bytes, StringOptions, FixedString4096BytesMotionAdapter> builder, Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }
    }
}