using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for SpriteRenderer.
    /// </summary>
    public static class LitMotionSpriteRendererExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to SpriteRenderer.color
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.Bind(spriteRenderer, static (x, m) =>
            {
                m.color = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to SpriteRenderer.color.r
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.Bind(spriteRenderer, static (x, m) =>
            {
                var c = m.color;
                c.r = x;
                m.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to SpriteRenderer.color.g
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.Bind(spriteRenderer, static (x, m) =>
            {
                var c = m.color;
                c.g = x;
                m.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to SpriteRenderer.color.b
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.Bind(spriteRenderer, static (x, m) =>
            {
                var c = m.color;
                c.b = x;
                m.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to SpriteRenderer.color.a
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, SpriteRenderer spriteRenderer)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(spriteRenderer);
            return builder.Bind(spriteRenderer, static (x, m) =>
            {
                var c = m.color;
                c.a = x;
                m.color = c;
            });
        }
    }
}