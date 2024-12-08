using System;

namespace LitMotion
{
    /// <summary>
    /// Options for integer type motion.
    /// </summary>
    [Serializable]
    public struct IntegerOptions : IMotionOptions, IEquatable<IntegerOptions>
    {
        public RoundingMode RoundingMode;

        public readonly bool Equals(IntegerOptions other)
        {
            return other.RoundingMode == RoundingMode;
        }

        public override readonly bool Equals(object obj)
        {
            if (obj is IntegerOptions integerOptions) return Equals(integerOptions);
            return false;
        }

        public override readonly int GetHashCode()
        {
            return (int)RoundingMode;
        }
    }

    /// <summary>
    /// Specifies the rounding format for values after the decimal point.
    /// </summary>
    public enum RoundingMode : byte
    {
        ToEven,
        AwayFromZero,
        ToZero,
        ToPositiveInfinity,
        ToNegativeInfinity
    }
}