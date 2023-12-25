#if LITMOTION_TEST_UNITASK
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

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
    }
}
#endif