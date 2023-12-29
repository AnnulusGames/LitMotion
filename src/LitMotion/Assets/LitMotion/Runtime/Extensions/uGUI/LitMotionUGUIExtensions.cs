using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using Unity.Collections;

namespace LitMotion.Extensions
{
    public static class LitMotionUGUIExtensions
    {
        public static MotionHandle BindToColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, Graphic graphic)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Assert.IsNotNull(graphic);
            return builder.BindWithState(graphic, (x, target) =>
            {
                if (target == null) return;
                target.color = x;
            });
        }

        public static MotionHandle BindToColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Graphic graphic)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
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

        public static MotionHandle BindToColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Graphic graphic)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
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

        public static MotionHandle BindToColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Graphic graphic)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
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

        public static MotionHandle BindToColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Graphic graphic)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
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

        public static MotionHandle BindToFillAmount<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Image image)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Assert.IsNotNull(image);
            return builder.BindWithState(image, (x, target) =>
            {
                if (target == null) return;
                target.fillAmount = x;
            });
        }

        public static MotionHandle BindToFontSize<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.fontSize = x;
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString32Bytes, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString32Bytes, TOptions>
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString64Bytes, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString64Bytes, TOptions>
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString128Bytes, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString128Bytes, TOptions>
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString512Bytes, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString512Bytes, TOptions>
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString4096Bytes, TOptions, TAdapter> builder, Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString4096Bytes, TOptions>
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