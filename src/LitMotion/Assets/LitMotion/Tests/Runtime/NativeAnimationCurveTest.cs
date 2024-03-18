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
        public void Test_Evaluate()
        {
            var curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            using var native = new NativeAnimationCurve(curve, Allocator.Temp);
            for (int i = 1; i <= 100; i++)
            {
                var t = 1f / i;
                Assert.That(curve.Evaluate(t), Is.EqualTo(native.Evaluate(t)).Using(FloatEqualityComparer.Instance));
            }
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        [Test]
        public void Test_Dispose()
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
#endif
    }
}
