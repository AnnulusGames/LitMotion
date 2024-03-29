using LitMotion.Extensions;
using UnityEngine;

namespace LitMotion.Sequences
{
    [SequenceComponentMenu("Material/Material Property (Color)")]
    public sealed class MaterialColorPropertyComponent : MaterialPropertyComponentBase<Color>
    {
        protected override string GetDefaultDisplayName()
        {
            return "Material Property (Color)";
        }
        
        protected override MotionHandle CreateMotion(Material material, string propertyName, Color current)
        {
            var motionBuilder = LMotion.Create(current + StartValue, current + EndValue, Duration);
            ConfigureMotionBuilder(ref motionBuilder);

            return motionBuilder.BindToMaterialColor(material, propertyName);
        }

        protected override Color GetValue(Material material, string propertyName)
        {
            return material.GetColor(propertyName);
        }

        protected override void SetValue(Material material, string propertyName, Color value)
        {
            material.SetColor(propertyName, value);
        }
    }
}
