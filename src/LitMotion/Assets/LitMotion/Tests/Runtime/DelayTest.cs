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
            var t = Time.timeAsDouble;
            yield return LMotion.Create(0f, 1f, 0.5f)
                .WithDelay(0.5f)
                .BindToUnityLogger()
                .ToYieldInteraction();
            Assert.That(Time.timeAsDouble - t, Is.GreaterThan(0.95).And.LessThan(1.1));
        }

        [UnityTest]
        public IEnumerator Test_Delay_WithZeroDuration()
        {
            var t = Time.timeAsDouble;
            yield return LMotion.Create(0f, 1f, 0f)
                .WithDelay(1f)
                .BindToUnityLogger()
                .ToYieldInteraction();
            Assert.That(Time.timeAsDouble - t, Is.GreaterThan(0.95).And.LessThan(1.1));
        }

        [UnityTest]
        public IEnumerator Test_Delay_EveryLoop()
        {
            var t = Time.timeAsDouble;
            yield return LMotion.Create(0f, 1f, 0.5f)
                .WithLoops(2)
                .WithDelay(0.5f, DelayType.EveryLoop)
                .BindToUnityLogger()
                .ToYieldInteraction();
            Assert.That(Time.timeAsDouble - t, Is.GreaterThan(1.95).And.LessThan(2.1));
        }

        [UnityTest]
        public IEnumerator Test_Delay_EveryLoop_WithZeroDuration()
        {
            var t = Time.timeAsDouble;
            yield return LMotion.Create(0f, 1f, 0f)
                .WithLoops(3)
                .WithDelay(0.5f, DelayType.EveryLoop)
                .BindToUnityLogger()
                .ToYieldInteraction();
            Assert.That(Time.timeAsDouble - t, Is.GreaterThan(1.45).And.LessThan(1.6));
        }
    }
}