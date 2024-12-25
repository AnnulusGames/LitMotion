using UnityEngine;
using LitMotion.Adapters;

namespace LitMotion
{
    /// <summary>
    /// The main class of the LitMotion library that creates and configures motion.
    /// </summary>
    public static partial class LMotion
    {
        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<float, NoOptions, FloatMotionAdapter> Create(float from, float to, float duration) => Create<float, NoOptions, FloatMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<double, NoOptions, DoubleMotionAdapter> Create(double from, double to, float duration) => Create<double, NoOptions, DoubleMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<int, IntegerOptions, IntMotionAdapter> Create(int from, int to, float duration) => Create<int, IntegerOptions, IntMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<long, IntegerOptions, LongMotionAdapter> Create(long from, long to, float duration) => Create<long, IntegerOptions, LongMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> Create(Vector2 from, Vector2 to, float duration) => Create<Vector2, NoOptions, Vector2MotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> Create(Vector3 from, Vector3 to, float duration) => Create<Vector3, NoOptions, Vector3MotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Vector4, NoOptions, Vector4MotionAdapter> Create(Vector4 from, Vector4 to, float duration) => Create<Vector4, NoOptions, Vector4MotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Quaternion, NoOptions, QuaternionMotionAdapter> Create(Quaternion from, Quaternion to, float duration) => Create<Quaternion, NoOptions, QuaternionMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Color, NoOptions, ColorMotionAdapter> Create(Color from, Color to, float duration) => Create<Color, NoOptions, ColorMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<Rect, NoOptions, RectMotionAdapter> Create(Rect from, Rect to, float duration) => Create<Rect, NoOptions, RectMotionAdapter>(from, to, duration);

        /// <summary>
        /// Create a builder for building motion.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion entity</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="from">Start value</param>
        /// <param name="to">End value</param>
        /// <param name="duration">Duration</param>
        /// <returns>Created motion builder</returns>
        public static MotionBuilder<TValue, TOptions, TAdapter> Create<TValue, TOptions, TAdapter>(in TValue from, in TValue to, float duration)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var buffer = MotionBuilderBuffer<TValue, TOptions>.Rent();
            buffer.StartValue = from;
            buffer.EndValue = to;
            buffer.Duration = duration;
            return new MotionBuilder<TValue, TOptions, TAdapter>(buffer);
        }
    }
}