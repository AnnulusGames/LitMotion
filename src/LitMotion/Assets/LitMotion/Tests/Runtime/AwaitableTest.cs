#if UNITY_2023_1_OR_NEWER
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Runtime
{
    public class AwaitableTest
    {
        [Test]
        public async Task Test_Awaitable()
        {
            var value = 0f;
            var handle = LMotion.Create(0f, 10f, 1f).Bind(x => value = x);
            await handle.ToAwaitable();
            Assert.That(value, Is.EqualTo(10f));
        }

        [Test]
        public async Task Test_Awaitable_Completed()
        {
            await default(MotionHandle).ToAwaitable();
        }

        [Test]
        public async Task Test_Awaitable_CancelAwait()
        {
            var canceled = false;

            var source = new CancellationTokenSource();
            source.CancelAfter(500);

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();
            try
            {
                await handle.ToAwaitable(source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(canceled);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task Test_Awaitable_WithCanceledToken()
        {
            var canceled = false;

            var source = new CancellationTokenSource();
            source.Cancel();

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();
            try
            {
                await handle.ToAwaitable(source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(canceled);
                return;
            }
            Assert.Fail();
        }

        [Test]
        public async Task Test_Awaitable_CancelWhileAwait()
        {
            var canceled = false;

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();

            _ = DelayedCall(0.2f, () => handle.Cancel());

            try
            {
                await handle.ToAwaitable();
            }
            catch (OperationCanceledException)
            {
                Assert.IsTrue(canceled);
                return;
            }
            Assert.Fail();
        }


        [Test]
        public async Task Test_Awaitable_CancelWhileAwait_WithoutCancelAwaitOnMotionCanceled()
        {
            var canceled = false;

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();

            _ = DelayedCall(0.2f, () => handle.Cancel());

            try
            {
                await handle.ToAwaitable(CancelBehavior.Cancel, false);
            }
            catch (OperationCanceledException)
            {
                Assert.Fail();
                return;
            }

            Assert.IsTrue(canceled);
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
                await handle.ToAwaitable();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            Assert.Fail();
        }

        async Awaitable DelayedCall(float delay, Action action)
        {
            await Awaitable.WaitForSecondsAsync(delay);
            action.Invoke();
        }
    }
}
#endif
