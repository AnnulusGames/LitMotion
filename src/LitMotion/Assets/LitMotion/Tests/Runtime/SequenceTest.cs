using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class SequenceTest
    {
        [UnityTest]
        public IEnumerator Test_Append()
        {
            var x = 0f;
            var y = 0f;

            LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => x = v))
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => y = v))
                .Run();

            yield return new WaitForSeconds(0.21f);
            Assert.That(x, Is.EqualTo(1f));
            yield return new WaitForSeconds(0.21f);
            Assert.That(y, Is.EqualTo(1f));
        }

        [UnityTest]
        public IEnumerator Test_Join()
        {
            var x = 0f;
            var y = 0f;

            LSequence.Create()
                .Join(LMotion.Create(0f, 1f, 0.2f).Bind(v => x = v))
                .Join(LMotion.Create(0f, 1f, 0.2f).Bind(v => y = v))
                .Run();

            yield return new WaitForSeconds(0.21f);
            Assert.That(x, Is.EqualTo(1f));
            Assert.That(y, Is.EqualTo(1f));
        }

        [UnityTest]
        public IEnumerator Test_Insert()
        {
            var x = 0f;
            var y = 0f;

            LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => x = v))
                .Insert(0.1f, LMotion.Create(0f, 1f, 0.2f).Bind(v => y = v))
                .Run();

            yield return new WaitForSeconds(0.21f);
            Assert.That(x, Is.EqualTo(1f));
            yield return new WaitForSeconds(0.11f);
            Assert.That(y, Is.EqualTo(1f));
        }

        [UnityTest]
        public IEnumerator Test_AppendInterval()
        {
            var x = 0f;

            LSequence.Create()
                .AppendInterval(0.2f)
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => x = v))
                .Run();

            yield return new WaitForSeconds(0.19f);
            Assert.That(x, Is.EqualTo(0f));
            yield return new WaitForSeconds(0.22f);
            Assert.That(x, Is.EqualTo(1f));
        }

        [UnityTest]
        public IEnumerator Test_Nested_Sequence()
        {
            var x = 0f;
            var y = 0f;

            var sequence1 = LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => x = v))
                .Append(LMotion.Create(1f, 0f, 0.2f).Bind(v => x = v))
                .Run();

            var sequence2 = LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => y = v))
                .Append(LMotion.Create(1f, 0f, 0.2f).Bind(v => y = v))
                .Run();

            var handle = LSequence.Create()
                .Append(sequence1)
                .Append(sequence2)
                .Run();

            yield return new WaitForSeconds(0.2f);
            Assert.That(x, Is.GreaterThan(0.9f));
            Assert.That(y, Is.EqualTo(0f));
            yield return new WaitForSeconds(0.2f);
            Assert.That(x, Is.EqualTo(0f));
            Assert.That(y, Is.LessThan(0.1f));
            yield return new WaitForSeconds(0.2f);
            Assert.That(x, Is.EqualTo(0f));
            Assert.That(y, Is.GreaterThan(0.9f));
            yield return new WaitForSeconds(0.2f);
            Assert.That(x, Is.EqualTo(0f));
            Assert.That(y, Is.EqualTo(0f));
        }

        [Test]
        public void Test_Complete()
        {
            var flag1 = false;
            var flag2 = false;
            var x = 0f;
            var y = 0f;

            var handle = LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 1f)
                    .WithOnComplete(() => flag1 = true)
                    .Bind(v => x = v))
                .Append(LMotion.Create(0f, 1f, 1f)
                    .WithOnComplete(() => flag2 = true)
                    .Bind(v => y = v))
                .Run();

            handle.Complete();

            Assert.IsTrue(flag1);
            Assert.IsTrue(flag2);
            Assert.That(x, Is.EqualTo(1f));
            Assert.That(y, Is.EqualTo(1f));
        }

        [Test]
        public void Test_Cancel()
        {
            var flag1 = false;
            var flag2 = false;

            var handle = LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 1f)
                    .WithOnCancel(() => flag1 = true)
                    .RunWithoutBinding())
                .Append(LMotion.Create(0f, 1f, 1f)
                    .WithOnCancel(() => flag2 = true)
                    .RunWithoutBinding())
                .Run();

            handle.Cancel();

            Assert.IsTrue(flag1);
            Assert.IsTrue(flag2);
        }


        [UnityTest]
        public IEnumerator Test_Error_AppendRunningMotion()
        {
            var handle = LMotion.Create(0f, 1f, 10f).RunWithoutBinding();
            yield return null;

            Assert.Throws<ArgumentException>(() =>
            {
                LSequence.Create()
                    .Append(handle)
                    .Run();
            }, "Cannot add a running motion to a sequence.");
        }

        [Test]
        public void Test_Error_UseMotionHandleInSequence()
        {
            var handle = LMotion.Create(0f, 1f, 10f).RunWithoutBinding();
            LSequence.Create()
                .Append(handle)
                .Run();

            Assert.Throws<InvalidOperationException>(() =>
            {
                handle.Complete();
            }, "Cannot access the motion in sequence.");

            Assert.Throws<InvalidOperationException>(() =>
            {
                handle.Cancel();
            }, "Cannot access the motion in sequence.");

            Assert.Throws<InvalidOperationException>(() =>
            {
                handle.Time = 0;
            }, "Cannot access the motion in sequence.");
        }
    }
}
