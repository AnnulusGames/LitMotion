#if LITMOTION_SUPPORT_VFX_GRAPH
using UnityEngine;
using UnityEngine.VFX;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for VisualEffect.
    /// </summary>
    public static class LitMotionVisualEffectExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, name, static (x, visualEffect, name) =>
            {
                visualEffect.SetFloat(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, Box.Create(nameID), static (x, visualEffect, nameID) =>
            {
                visualEffect.SetFloat(nameID.Value, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, name, static (x, visualEffect, name) =>
            {
                visualEffect.SetInt(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, Box.Create(nameID), static (x, visualEffect, nameID) =>
            {
                visualEffect.SetFloat(nameID.Value, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector2<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, name, static (x, visualEffect, name) =>
            {
                visualEffect.SetVector2(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector2<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, Box.Create(nameID), static (x, visualEffect, nameID) =>
            {
                visualEffect.SetVector2(nameID.Value, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector3<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, name, static (x, visualEffect, name) =>
            {
                visualEffect.SetVector3(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector3<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, Box.Create(nameID), static (x, visualEffect, nameID) =>
            {
                visualEffect.SetVector3(nameID.Value, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector4<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, name, static (x, visualEffect, name) =>
            {
                visualEffect.SetVector4(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualEffect parameter.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToVisualEffectVector4<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.Bind(visualEffect, Box.Create(nameID), static (x, visualEffect, nameID) =>
            {
                visualEffect.SetVector4(nameID.Value, x);
            });
        }
    }
}
#endif
