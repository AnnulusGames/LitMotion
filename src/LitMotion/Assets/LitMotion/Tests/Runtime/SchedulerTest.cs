using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class SchedulerTest
    {
        const float Duration = 0.2f;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1;
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_Update_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_Update_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.UpdateRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_LateUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.LateUpdate)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_LateUpdate_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.LateUpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_LateUpdate_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.LateUpdateRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_FixedUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.FixedUpdate)
                .Bind(x =>
                {
                    Assert.IsTrue(Time.inFixedTimeStep);
                })
                .ToYieldInteraction();
        }
    }
}