using System;
using System.Threading;
using System.Threading.Tasks;
using LitMotion.Extensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class ValueTaskTest
    {
        [Test]
        public async Task Test_ToValueTask()
        {
            var value = 0f;
            await LMotion.Create(0f, 10f, 0.5f).Bind(x => value = x).ToValueTask();
            Assert.That(value, Is.EqualTo(10f));
        }

        [Test]
        public async Task Test_ToValueTask_AsTask()
        {
            var value = 0f;
            await LMotion.Create(0f, 10f, 0.5f).Bind(x => value = x).ToValueTask().AsTask();
            Assert.That(value, Is.EqualTo(10f));
        }

        [Test]
        public async Task Test_AwaitManyTimes()
        {
            var value = 0f;
            var startValue = 0f;
            var endValue = 10f;

            for (int i = 0; i < 50; i++)
            {
                await LMotion.Create(startValue, endValue, 0.1f)
                    .Bind(x => value = x)
                    .ToValueTask();
                Assert.That(value, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            }
        }

        [Test]
        public async Task Test_CancelAwait()
        {
            var canceled = false;

            var source = new CancellationTokenSource();
            source.CancelAfter(500);

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();
            try
            {
                await handle.ToValueTask(source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(canceled);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task Test_WithCanceledToken()
        {
            var canceled = false;

            var source = new CancellationTokenSource();
            source.Cancel();

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();
            try
            {
                await handle.ToValueTask(source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(canceled);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task Test_CancelWhileAwait()
        {
            var handle = LMotion.Create(0f, 10f, 1f).BindToUnityLogger();

            _ = LMotion.Create(0f, 1f, 0.2f)
                .WithOnComplete(() => handle.Cancel())
                .RunWithoutBinding();

            try
            {
                await handle.ToValueTask();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task Test_CancelWhileAwait_WithCancelOnError()
        {
            LogAssert.ignoreFailingMessages = true;

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithCancelOnError()
                .Bind(x =>
                {
                    if (x > 5f) throw new Exception("Test");
                });

            try
            {
                await handle.ToValueTask();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            Assert.Fail();
        }
    }
}
