using System;
using Unity.Collections;

namespace LitMotion
{
    /// <summary>
    /// Type of characters used to fill in invisible strings.
    /// </summary>
    public enum ScrambleMode : byte
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// A-Z
        /// </summary>
        Uppercase = 1,
        /// <summary>
        /// a-z
        /// </summary>
        Lowercase = 2,
        /// <summary>
        /// 0-9
        /// </summary>
        Numerals = 3,
        /// <summary>
        /// A-Z, a-z, 0-9
        /// </summary>
        All = 4,
        /// <summary>
        /// Custom characters.
        /// </summary>
        Custom = 5
    }

    /// <summary>
    /// Options for string type motion.
    /// </summary>
    [Serializable]
    public struct StringOptions : IMotionOptions, IEquatable<StringOptions>
    {
        public ScrambleMode ScrambleMode;
        public bool RichTextEnabled;
        public FixedString64Bytes CustomScrambleChars;
        public uint RandomSeed;

        public readonly bool Equals(StringOptions other)
        {
            return other.ScrambleMode == ScrambleMode &&
                other.RichTextEnabled == RichTextEnabled &&
                other.CustomScrambleChars == CustomScrambleChars &&
                other.RandomSeed == RandomSeed;
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is StringOptions options) return Equals(options);
            return false;
        }

        public override readonly int GetHashCode()
        {
            return HashCode.Combine(ScrambleMode, RichTextEnabled, CustomScrambleChars, RandomSeed);
        }
    }
}