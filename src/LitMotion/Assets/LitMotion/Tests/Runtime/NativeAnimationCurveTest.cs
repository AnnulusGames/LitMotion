using System;
using LitMotion.Collections;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class NativeAnimationCurveTest
    {
        [Test]
        public void Test_NativeAnimationCurve_Evaluate()
        {
            var curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            using var native = new NativeAnimationCurve(curve, Allocator.Temp);
            for (int i = 0; i < 100; i++)
            {
                var t = Mathf.InverseLerp(0, 99, i);
                Assert.That(curve.Evaluate(t), Is.EqualTo(native.Evaluate(t)).Using(FloatEqualityComparer.Instance));
            }
        }

        [Test]
        public void Test_UnsafeAnimationCurve_Evaluate()
        {
            var curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            using var native = new UnsafeAnimationCurve(curve, Allocator.Temp);
            for (int i = 0; i < 100; i++)
            {
                var t = Mathf.InverseLerp(0, 99, i);
                Assert.That(curve.Evaluate(t), Is.EqualTo(native.Evaluate(t)).Using(FloatEqualityComparer.Instance));
            }
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        [Test]
        public void Test_NativeAnimationCurve_Dispose()
        {
            var curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            var native = new NativeAnimationCurve(curve, Allocator.Temp);

            native.Dispose();

            try
            {
                native.Evaluate(0f);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            Assert.Fail();
        }

        [Test]
        public void Test_NativeAnimationCurve_Dispose_RewindableAllocator()
        {
            var curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            var allocator = RewindableAllocatorFactory.CreateAllocator();
            var native = new NativeAnimationCurve(curve, allocator.Allocator.Handle);

            allocator.Allocator.Rewind();

            try
            {
                native.Evaluate(0f);
            }
            catch (ObjectDisposedException)
            {
                return;
            }

            Assert.Fail();
        }
#endif
    }
}
