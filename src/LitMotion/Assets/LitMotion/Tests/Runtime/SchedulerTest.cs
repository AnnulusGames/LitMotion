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
        public IEnumerator Test_Scheduler_Initialization()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.Initialization)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_Initialization_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.InitializationIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_Initialization_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.InitializationRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_EarlyUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.EarlyUpdate)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_EarlyUpdate_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.EarlyUpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_EarlyUpdate_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.EarlyUpdateRealtime)
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

        [UnityTest]
        public IEnumerator Test_Scheduler_Update()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.Update)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_Update_WithHalfTimeScale()
        {
            Time.timeScale = 0.5f;
            var t = Time.unscaledTimeAsDouble;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.Update)
                .RunWithoutBinding()
                .ToYieldInteraction();
            Assert.That(Time.unscaledTimeAsDouble - t, Is.GreaterThan(Duration * 2f));
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
        public IEnumerator Test_Scheduler_PreLateUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PreLateUpdate)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_PreLateUpdate_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PreLateUpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_PreLateUpdate_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PreLateUpdateRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_PostLateUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PostLateUpdate)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_PostLateUpdate_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PostLateUpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_PostLateUpdate_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.PostLateUpdateRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_TimeUpdate()
        {
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.TimeUpdate)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_TimeUpdate_IgnoreTimeScale()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.TimeUpdateIgnoreTimeScale)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }

        [UnityTest]
        public IEnumerator Test_Scheduler_TimeUpdate_Realtime()
        {
            Time.timeScale = 0;
            yield return LMotion.Create(0f, 10f, Duration)
                .WithScheduler(MotionScheduler.TimeUpdateRealtime)
                .RunWithoutBinding()
                .ToYieldInteraction();
        }
    }
}