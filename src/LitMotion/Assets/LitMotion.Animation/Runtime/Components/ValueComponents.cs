using System;
using LitMotion.Adapters;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace LitMotion.Animation.Components
{
    public abstract class ValueAnimationComponent<TValue, TOptions, TAdapter> : LitMotionAnimationComponent
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        [SerializeField] SerializableMotionSettings<TValue, TOptions> settings;
        [SerializeField] UnityEvent<TValue> onValueChanged;

        public override MotionHandle Play()
        {
            return LMotion.Create<TValue, TOptions, TAdapter>(settings)
                .Bind(this, (x, state) =>
                {
                    state.onValueChanged.Invoke(x);
                });
        }
    }

    [Serializable]
    [AddAnimationComponentMenu("Value/Float")]
    public sealed class FloatValueAnimation : ValueAnimationComponent<float, NoOptions, FloatMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Double")]
    public sealed class DoubleValueAnimation : ValueAnimationComponent<double, NoOptions, DoubleMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Int")]
    public sealed class IntValueAnimation : ValueAnimationComponent<int, IntegerOptions, IntMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Long")]
    public sealed class LongValueAnimation : ValueAnimationComponent<long, IntegerOptions, LongMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Vector2")]
    public sealed class Vector2ValueAnimation : ValueAnimationComponent<Vector2, NoOptions, Vector2MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Vector3")]
    public sealed class Vector3ValueAnimation : ValueAnimationComponent<Vector3, NoOptions, Vector3MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Vector4")]
    public sealed class Vector4ValueAnimation : ValueAnimationComponent<Vector4, NoOptions, Vector4MotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/Color")]
    public sealed class ColorValueAnimation : ValueAnimationComponent<Color, NoOptions, ColorMotionAdapter> { }

    [Serializable]
    [AddAnimationComponentMenu("Value/String")]
    public sealed class StringValueAnimation : LitMotionAnimationComponent
    {
        [SerializeField] SerializableMotionSettings<FixedString512Bytes, StringOptions> settings;
        [SerializeField] UnityEvent<string> onValueChanged;

        public override MotionHandle Play()
        {
            return LMotion.Create<FixedString512Bytes, StringOptions, FixedString512BytesMotionAdapter>(settings)
                .Bind(this, static (x, state) =>
                {
                    // TODO: avoid allocation
                    state.onValueChanged.Invoke(x.ConvertToString());
                });
        }
    }
}