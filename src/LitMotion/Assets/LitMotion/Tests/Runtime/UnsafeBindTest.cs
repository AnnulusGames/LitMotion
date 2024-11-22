using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class UnsafeBindTest
    {
        [UnityTest]
        public IEnumerator Test_Bind_Pointer()
        {
            var endValue = 10f;
            Create(endValue);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(sharedValue, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Bind_Pointer_WithState_1()
        {
            var target1 = new TestClass();
            var endValue = 10f;
            Create(endValue, target1);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(target1.Value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Bind_Pointer_WithState_2()
        {
            var target1 = new TestClass();
            var target2 = new TestClass();
            var endValue = 10f;
            Create(endValue, target1, target2);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(target1.Value, endValue);
            Assert.AreEqual(target2.Value, endValue);
        }

        [UnityTest]
        public IEnumerator Test_Bind_Pointer_WithState_3()
        {
            var target1 = new TestClass();
            var target2 = new TestClass();
            var target3 = new TestClass();
            var endValue = 10f;
            Create(endValue, target1, target2, target3);
            yield return new WaitForSeconds(1.1f);
            Assert.AreEqual(target1.Value, endValue);
            Assert.AreEqual(target2.Value, endValue);
            Assert.AreEqual(target3.Value, endValue);
        }

        static float sharedValue;

        static unsafe MotionHandle Create(float endValue)
        {
            return LMotion.Create(0f, endValue, 1f).Bind(&BindMethod);
        }

        static unsafe MotionHandle Create(float endValue, TestClass state0)
        {
            return LMotion.Create(0f, endValue, 1f).Bind(state0, &BindMethod);
        }

        static unsafe MotionHandle Create(float endValue, TestClass state0, TestClass state1)
        {
            return LMotion.Create(0f, endValue, 1f).Bind(state0, state1, &BindMethod);
        }

        static unsafe MotionHandle Create(float endValue, TestClass state0, TestClass state1, TestClass state2)
        {
            return LMotion.Create(0f, endValue, 1f).Bind(state0, state1, state2, &BindMethod);
        }

        static void BindMethod(float value)
        {
            sharedValue = value;
        }

        static void BindMethod(float value, TestClass state0)
        {
            state0.Value = value;
        }

        static void BindMethod(float value, TestClass state0, TestClass state1)
        {
            state0.Value = value;
            state1.Value = value;
        }

        static void BindMethod(float value, TestClass state0, TestClass state1, TestClass state2)
        {
            state0.Value = value;
            state1.Value = value;
            state2.Value = value;
        }

        class TestClass
        {
            public float Value { get; set; }
        }
    }
}