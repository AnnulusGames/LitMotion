#if LITMOTION_SUPPORT_URP
using LitMotion.Adapters;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace LitMotion.Sequences.Components
{
    [SequenceComponentMenu("URP/Post-processing/Bloom (Intensity)")]
    public sealed class BloomIntensityComponent : PostProcessingPropertyComponentBase<float, NoOptions, FloatMotionAdapter, Bloom>
    {
        protected override string GetDefaultDisplayName()
        {
            return "Bloom (Intensity)";
        }

        protected override float GetRelativeValue(float start, float end) => start + end;
        protected override float GetValue(Object obj) => ((Bloom)obj).intensity.value;
        protected override void SetValue(Object obj, float value) => ((Bloom)obj).intensity.value = value;
    }
}
#endif