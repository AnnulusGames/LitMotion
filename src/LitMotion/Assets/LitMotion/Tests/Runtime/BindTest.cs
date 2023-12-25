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
            LMotion.Create(0f, endValue, 1f).BindWithState(target, (x, target) => target.Value = x);
            yield return new WaitForSeconds(1.1f);
            Assert.AreApproximatelyEqual(target.Value, endValue);
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