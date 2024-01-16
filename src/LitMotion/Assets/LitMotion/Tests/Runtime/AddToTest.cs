using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class AddToTest
    {
        [UnityTest]
        public IEnumerator Test_AddTo()
        {
            var canceled = false;
            var obj = new GameObject("Target");
            var handle = LMotion.Create(0f, 1f, 2f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding()
                .AddTo(obj);
            yield return new WaitForSeconds(0.1f);
            Object.DestroyImmediate(obj);
            Assert.IsTrue(canceled);
        }
    }
}