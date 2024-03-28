using LitMotion.Extensions;
using UnityEngine;

namespace LitMotion.Sequences
{
    [SequenceComponentMenu("Material/Material Property (float)")]
    public sealed class MaterialFloatPropertyComponent : MaterialPropertyComponentBase<float>
    {
        public override void ResetComponent()
        {
            base.ResetComponent();
            displayName = "Material Property (float)";
        }

        protected override MotionHandle CreateMotion(Material material, string propertyName, float current)
        {
            var motionBuilder = LMotion.Create(current + StartValue, current + EndValue, Duration);
            ConfigureMotionBuilder(ref motionBuilder);

            return motionBuilder.BindToMaterialFloat(material, propertyName);
        }

        protected override float GetValue(Material material, string propertyName)
        {
            return material.GetFloat(propertyName);
        }

        protected override void SetValue(Material material, string propertyName, float value)
        {
            material.SetFloat(propertyName, value);
        }
    }
}
