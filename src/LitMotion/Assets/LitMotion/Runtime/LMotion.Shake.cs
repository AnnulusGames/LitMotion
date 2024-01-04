using UnityEngine;
using LitMotion.Adapters;

namespace LitMotion
{
    public static partial class LMotion
    {
        /// <summary>
        /// API for creating Shake motions.
        /// </summary>
        public static class Shake
        {
            /// <summary>
            /// Create a builder for building Shake motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<float, ShakeOptions, FloatShakeMotionAdapter> Create(float startValue, float strength, float duration)
            {
                return Create<float, ShakeOptions, FloatShakeMotionAdapter>(startValue, strength, duration)
                    .WithOptions(ShakeOptions.Default);
            }

            /// <summary>
            /// Create a builder for building Shake motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<Vector2, ShakeOptions, Vector2ShakeMotionAdapter> Create(Vector2 startValue, Vector2 strength, float duration)
            {
                return Create<Vector2, ShakeOptions, Vector2ShakeMotionAdapter>(startValue, strength, duration)
                    .WithOptions(ShakeOptions.Default);
            }

            /// <summary>
            /// Create a builder for building Shake motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<Vector3, ShakeOptions, Vector3ShakeMotionAdapter> Create(Vector3 startValue, Vector3 strength, float duration)
            {
                return Create<Vector3, ShakeOptions, Vector3ShakeMotionAdapter>(startValue, strength, duration)
                    .WithOptions(ShakeOptions.Default);
            }
        }
    }
}