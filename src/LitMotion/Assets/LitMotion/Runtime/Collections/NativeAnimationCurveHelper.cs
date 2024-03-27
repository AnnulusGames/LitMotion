using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace LitMotion.Collections
{
    internal readonly struct KeyframeComparer : IComparer<Keyframe>
    {
        public readonly int Compare(Keyframe keyframe1, Keyframe keyframe2) => keyframe1.time.CompareTo(keyframe2.time);
    }

    [BurstCompile]
    internal unsafe static class NativeAnimationCurveHelper
    {
        static readonly float4x4 curveMatrix = new
        (
            1, 0, 0, 0,
            -3, 3, 0, 0,
            3, -6, 3, 0,
            -1, 3, -3, 1
        );

        static readonly float3x3 curveMatrixPrime = new
        (
            1, 0, 0,
            -2, 2, 0,
            1, -2, 1
        );

        [BurstCompile]
        public static float Evaluate(Keyframe* ptr, int length, WrapMode preWrapMode, WrapMode postWrapMode, float time)
        {
            time = WrapTime(ptr, length, preWrapMode, postWrapMode, time);
            Keyframe keyframe = default;
            keyframe.time = time;
            int index = GetInsertionIndexForSortedArray(ptr, length, ref keyframe);
            if (index == 0)
            {
                index++;
            }
            else if (index == length)
            {
                index = length - 1;
            }
            keyframe = ptr[index - 1];
            Keyframe nextKeyframe = ptr[index];
            return Evaluate(time, ref keyframe, ref nextKeyframe);
        }

        static int GetInsertionIndexForSortedArray(Keyframe* ptr, int length, ref Keyframe keyframe)
        {
            var list = new UnsafeList<Keyframe>(ptr, length);
            var index = list.BinarySearch(keyframe, default(KeyframeComparer));
            if (index < 0) index = ~index;
            return index;
        }

        static float WrapTime(Keyframe* ptr, int length, WrapMode preWrapMode, WrapMode postWrapMode, float time)
        {
            float lastKeyTime = ptr[length - 1].time;
            if (time < 0f)
            {
                switch (preWrapMode)
                {
                    case WrapMode.Default:
                    case WrapMode.ClampForever:
                    case WrapMode.Once:
                        time = 0f;
                        break;
                    case WrapMode.Loop:
                        time = time % lastKeyTime - ptr[0].time;
                        break;
                    case WrapMode.PingPong:
                        time = Mathf.PingPong(time, lastKeyTime - ptr[0].time);
                        break;
                }
            }
            else if (time > lastKeyTime)
            {
                switch (postWrapMode)
                {
                    case WrapMode.Default:
                    case WrapMode.ClampForever:
                        time = lastKeyTime;
                        break;
                    case WrapMode.Once:
                        time = 0f;
                        break;
                    case WrapMode.Loop:
                        time = time % lastKeyTime - ptr[0].time;
                        break;
                    case WrapMode.PingPong:
                        time = Mathf.PingPong(time, lastKeyTime - ptr[0].time);
                        break;
                }
            }
            return time;
        }

        static float Evaluate(float time, ref Keyframe keyframe, ref Keyframe nextKeyframe)
        {
            if (!math.isfinite(keyframe.outTangent) || !math.isfinite(nextKeyframe.inTangent))
            {
                return keyframe.value;
            }
            float timeDiff = nextKeyframe.time - keyframe.time;
            float t = (time - keyframe.time) / timeDiff;
            float outWeight = (int)keyframe.weightedMode >= (int)WeightedMode.Out ? keyframe.outWeight : 1f / 3f;
            float inWeight = (int)nextKeyframe.weightedMode >= (int)WeightedMode.In ? nextKeyframe.inWeight : 1f / 3f;
            float tBottom = 0, tTop = 1;
            float diff = float.MaxValue;

            float4 xCoords = new(keyframe.time, keyframe.time + outWeight * timeDiff, nextKeyframe.time - inWeight * timeDiff, nextKeyframe.time);
            float4 curveXCoords = math.mul(curveMatrix, xCoords);
            GetTWithNewtonMethod(time, in xCoords, in curveXCoords, ref t, ref tBottom, ref tTop, ref diff);
            GetTWithBisectionMethod(time, in curveXCoords, ref t, ref tBottom, ref tTop, ref diff);

            float4 yCoords = new(keyframe.value, keyframe.value + outWeight * keyframe.outTangent * timeDiff, nextKeyframe.value - inWeight * nextKeyframe.inTangent * timeDiff, nextKeyframe.value);
            float4 curveYCoords = math.mul(curveMatrix, yCoords);
            return CubicBezier(in curveYCoords, t);
        }

        static float CubicBezier(in float4 curveMatrix, float t)
        {
            float tt = t * t;
            float4 powerSeries = new(1, t, tt, tt * t);
            return math.dot(powerSeries, curveMatrix);
        }

        static float CubicBezier(in float3 curveMatrix, float t)
        {
            float3 powerSeries = new(1, t, t * t);
            return math.dot(powerSeries, curveMatrix);
        }

        static float DeCasteljauBezier(int degree, float4 coords, float t)
        {
            float one_t = 1 - t;
            for (int k = 1; k <= degree; k++)
            {
                for (int i = 0; i <= (degree - k); i++)
                {
                    coords[i] = one_t * coords[i] + t * coords[i + 1];
                }
            }
            return coords[0];
        }

        static void GetTWithBisectionMethod(float time, in float4 curveXCoords, ref float t, ref float tBottom, ref float tTop, ref float diff)
        {
            const float accuracy = 0.0000001f;
            const int maxIterationCount = 20;
            int iterationCount = 0;
            while (diff > accuracy && iterationCount < maxIterationCount)
            {
                iterationCount++;
                t = (tTop + tBottom) * 0.5f;
                float x = CubicBezier(in curveXCoords, t);
                diff = math.abs(x - time);
                UpdateTLimits(x, time, t, ref tBottom, ref tTop);
            }
        }

        static void GetTWithNewtonMethod(float time, in float4 xCoords, in float4 curveXCoords, ref float t, ref float tBottom, ref float tTop, ref float diff)
        {
            const float accuracy = 0.0000001f;
            const int maxIterationCount = 20;
            int iterationCount = 0;

            float4 primeCoords = default;
            for (int i = 0; i < 3; i++)
            {
                primeCoords[i] = (xCoords[i + 1] - xCoords[i]) * 3;
            }
            float4 primePrimeCoords = default;
            for (int i = 0; i < 2; i++)
            {
                primePrimeCoords[i] = (primeCoords[i + 1] - primeCoords[i]) * 2;
            }

            float3 curvePrimeCoords = math.mul(curveMatrixPrime, primeCoords.xyz);
            while (diff > accuracy && iterationCount < maxIterationCount)
            {
                iterationCount++;
                float x;
                float newT = UseNewtonMethod(curveXCoords, time, t, curvePrimeCoords, primePrimeCoords, out x);
                float newDiff = math.abs(x - time);
                if (newT < 0 || newT > 1 || newDiff > diff)
                {
                    break;
                }
                diff = newDiff;
                UpdateTLimits(x, time, t, ref tBottom, ref tTop);
                t = newT;
            }
        }

        static float UseNewtonMethod(float4 curveCoords, float coord, float t, float3 curvePrimeCoords, float4 primePrimeCoords, out float coordAtT)
        {
            coordAtT = CubicBezier(in curveCoords, t);
            float coordPrimeAtT = CubicBezier(in curvePrimeCoords, t);
            float coordPrimePrimeAtT = DeCasteljauBezier(1, primePrimeCoords, t);
            float coordAtTMinusCoord = coordAtT - coord;
            float fAtT = coordAtTMinusCoord * coordPrimeAtT;
            float fPrimeAtT = coordAtTMinusCoord * coordPrimePrimeAtT + coordPrimeAtT * coordPrimeAtT;
            return t - (fAtT / fPrimeAtT);
        }

        static void UpdateTLimits(float x, float time, float t, ref float tBottom, ref float tTop)
        {
            if (x > time)
            {
                tTop = math.clamp(t, tBottom, tTop);
            }
            else
            {
                tBottom = math.clamp(t, tBottom, tTop);
            }
        }
    }
}