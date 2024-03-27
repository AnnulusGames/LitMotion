using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

// TODO: avoid animationCurve.keys allocation

namespace LitMotion.Collections
{
    public unsafe struct NativeAnimationCurve : IDisposable
    {
        NativeList<Keyframe> keys;
        WrapMode preWrapMode;
        WrapMode postWrapMode;

        public NativeAnimationCurve(AllocatorManager.AllocatorHandle allocator)
        {
            keys = new NativeList<Keyframe>(0, allocator);
            preWrapMode = default;
            postWrapMode = default;
        }

        public NativeAnimationCurve(AnimationCurve animationCurve, AllocatorManager.AllocatorHandle allocator)
        {
            var l = animationCurve.length;
            keys = new NativeList<Keyframe>(l, allocator);
            keys.Resize(l, NativeArrayOptions.UninitializedMemory);
            fixed (Keyframe* src = &animationCurve.keys[0])
            {
                UnsafeUtility.MemCpy(keys.GetUnsafePtr(), src, l * sizeof(Keyframe));
            }
            keys.Sort(default(KeyframeComparer));
            preWrapMode = animationCurve.preWrapMode;
            postWrapMode = animationCurve.postWrapMode;
        }

        public void CopyFrom(AnimationCurve animationCurve)
        {
            var l = animationCurve.length;
            keys.Resize(l, NativeArrayOptions.UninitializedMemory);
            fixed (Keyframe* src = &animationCurve.keys[0])
            {
                UnsafeUtility.MemCpy(keys.GetUnsafePtr(), src, l * sizeof(Keyframe));
            }
            keys.Sort(default(KeyframeComparer));
            preWrapMode = animationCurve.preWrapMode;
            postWrapMode = animationCurve.postWrapMode;
        }

        public void CopyFrom(in NativeAnimationCurve animationCurve)
        {
            keys.CopyFrom(animationCurve.keys);
            preWrapMode = animationCurve.preWrapMode;
            postWrapMode = animationCurve.postWrapMode;
        }

        public void Dispose()
        {
            keys.Dispose();
        }

        public readonly bool IsCreated => keys.IsCreated;

        public float Evaluate(float time)
        {
            return NativeAnimationCurveHelper.Evaluate((Keyframe*)keys.GetUnsafePtr(), keys.Length, preWrapMode, postWrapMode, time);
        }
    }
}