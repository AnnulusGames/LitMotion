using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class DelayTest
    {
        [UnityTest]
        public IEnumerator Test_Delay()
        {
            var t = Time.time;
            yield return LMotion.Create(0f, 1f, 0.5f)
                .WithDelay(0.5f)
                .RunWithoutBinding()
                .ToYieldInteraction();
            Assert.IsTrue(Time.time - t >= 1f);
        }

        [UnityTest]
        public IEnumerator Test_Delay_WithZeroDuration()
        {
            var t = Time.time;
            yield return LMotion.Create(0f, 1f, 0f)
                .WithDelay(1f)
                .RunWithoutBinding()
                .ToYieldInteraction();
            Assert.IsTrue(Time.time - t >= 1f);
        }
    }
}