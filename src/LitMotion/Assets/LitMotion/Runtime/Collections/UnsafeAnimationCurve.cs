using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

// TODO: avoid animationCurve.keys allocation

namespace LitMotion.Collections
{
    public unsafe struct UnsafeAnimationCurve : IDisposable
    {
        internal UnsafeList<Keyframe> keys;
        internal WrapMode preWrapMode;
        internal WrapMode postWrapMode;

        public UnsafeAnimationCurve(AllocatorManager.AllocatorHandle allocator)
        {
            keys = new UnsafeList<Keyframe>(0, allocator);
            preWrapMode = default;
            postWrapMode = default;
        }

        public UnsafeAnimationCurve(AnimationCurve animationCurve, AllocatorManager.AllocatorHandle allocator)
        {
            var l = animationCurve.length;
            keys = new UnsafeList<Keyframe>(l, allocator);
            keys.Resize(l, NativeArrayOptions.UninitializedMemory);
            fixed (Keyframe* src = &animationCurve.keys[0])
            {
                UnsafeUtility.MemCpy(keys.Ptr, src, l * sizeof(Keyframe));
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
                UnsafeUtility.MemCpy(keys.Ptr, src, l * sizeof(Keyframe));
            }
            keys.Sort(default(KeyframeComparer));
            preWrapMode = animationCurve.preWrapMode;
            postWrapMode = animationCurve.postWrapMode;
        }

        public void CopyFrom(in UnsafeAnimationCurve animationCurve)
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
            return NativeAnimationCurveHelper.Evaluate(keys.Ptr, keys.Length, preWrapMode, postWrapMode, time);
        }
    }
}