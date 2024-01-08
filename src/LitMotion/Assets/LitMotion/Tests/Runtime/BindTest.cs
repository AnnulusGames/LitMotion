using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class BindTest
    {
        [UnityTest]
        public IEnumerator Test_Bind_LocalVariable()
        {
            var value = 0f;
            var endValue = 10f;
            LMotion.Create(0f, endValue, 1f).Bind(x => value = x);
            yield return new WaitForSeconds(1.1f);
            Assert.AreApproximatelyEqual(value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_BindWithState()
        {
            var target = new TestClass();
            var endValue = 10f;
            LMotion.Create(0f, endValue, 0.5f).BindWithState(target, (x, target) => 
            {
                target.Value = x;
            });
            yield return new WaitForSeconds(0.6f);
            Assert.AreApproximatelyEqual(target.Value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_BindWithState_2()
        {
            var target1 = new TestClass();
            var target2 = new TestClass();
            var endValue = 10f;
            LMotion.Create(0f, endValue, 0.5f).BindWithState(target1, target2, (x, target1, target2) =>
            {
                target1.Value = x;
                target2.Value = x;
            });
            yield return new WaitForSeconds(0.6f);
            Assert.AreApproximatelyEqual(target1.Value, endValue);
            Assert.AreApproximatelyEqual(target2.Value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_BindWithState_3()
        {
            var target1 = new TestClass();
            var target2 = new TestClass();
            var target3 = new TestClass();
            var endValue = 10f;
            LMotion.Create(0f, endValue, 0.5f).BindWithState(target1, target2, target3, (x, target1, target2, target3) =>
            {
                target1.Value = x;
                target2.Value = x;
                target3.Value = x;
            });
            yield return new WaitForSeconds(0.6f);
            Assert.AreApproximatelyEqual(target1.Value, endValue);
            Assert.AreApproximatelyEqual(target2.Value, endValue);
            Assert.AreApproximatelyEqual(target3.Value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_BindToProgress()
        {
            var value = 0f;
            var progress = new Progress<float>(x => value = x);
            var endValue = 10f;
            LMotion.Create(0f, endValue, 1f).BindToProgress(progress);
            yield return new WaitForSeconds(1.1f);
            Assert.AreApproximatelyEqual(value, endValue);
        }

        class TestClass
        {
            public float Value { get; set; }
        }
    }
}