using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class MotionHandleTest
    {
        [UnityTest]
        public IEnumerator Test_Cancel()
        {
            var value = 0f;
            var endValue = 10f;
            var handle = LMotion.Create(0f, endValue, 2f)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            handle.Cancel();
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(value < endValue);
            Assert.IsTrue(!handle.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_TryCancel()
        {
            var value = 0f;
            var endValue = 10f;
            var handle = LMotion.Create(0f, endValue, 2f)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            var tryResult = handle.TryCancel();
            Assert.IsTrue(tryResult);
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(value < endValue);
            Assert.IsTrue(!handle.IsActive());

            tryResult = handle.TryCancel();
            Assert.IsFalse(tryResult);
        }

        [UnityTest]
        public IEnumerator Test_Complete()
        {
            var value = 0f;
            var endValue = 10f;
            var handle = LMotion.Create(0f, endValue, 2f)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            handle.Complete();
            Assert.That(value, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
            Assert.IsTrue(!handle.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_TryComplete()
        {
            var value = 0f;
            var endValue = 10f;
            var handle = LMotion.Create(0f, endValue, 2f)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            var tryResult = handle.TryComplete();
            Assert.IsTrue(tryResult);
            Assert.That(value, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
            Assert.IsTrue(!handle.IsActive());

            tryResult = handle.TryComplete();
            Assert.IsFalse(tryResult);
        }

        [UnityTest]
        public IEnumerator Test_Complete_WithYoyoLoop()
        {
            var value = 0f;
            var startValue = 0f;
            var handle = LMotion.Create(startValue, 10f, 2f)
                .WithLoops(2, LoopType.Yoyo)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            handle.Complete();
            Assert.That(value, Is.EqualTo(startValue).Using(FloatEqualityComparer.Instance));
            Assert.IsTrue(!handle.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_CompleteAndCancel_WithInfiniteLoop()
        {
            var value = 0f;
            var startValue = 0f;
            var handle = LMotion.Create(startValue, 10f, 2f)
                .WithLoops(-1)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });
            yield return new WaitForSeconds(1f);
            handle.TryComplete();
            Assert.IsTrue(handle.IsActive());
            handle.TryCancel();
            Assert.IsTrue(!handle.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_ToDisposable()
        {
            var value = 0f;
            var endValue = 10f;
            var disposable = LMotion.Create(0f, endValue, 2f)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                })
                .ToDisposable();
            yield return new WaitForSeconds(1f);
            disposable.Dispose();
            yield return new WaitForSeconds(1f);
            Assert.IsTrue(value < endValue);
        }

        [UnityTest]
        public IEnumerator Test_IsActive()
        {
            MotionHandle handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
            Assert.IsTrue(handle.IsActive());
            yield return new WaitForSeconds(2.5f);
            Assert.IsFalse(handle.IsActive());
        }

        [UnityTest]
        public IEnumerator Test_CompletedLoops()
        {
            var handle = LMotion.Create(0f, 10f, 1f)
                .WithLoops(3)
                .RunWithoutBinding()
                .Preserve();

            Assert.That(handle.ComplatedLoops, Is.EqualTo(0));
            yield return new WaitForSeconds(1f);
            Assert.That(handle.ComplatedLoops, Is.EqualTo(1));
            yield return new WaitForSeconds(1f);
            Assert.That(handle.ComplatedLoops, Is.EqualTo(2));
            yield return new WaitForSeconds(1f);
            Assert.That(handle.ComplatedLoops, Is.EqualTo(3));
        }
    }
}