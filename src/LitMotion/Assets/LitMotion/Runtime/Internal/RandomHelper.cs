using Unity.Mathematics;
using UnityEngine;

namespace LitMotion
{
    internal static class RandomHelper
    {
        public static float NextFloat(uint seed, double time, float min, float max)
        {
            return Create(seed, time).NextFloat(min, max);
        }

        public static float2 NextFloat2(uint seed, double time, in float2 min, in float2 max)
        {
            return Create(seed, time).NextFloat2(min, max);
        }

        public static float3 NextFloat3(uint seed, double time, in float3 min, in float3 max)
        {
            return Create(seed, time).NextFloat3(min, max);
        }

        static uint GetHash(uint seed, double time)
        {
            var hash = new Hash128();
            hash.Append(ref seed);
            hash.Append(ref time);
            return (uint)hash.GetHashCode();
        }

        public static Unity.Mathematics.Random Create(uint seed, double time)
        {
            return new Unity.Mathematics.Random(GetHash(seed, time));
        }
    }
}