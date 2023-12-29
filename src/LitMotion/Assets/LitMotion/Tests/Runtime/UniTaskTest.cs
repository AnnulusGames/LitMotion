#if LITMOTION_TEST_UNITASK
using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
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
                .BindToUnityLogger();
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
                    .Bind(x => value = x);
                Assert.That(value, Is.EqualTo(10f).Using(FloatEqualityComparer.Instance));
            }
        });

        [UnityTest]
        public IEnumerator Test_CancelWhileAwait() => UniTask.ToCoroutine(async () =>
        {
            var handle = LMotion.Create(0f, 10f, 1f).BindToUnityLogger();
            DelayedCall(0.2f, () => handle.Cancel()).Forget();
            await handle;
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
            await handle;
        });

        async UniTaskVoid DelayedCall(float delay, Action action)
        {
            await UniTask.WaitForSeconds(delay);
            action.Invoke();
        }
    }
}
#endif