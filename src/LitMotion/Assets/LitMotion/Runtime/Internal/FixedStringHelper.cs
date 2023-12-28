using Unity.Collections;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;

namespace LitMotion
{
    [BurstCompile]
    internal unsafe static partial class FixedStringHelper
    {
        static readonly char[] LowercaseChars = new char[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };
        static readonly char[] UppercaseChars = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };
        static readonly char[] NumeralsChars = new char[]
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        static readonly char[] AllChars = new char[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        static char GetScrambleChar(ScrambleMode scrambleMode, ref Random random)
        {
            return scrambleMode switch
            {
                ScrambleMode.None => default,
                ScrambleMode.Uppercase => UppercaseChars[random.NextInt(0, UppercaseChars.Length)],
                ScrambleMode.Lowercase => LowercaseChars[random.NextInt(0, LowercaseChars.Length)],
                ScrambleMode.Numerals => NumeralsChars[random.NextInt(0, NumeralsChars.Length)],
                ScrambleMode.All => AllChars[random.NextInt(0, AllChars.Length)],
                _ => default
            };
        }
    }
}