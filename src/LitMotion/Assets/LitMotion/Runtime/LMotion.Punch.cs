using UnityEngine;
using LitMotion.Adapters;

namespace LitMotion
{
    public static partial class LMotion
    {
        /// <summary>
        /// API for creating Punch motions.
        /// </summary>
        public static class Punch
        {
            /// <summary>
            /// Create a builder for building Punch motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<float, PunchOptions, FloatPunchMotionAdapter> Create(float startValue, float strength, float duration)
            {
                return Create<float, PunchOptions, FloatPunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }

            /// <summary>
            /// Create a builder for building Punch motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<Vector2, PunchOptions, Vector2PunchMotionAdapter> Create(Vector2 startValue, Vector2 strength, float duration)
            {
                return Create<Vector2, PunchOptions, Vector2PunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }

            /// <summary>
            /// Create a builder for building Punch motion.
            /// </summary>
            /// <param name="startValue">Start value</param>
            /// <param name="strength">Vibration strength</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<Vector3, PunchOptions, Vector3PunchMotionAdapter> Create(Vector3 startValue, Vector3 strength, float duration)
            {
                return Create<Vector3, PunchOptions, Vector3PunchMotionAdapter>(startValue, strength, duration)
                    .WithOptions(PunchOptions.Default);
            }
        }
    }
}