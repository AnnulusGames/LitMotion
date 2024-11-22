using System.Collections;
using System.Runtime.CompilerServices;
using LitMotion.Adapters;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Benchmark
{
    public class FunctionPointerBenchmark
    {
        Transform transform;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MotionDispatcher.EnsureStorageCapacity<float, NoOptions, FloatMotionAdapter>(200000);

            transform = new GameObject("Test").transform;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Object.Destroy(transform.gameObject);
            transform = null;
        }

        [TearDown]
        public void TearDown()
        {
            MotionDispatcher.Clear();
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Delegate_Update_Float()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(x => DoNothing(x));
                }
            });
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Delegate_Update_Position()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    LMotion.Create(Vector3.zero, Vector3.one, 3f)
                        .Bind(transform, (x, t) => t.position = x);
                }
            });
        }

        [UnityTest]
        [Performance]
        public unsafe IEnumerator Benchmark_FunctionPointer_Update_Float()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(&DoNothing);
                }
            });
        }

        [UnityTest]
        [Performance]
        public unsafe IEnumerator Benchmark_FunctionPointer_Update_Position()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 200000; i++)
                {
                    LMotion.Create(Vector3.zero, Vector3.one, 3f)
                        .Bind(transform, &SetPosition);
                }
            });
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void DoNothing(float value) { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void SetPosition(Vector3 value, Transform t) { t.position = value; }
    }
}
