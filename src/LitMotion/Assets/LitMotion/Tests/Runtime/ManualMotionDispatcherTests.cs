using NUnit.Framework;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class ManualMotionDispatcherTests
    {
        [Test]
        public void Test_Update()
        {
            var dispatcher = new ManualMotionDispatcher();

            var x = 0f;
            var isCompleted = false;
            LMotion.Create(0f, 10f, 1f)
                .WithScheduler(dispatcher.Scheduler)
                .WithOnComplete(() => isCompleted = true)
                .Bind(v => x = v);

            dispatcher.Update(0.5);
            Assert.That(x, Is.EqualTo(5f).Using(FloatEqualityComparer.Instance));
            dispatcher.Update(0.5);
            Assert.That(x, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            Assert.That(isCompleted, Is.True);
        }

        [Test]
        public void Test_Time()
        {
            var dispatcher = new ManualMotionDispatcher();

            var x = 0f;
            var handle = LMotion.Create(0f, 10f, 1f)
                .WithScheduler(dispatcher.Scheduler)
                .Bind(v => x = v)
                .Preserve();

            dispatcher.Update(1.0);
            Assert.That(x, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            dispatcher.Time = 0;
            Assert.That(x, Is.EqualTo(0f).Using(FloatEqualityComparer.Instance));

            handle.Cancel();
        }
    }
}