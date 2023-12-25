using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class SchedulerTest
    {
        [UnityTest]
        public IEnumerator Test_Scheduler_FixedUpdate()
        {
            yield return LMotion.Create(0f, 10f, 1f)
                .WithScheduler(MotionScheduler.FixedUpdate)
                .Bind(x =>
                {
                    Assert.IsTrue(Time.inFixedTimeStep);
                })
                .ToYieldInteraction();
        }
    }
}