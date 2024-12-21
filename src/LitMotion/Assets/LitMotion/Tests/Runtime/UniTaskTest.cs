#if LITMOTION_TEST_UNITASK
using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion.Extensions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.TestTools.Utils;

namespace LitMotion.Tests.Runtime
{
    public class UniTaskTest
    {
        readonly CancellationTokenSource cts = new();

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            cts.Cancel();
        }

        [UnityTest]
        public IEnumerator Test_ToUniTask() => UniTask.ToCoroutine(async () =>
        {
            await LMotion.Create(0f, 10f, 1f)
                .BindToUnityLogger()
                .ToUniTask();
        });

        [UnityTest]
        public IEnumerator Test_ToUniTask_CompleteAndCancelAwait() => UniTask.ToCoroutine(async () =>
        {
            var completed = false;
            var source = new CancellationTokenSource();
            source.CancelAfterSlim(TimeSpan.FromSeconds(0.5f));

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnComplete(() => completed = true)
                .RunWithoutBinding();

            try
            {
                await handle.ToUniTask(CancelBehavior.Complete, source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsFalse(handle.IsActive());
                Assert.IsTrue(completed);
                return;
            }

            Assert.Fail();
        });

        [UnityTest]
        public IEnumerator Test_ToUniTask_CancelAwait() => UniTask.ToCoroutine(async () =>
        {
            var completed = false;
            var canceled = false;
            var source = new CancellationTokenSource();
            source.CancelAfterSlim(TimeSpan.FromSeconds(0.5f));

            var handle = LMotion.Create(0f, 10f, 1f)
                .WithOnComplete(() => completed = true)
                .WithOnCancel(() => canceled = true)
                .RunWithoutBinding();

            try
            {
                await handle.ToUniTask(CancelBehavior.Cancel, source.Token);
            }
            catch (OperationCanceledException)
            {
                Assert.IsFalse(handle.IsActive());
                Assert.IsTrue(canceled);
                Assert.IsFalse(completed);

                return;
            }

            Assert.Fail();
        });

        [UnityTest]
        public IEnumerator Test_BindToAsyncReactiveProperty() => UniTask.ToCoroutine(async () =>
        {
            var reactiveProperty = new AsyncReactiveProperty<float>(0f);
            _ = LMotion.Create(0f, 10f, 1f)
                .WithOnComplete(() => reactiveProperty.Dispose())
                .BindToAsyncReactiveProperty(reactiveProperty);

            await foreach (var i in reactiveProperty.WithoutCurrent())
            {
                Debug.Log(i);
            }
        });

        [UnityTest]
        public IEnumerator Test_AwaitManyTimes() => UniTask.ToCoroutine(async () =>
        {
            var value = 0f;
            var startValue = 0f;
            var endValue = 10f;

            for (int i = 0; i < 50; i++)
            {
                await LMotion.Create(startValue, endValue, 0.1f)
                    .Bind(x => value = x)
                    .ToUniTask();
                Assert.That(value, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            }
        });

        [UnityTest]
        public IEnumerator Test_CancelWhileAwait() => UniTask.ToCoroutine(async () =>
        {
            var handle = LMotion.Create(0f, 10f, 1f).BindToUnityLogger();
            DelayedCall(0.2f, () => handle.Cancel()).Forget();
            try
            {
                await handle.ToUniTask();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            Assert.Fail();
        });

        [UnityTest]
        public IEnumerator Test_CancelWhileAwait_WithoutCancelAwaitOnMotionCanceled() => UniTask.ToCoroutine(async () =>
        {
            var handle = LMotion.Create(0f, 10f, 1f).BindToUnityLogger();
            DelayedCall(0.2f, () => handle.Cancel()).Forget();
            try
            {
                await handle.ToUniTask(CancelBehavior.Cancel, false);
            }
            catch (OperationCanceledException)
            {
                Assert.Fail();
                return;
            }
        });

        [UnityTest]
        public IEnumerator Test_CancelWhileAwait_WithCancelOnError() => UniTask.ToCoroutine(async () =>
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
                await handle.ToUniTask();
            }
            catch (OperationCanceledException)
            {
                return;
            }
            Assert.Fail();
        });

        [UnityTest]
        public IEnumerator Test_CancelWhileAwaitFollowedByAnother() => UniTask.ToCoroutine(async () =>
        {
            var cancellationTokenSource = new CancellationTokenSource();
            LMotion.Create(0f, 10f, 1.0f)
                .RunWithoutBinding()
                .ToUniTask(cancellationTokenSource.Token)
                .Forget();

            await UniTask.Delay(100);
            cancellationTokenSource.Cancel();

            var canceled = await LMotion.Create(10.0f, 0.0f, 1.0f)
                .RunWithoutBinding()
                .ToUniTask()
                .SuppressCancellationThrow();

            Assert.IsFalse(canceled);
        });

        async UniTaskVoid DelayedCall(float delay, Action action)
        {
            await UniTask.WaitForSeconds(delay);
            action.Invoke();
        }
    }
}
#endif