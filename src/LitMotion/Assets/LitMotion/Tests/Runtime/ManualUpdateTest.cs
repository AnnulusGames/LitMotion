using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class ManualUpdateTest
    {
        [Test]
        public void Test_FloatMotion()
        {
            ManualMotionDispatcher.Reset();

            var value = 0f;
            var endValue = 10f;
            var handle = LMotion.Create(value, endValue, 2f)
                .WithScheduler(MotionScheduler.Manual)
                .Bind(x =>
                {
                    value = x;
                    Debug.Log(x);
                });

            while (handle.IsActive())
            {
                var deltaTime = 0.1f;
                ManualMotionDispatcher.Update(deltaTime);
            }

            Assert.That(value, Is.EqualTo(endValue).Using(FloatEqualityComparer.Instance));
        }
    }
}