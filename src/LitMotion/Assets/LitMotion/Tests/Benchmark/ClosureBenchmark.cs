using System.Collections;
using System.Runtime.CompilerServices;
using LitMotion.Adapters;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Benchmark
{
    public class ClosureBenchmark
    {
        CompositeMotionHandle handles = new(25000);

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

        [Test]
        [Performance]
        public void Benchmark_Bind_ActionWithState_Startup()
        {
            Measure.Method(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(state, (x, state) =>
                        {
                            DoNothing(x, state);
                        })
                        .AddTo(handles);
                })
                .WarmupCount(10)
                .IterationsPerMeasurement(5000)
                .MeasurementCount(5)
                .CleanUp(() =>
                {
                    handles.Cancel();
                })
                .Run();
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Closure_Startup()
        {
            Measure.Method(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(x =>
                        {
                            DoNothing(x, state);
                        })
                        .AddTo(handles);
                })
                .WarmupCount(10)
                .IterationsPerMeasurement(5000)
                .MeasurementCount(5)
                .CleanUp(() =>
                {
                    handles.Cancel();
                })
                .Run();
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_ActionWithState_Update()
        {
            for (int i = 0; i < 10000; i++)
            {
                LMotion.Create(0f, 1f, 3f)
                    .Bind(i, (x, state) =>
                    {
                        DoNothing(x, state);
                    })
                    .AddTo(handles);
            }

            yield return Measure.Frames()
                .WarmupCount(5)
                .MeasurementCount(20)
                .Run();
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_Closure_Update()
        {
            for (int i = 0; i < 10000; i++)
            {
                LMotion.Create(0f, 1f, 3f)
                    .Bind(x =>
                    {
                        DoNothing(x, i);
                    })
                    .AddTo(handles);
            }

            yield return Measure.Frames()
                .WarmupCount(5)
                .MeasurementCount(20)
                .Run();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void DoNothing(float value, int state) { }
    }
}