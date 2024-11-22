using System.Collections;
using System.Runtime.CompilerServices;
using LitMotion.Adapters;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Benchmark
{
    public class FunctionPointerBenchmark
    {
        CompositeMotionHandle handles = new(10000);

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            MotionDispatcher.EnsureStorageCapacity<float, NoOptions, FloatMotionAdapter>(64000);
        }

        [TearDown]
        public void TearDown()
        {
            handles.Cancel();
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Delegate_Update()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(x => DoNothing(x))
                        .AddTo(handles);
                }
            });
        }

        [UnityTest]
        [Performance]
        public unsafe IEnumerator Benchmark_FunctionPointer_Update()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(&DoNothing)
                        .AddTo(handles);
                }
            });
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void DoNothing(float value) { }
    }
}
