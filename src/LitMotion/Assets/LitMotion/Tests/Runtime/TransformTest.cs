using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;
using LitMotion.Extensions;

namespace LitMotion.Tests.Runtime
{
    public class TransformTest
    {
        Transform target;
        const float Duration = 1f;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            target = new GameObject("Target").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(target.gameObject);
        }

        [UnityTest]
        public IEnumerator Test_BindToPosition()
        {
            var endValue = Vector3.one;
            yield return LMotion.Create(Vector3.zero, endValue, Duration).BindToPosition(target).ToYieldInstruction();
            Assert.That(target.position, Is.EqualTo(endValue).Using(Vector3EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionX()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToPositionX(target).ToYieldInstruction();
            Assert.That(target.position.x, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionY()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToPositionY(target).ToYieldInstruction();
            Assert.That(target.position.y, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionZ()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToPositionZ(target).ToYieldInstruction();
            Assert.That(target.position.z, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionXY()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToPositionXY(target).ToYieldInstruction();
            Assert.That(new Vector2(target.position.x, target.position.y), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionXZ()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToPositionXZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.position.x, target.position.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToPositionYZ()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToPositionYZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.position.y, target.position.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPosition()
        {
            var endValue = Vector3.one;
            yield return LMotion.Create(Vector3.zero, endValue, Duration).BindToLocalPosition(target).ToYieldInstruction();
            Assert.That(target.localPosition, Is.EqualTo(endValue).Using(Vector3EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionX()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalPositionX(target).ToYieldInstruction();
            Assert.That(target.localPosition.x, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionY()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalPositionY(target).ToYieldInstruction();
            Assert.That(target.localPosition.y, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionZ()
        {
            var endValue = 10f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalPositionZ(target).ToYieldInstruction();
            Assert.That(target.localPosition.z, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionXY()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalPositionXY(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localPosition.x, target.localPosition.y), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionXZ()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalPositionXZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localPosition.x, target.localPosition.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalPositionYZ()
        {
            var endValue = Vector2.one;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalPositionYZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localPosition.y, target.localPosition.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToRotation()
        {
            var endValue = Quaternion.Euler(10f, 20f, 30f);
            yield return LMotion.Create(Quaternion.identity, endValue, Duration).BindToRotation(target).ToYieldInstruction();
            Assert.That(target.rotation, Is.EqualTo(endValue).Using(QuaternionEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalRotation()
        {
            var endValue = Quaternion.Euler(10f, 20f, 30f);
            yield return LMotion.Create(Quaternion.identity, endValue, Duration).BindToLocalRotation(target).ToYieldInstruction();
            Assert.That(target.localRotation, Is.EqualTo(endValue).Using(QuaternionEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAngles()
        {
            var endValue = Vector3.one * 10f;
            yield return LMotion.Create(Vector3.zero, endValue, Duration).BindToEulerAngles(target).ToYieldInstruction();
            Assert.That(target.eulerAngles, Is.EqualTo(endValue).Using(Vector3EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesX()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToEulerAnglesX(target).ToYieldInstruction();
            Assert.That(target.eulerAngles.x, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesY()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToEulerAnglesY(target).ToYieldInstruction();
            Assert.That(target.eulerAngles.y, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesZ()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToEulerAnglesZ(target).ToYieldInstruction();
            Assert.That(target.eulerAngles.z, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesXY()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToEulerAnglesXY(target).ToYieldInstruction();
            Assert.That(new Vector2(target.eulerAngles.x, target.eulerAngles.y), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesXZ()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToEulerAnglesXZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.eulerAngles.x, target.eulerAngles.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToEulerAnglesYZ()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToEulerAnglesYZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.eulerAngles.y, target.eulerAngles.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAngles()
        {
            var endValue = Vector3.one * 10f;
            yield return LMotion.Create(Vector3.zero, endValue, Duration).BindToLocalEulerAngles(target).ToYieldInstruction();
            Assert.That(target.localEulerAngles, Is.EqualTo(endValue).Using(Vector3EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesX()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToLocalEulerAnglesX(target).ToYieldInstruction();
            Assert.That(target.localEulerAngles.x, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesY()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToLocalEulerAnglesY(target).ToYieldInstruction();
            Assert.That(target.localEulerAngles.y, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesZ()
        {
            var endValue = 10f;
            yield return LMotion.Create(0f, endValue, Duration).BindToLocalEulerAnglesZ(target).ToYieldInstruction();
            Assert.That(target.localEulerAngles.z, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesXY()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalEulerAnglesXY(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localEulerAngles.x, target.localEulerAngles.y), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesXZ()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalEulerAnglesXZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localEulerAngles.x, target.localEulerAngles.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalEulerAnglesYZ()
        {
            var endValue = Vector2.one * 10f;
            yield return LMotion.Create(Vector2.zero, endValue, Duration).BindToLocalEulerAnglesYZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localEulerAngles.y, target.localEulerAngles.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScale()
        {
            var endValue = Vector3.one * 2f;
            yield return LMotion.Create(Vector3.one, endValue, Duration).BindToLocalScale(target).ToYieldInstruction();
            Assert.That(target.localScale, Is.EqualTo(endValue).Using(Vector3EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleX()
        {
            var endValue = 2f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalScaleX(target).ToYieldInstruction();
            Assert.That(target.localScale.x, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleY()
        {
            var endValue = 2f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalScaleY(target).ToYieldInstruction();
            Assert.That(target.localScale.y, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleZ()
        {
            var endValue = 2f;
            yield return LMotion.Create(1f, endValue, Duration).BindToLocalScaleZ(target).ToYieldInstruction();
            Assert.That(target.localScale.z, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleXY()
        {
            var endValue = Vector2.one * 2f;
            yield return LMotion.Create(Vector2.one, endValue, Duration).BindToLocalScaleXY(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localScale.x, target.localScale.y), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleXZ()
        {
            var endValue = Vector2.one * 2f;
            yield return LMotion.Create(Vector2.one, endValue, Duration).BindToLocalScaleXZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localScale.x, target.localScale.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }

        [UnityTest]
        public IEnumerator Test_BindToLocalScaleYZ()
        {
            var endValue = Vector2.one * 2f;
            yield return LMotion.Create(Vector2.one, endValue, Duration).BindToLocalScaleYZ(target).ToYieldInstruction();
            Assert.That(new Vector2(target.localScale.y, target.localScale.z), Is.EqualTo(endValue).Using(Vector2EqualityComparer.Instance));
        }
    }
}