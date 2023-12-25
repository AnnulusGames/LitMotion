using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

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
            Assert.AreApproximatelyEqual(value, endValue);
            Assert.IsTrue(!handle.IsActive());
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
            Assert.AreApproximatelyEqual(value, startValue);
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
            handle.Complete();
            Assert.IsTrue(handle.IsActive());
            handle.Cancel();
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
    }
}