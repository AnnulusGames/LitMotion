using UnityEngine;
using UnityEngine.Assertions;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionMaterialExtensions
    {
        public static MotionHandle BindToMaterialFloat(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Material material, string name)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetFloat(name, x);
            });
        }

        public static MotionHandle BindToMaterialFloat(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Material material, int nameID)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetFloat(nameID, x);
            });
        }

        public static MotionHandle BindToMaterialInt(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, Material material, string name)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetInteger(name, x);
            });
        }

        public static MotionHandle BindToMaterialInt(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, Material material, int nameID)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetInteger(nameID, x);
            });
        }

        public static MotionHandle BindToMaterialColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, Material material, string name)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetColor(name, x);
            });
        }

        public static MotionHandle BindToMaterialColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, Material material, int nameID)
        {
            Assert.IsNotNull(material);
            return builder.BindWithState(material, (x, m) =>
            {
                if (m == null) return;
                m.SetColor(nameID, x);
            });
        }
    }
}