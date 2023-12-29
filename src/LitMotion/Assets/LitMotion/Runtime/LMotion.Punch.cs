using UnityEngine;
using LitMotion.Adapters;

namespace LitMotion
{
    public static partial class LMotion
    {
        public static class Punch
        {
            public static MotionBuilder<float, PunchOptions, FloatPunchMotionAdapter> Create(float startValue, float strength, float duration)
            {
                return Create<float, PunchOptions, FloatPunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }

            public static MotionBuilder<Vector2, PunchOptions, Vector2PunchMotionAdapter> Create(Vector2 startValue, Vector2 strength, float duration)
            {
                return Create<Vector2, PunchOptions, Vector2PunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }

            public static MotionBuilder<Vector3, PunchOptions, Vector3PunchMotionAdapter> Create(Vector3 startValue, Vector3 strength, float duration)
            {
                return Create<Vector3, PunchOptions, Vector3PunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }
        }
    }
}