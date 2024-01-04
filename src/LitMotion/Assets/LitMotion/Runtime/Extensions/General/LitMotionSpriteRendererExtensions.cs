using UnityEngine;

namespace LitMotion.Extensions
{
    public static class LitMotionSpriteRendererExtensions
    {
        public static MotionHandle BindToColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                m.color = x;
            });
        }

        public static MotionHandle BindToColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.r = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.g = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.BindWithState(spriteRenderer, (x, m) =>
            {
                if (m == null) return;
                var c = m.color;
                c.b = x;
                m.color = c;
            });
        }

        public static MotionHandle BindToColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
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