using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Material.
    /// </summary>
    public static class LitMotionMaterialExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Material material, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetFloat(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Material material, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetFloat(nameID, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, Material material, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetInteger(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, Material material, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetInteger(nameID, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, Material material, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetColor(name, x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to a material property.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaterialColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, Material material, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetColor(nameID, x);
            });
        }
    }
}