using UnityEngine;
using UnityEngine.Assertions;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionSpriteRendererExtensions
    {
        public static MotionHandle BindToColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, SpriteRenderer spriteRenderer)
        {
            Assert.IsNotNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                m.color = x;
            });
        }

        public static MotionHandle BindToColorR(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, SpriteRenderer spriteRenderer)
        {
            Assert.IsNotNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.r = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorG(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, SpriteRenderer spriteRenderer)
        {
            Assert.IsNotNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.g = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorB(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, SpriteRenderer spriteRenderer)
        {
            Assert.IsNotNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.b = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorA(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, SpriteRenderer spriteRenderer)
        {
            Assert.IsNotNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.a = x;
                m.color = c;
            });
        }
    }
}