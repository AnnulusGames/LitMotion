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

    }
}