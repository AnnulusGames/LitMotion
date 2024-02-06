using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Sequences.Tests.Runtime
{
    public class SequenceEventsTest
    {
        [Test]
        public void Test_OnCancel()
        {
            var motionCanceled = false;
            var sequenceCanceled = false;
            var sequenceCompleted = false;

            var sequence = MotionSequence.CreateBuilder()
                .Append(() => LMotion.Create(0f, 1f, 1f).WithOnCancel(() => motionCanceled = true).RunWithoutBinding())
                .Build();

            sequence.OnCanceled += () => sequenceCanceled = true;
            sequence.OnCompleted += () => sequenceCompleted = true;

            for (int i = 0; i < 10; i++)
            {
                sequence.Play();
                sequence.Cancel();

                Assert.That(motionCanceled, Is.True);
                Assert.That(sequenceCanceled, Is.True);
                Assert.That(sequenceCompleted, Is.False);

                motionCanceled = sequenceCanceled = sequenceCompleted = false;
            }
        }

        [Test]
        public void Test_OnComplete()
        {
            var motionCompleted = false;
            var sequenceCanceled = false;
            var sequenceCompleted = false;
            var value = 0f;

            var sequence = MotionSequence.CreateBuilder()
                .Append(() => LMotion.Create(0f, 1f, 1f).WithOnComplete(() => motionCompleted = true).Bind(x => value = x))
                .Build();

            sequence.OnCanceled += () => sequenceCanceled = true;
            sequence.OnCompleted += () => sequenceCompleted = true;

            for (int i = 0; i < 10; i++)
            {
                sequence.Play();
                sequence.Complete();

                Assert.That(motionCompleted, Is.True);
                Assert.That(sequenceCanceled, Is.False);
                Assert.That(sequenceCompleted, Is.True);
                Assert.That(value, Is.EqualTo(1f).Using(FloatEqualityComparer.Instance));

                motionCompleted = sequenceCanceled = sequenceCompleted = false;
                value = 0f;
            }
        }
    }
}
