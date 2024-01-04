using UnityEngine;

namespace LitMotion.Extensions
{
    public static class LitMotionMaterialExtensions
    {
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