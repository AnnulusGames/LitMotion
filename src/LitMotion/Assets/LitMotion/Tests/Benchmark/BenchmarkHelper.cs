using System;
using System.Collections;
using Unity.PerformanceTesting;

namespace LitMotion.Tests.Benchmark
{
    public static class BenchmarkHelper
    {
        public static void MeasureStartUp(Action action, Action cleanUp)
        {
            Measure.Method(action)
                .WarmupCount(10)
                .IterationsPerMeasurement(10000)
                .MeasurementCount(10)
                .CleanUp(() =>
                {
                    cleanUp();
                })
                .Run();
        }

        public static IEnumerator MeasureUpdate(Action setUp)
        {
            setUp();

            return Measure.Frames()
                .WarmupCount(5)
                .MeasurementCount(200)
                .Run();
        }

        public static void MeasureGC(Action action)
        {
            GC.Collect();
            var prev = GC.GetTotalMemory(true);
            action();
            var current = GC.GetTotalMemory(true);
            Measure.Custom(new SampleGroup("GC.Alloc", SampleUnit.Byte), current - prev);
        }
    }
}