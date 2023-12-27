using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class CallbackTest
    {
        [UnityTest]
        public IEnumerator Test_OnComplete()
        {
            var completed = false;
            LMotion.Create(0f, 10f, 2f)
                .WithOnComplete(() => completed = true)
                .RunWithoutBinding();
            yield return new WaitForSeconds(2.1f);
            Assert.IsTrue(completed);
        }

        [UnityTest]
        public IEnumerator Test_CreateOnCallback()
        {
            var created = false;
            var completed = false;
            LMotion.Create(0f, 10f, 1f)
                .Bind(x =>
                {
                    if (x > 5f && !created)
                    {
                        LMotion.Create(0f, 10f, 1f)
                            .WithOnComplete(() => completed = true)
                            .RunWithoutBinding();
                        created = true;
                    }
                });
            yield return new WaitForSeconds(2.1f);
            Assert.IsTrue(created);
            Assert.IsTrue(completed);
        }

        [UnityTest]
        public IEnumerator Test_CompleteOnCallback_Self()
        {
            MotionHandle handle = default;
            handle = LMotion.Create(0f, 10f, 1f)
                .WithOnComplete(() => handle.Complete())
                .RunWithoutBinding();
            yield return handle.ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_CompleteOnCallback_Other()
        {
            MotionHandle otherHandle = LMotion.Create(0f, 10f, 5f).RunWithoutBinding();
            LMotion.Create(0f, 10f, 0.5f)
                .WithOnComplete(() => otherHandle.Complete())
                .RunWithoutBinding();
            
            yield return otherHandle.ToYieldInteraction();
        }
    }
}