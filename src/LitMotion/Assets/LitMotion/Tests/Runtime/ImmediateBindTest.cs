using NUnit.Framework;
using UnityEngine;

namespace LitMotion.Tests.Runtime
{
    public class ImmediateBindTest
    {
        [Test]
        public void Test_ImmediateBind()
        {
            var value = 0f;

            var motion = LMotion.Create(1f, 0f, 1f)
                .WithImmediateBind(false)
                .Bind(x => value = x);

            motion.Cancel();

            Assert.That(value, Is.EqualTo(0f));

            value = 0f;

            motion = LMotion.Create(1f, 0f, 1f)
                .WithImmediateBind()
                .Bind(x => value = x);

            motion.Cancel();

            Assert.That(value, Is.EqualTo(1f));
        }

        [Test]
        public void Test_ImmediateBind_AnimationCurve()
        {
            var curve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

            var value = 0f;
            var motion = LMotion.Create(0f, 1f, 1f)
                .WithEase(curve)
                .WithImmediateBind()
                .Bind(x => value = x);

            motion.Cancel();

            Assert.That(value, Is.EqualTo(1f));
        }
    }
}
