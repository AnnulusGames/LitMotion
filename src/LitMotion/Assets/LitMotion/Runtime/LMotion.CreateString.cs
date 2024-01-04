using Unity.Collections;
using LitMotion.Adapters;

namespace LitMotion
{
    public static partial class LMotion
    {
        /// <summary>
        /// API for creating string motions.
        /// </summary>
        public static class String
        {
            /// <summary>
            /// Create a builder for building motion.
            /// </summary>
            /// <param name="from">Start value</param>
            /// <param name="to">End value</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<FixedString32Bytes, StringOptions, FixedString32BytesMotionAdapter> Create32Bytes(in FixedString32Bytes from, in FixedString32Bytes to, float duration)
            {
                return Create<FixedString32Bytes, StringOptions, FixedString32BytesMotionAdapter>(from, to, duration);
            }

            /// <summary>
            /// Create a builder for building motion.
            /// </summary>
            /// <param name="from">Start value</param>
            /// <param name="to">End value</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<FixedString64Bytes, StringOptions, FixedString64BytesMotionAdapter> Create64Bytes(in FixedString64Bytes from, in FixedString64Bytes to, float duration)
            {
                return Create<FixedString64Bytes, StringOptions, FixedString64BytesMotionAdapter>(from, to, duration);
            }

            /// <summary>
            /// Create a builder for building motion.
            /// </summary>
            /// <param name="from">Start value</param>
            /// <param name="to">End value</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<FixedString128Bytes, StringOptions, FixedString128BytesMotionAdapter> Create128Bytes(in FixedString128Bytes from, in FixedString128Bytes to, float duration)
            {
                return Create<FixedString128Bytes, StringOptions, FixedString128BytesMotionAdapter>(from, to, duration);
            }

            /// <summary>
            /// Create a builder for building motion.
            /// </summary>
            /// <param name="from">Start value</param>
            /// <param name="to">End value</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<FixedString512Bytes, StringOptions, FixedString512BytesMotionAdapter> Create512Bytes(in FixedString512Bytes from, in FixedString512Bytes to, float duration)
            {
                return Create<FixedString512Bytes, StringOptions, FixedString512BytesMotionAdapter>(from, to, duration);
            }

            /// <summary>
            /// Create a builder for building motion.
            /// </summary>
            /// <param name="from">Start value</param>
            /// <param name="to">End value</param>
            /// <param name="duration">Duration</param>
            /// <returns>Created motion builder</returns>
            public static MotionBuilder<FixedString4096Bytes, StringOptions, FixedString4096BytesMotionAdapter> Create4096Bytes(in FixedString4096Bytes from, in FixedString4096Bytes to, float duration)
            {
                return Create<FixedString4096Bytes, StringOptions, FixedString4096BytesMotionAdapter>(from, to, duration);
            }
        }
    }
}