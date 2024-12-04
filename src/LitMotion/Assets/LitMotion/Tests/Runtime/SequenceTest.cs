using System;
using System.Collections;
using LitMotion.Sequences;
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
                .Schedule();

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
                .Schedule();

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
                .Schedule();

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
                .Schedule();

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
                .Schedule();

            var sequence2 = LSequence.Create()
                .Append(LMotion.Create(0f, 1f, 0.2f).Bind(v => y = v))
                .Append(LMotion.Create(1f, 0f, 0.2f).Bind(v => y = v))
                .Schedule();

            var handle = LSequence.Create()
                .Append(sequence1)
                .Append(sequence2)
                .Schedule();

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

        [UnityTest]
        public IEnumerator Test_Error_AppendRunningMotion()
        {
            var handle = LMotion.Create(0f, 1f, 10f).RunWithoutBinding();
            yield return null;

            Assert.Throws<ArgumentException>(() =>
            {
                LSequence.Create()
                    .Append(handle)
                    .Schedule();
            }, "Cannot add a running motion to a sequence.");
        }

        [Test]
        public void Test_Error_UseMotionHandleInSequence()
        {
            var handle = LMotion.Create(0f, 1f, 10f).RunWithoutBinding();
            LSequence.Create()
                .Append(handle)
                .Schedule();

            Assert.Throws<InvalidOperationException>(() =>
            {
                handle.Complete();
            });
        }
    }
}
