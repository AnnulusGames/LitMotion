using System;
using System.Collections;
using System.Runtime.CompilerServices;
using LitMotion.Adapters;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

namespace LitMotion.Tests.Benchmark
{
    public class BindBenchmark
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
        public unsafe void Benchmark_Bind_Pointer_Startup()
        {
            MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(state), (Action<float, int>)(static (x, state) => DoNothing(x, state)), &BindAction)
                        .AddTo(handles);
                });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Box_StartUp()
        {
            MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(state), (x, box) =>
                        {
                            DoNothing(x, box.Value);
                        })
                        .AddTo(handles);
                });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_ActionWithState_StartUp()
        {
            MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(new ActionWithState<float, int>(state, static (x, state) => DoNothing(x, state)), (x, state) => state.Invoke(x))
                        .AddTo(handles);
                });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_WrappedAction_StartUp()
        {
            MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(state), static (x, state) => DoNothing(x, state), (float x, Box<int> box, Action<float, int> action) => action(x, box.Value))
                        .AddTo(handles);
                });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Closure_Startup()
        {
            MeasureStartUp(() =>
            {
                int state = 0;
                LMotion.Create(0f, 1f, 3f)
                    .Bind(x =>
                    {
                        DoNothing(x, state);
                    })
                    .AddTo(handles);
            });
        }

        [UnityTest]
        [Performance]
        public unsafe IEnumerator Benchmark_Bind_Pointer_Update()
        {
            return MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), (Action<float, int>)(static (x, state) => DoNothing(x, state)), &BindAction)
                        .AddTo(handles);
                }
            });
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_Box_Update()
        {
            return MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), (x, state) =>
                        {
                            DoNothing(x, state.Value);
                        })
                        .AddTo(handles);
                }
            });
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_ActionWithState_Update()
        {
            return MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(new ActionWithState<float, int>(i, static (x, state) => DoNothing(x, state)), (x, action) => action.Invoke(x))
                        .AddTo(handles);
                }
            });
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_WrappedAction_Update()
        {
            return MeasureUpdate(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), static (x, state) => DoNothing(x, state), (float x, Box<int> box, Action<float, int> action) => action(x, box.Value))
                        .AddTo(handles);
                }
            });
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_Closure_Update()
        {
            return MeasureUpdate(() =>
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
            });
        }

        [Test]
        [Performance]
        public unsafe void Benchmark_Bind_Pointer_GC()
        {
            MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), (Action<float, int>)(static (x, state) => DoNothing(x, state)), &BindAction)
                        .AddTo(handles);
                }
            });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Box_GC()
        {
            MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), (x, i) =>
                        {
                            DoNothing(x, i.Value);
                        })
                        .AddTo(handles);
                }
            });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_WrappedAction_GC()
        {
            MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), static (x, state) => DoNothing(x, state), (float x, Box<int> box, Action<float, int> action) => action(x, box.Value))
                        .AddTo(handles);
                }
            });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_ActionWithState_GC()
        {
            MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(new ActionWithState<float, int>(i, static (x, state) => DoNothing(x, state)), (x, action) => action.Invoke(x))
                        .AddTo(handles);
                }
            });
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Closure_GC()
        {
            MeasureGC(() =>
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
            });
        }

        void MeasureStartUp(Action action)
        {
            Measure.Method(action)
                .WarmupCount(10)
                .IterationsPerMeasurement(10000)
                .MeasurementCount(10)
                .CleanUp(() =>
                {
                    handles.Cancel();
                })
                .Run();
        }

        IEnumerator MeasureUpdate(Action setUp)
        {
            setUp();

            return Measure.Frames()
                .WarmupCount(5)
                .MeasurementCount(50)
                .Run();
        }

        void MeasureGC(Action action)
        {
            GC.Collect();
            var prev = GC.GetTotalMemory(true);
            action();
            var current = GC.GetTotalMemory(true);
            Measure.Custom(new SampleGroup("GC.Alloc", SampleUnit.Byte), current - prev);
            handles.Cancel();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        static void DoNothing(float value, int state) { }

        static void BindAction(float value, Box<int> state, Action<float, int> action)
        {
            action(value, state.Value);
        }

    }

    internal sealed class ActionWithState<TValue, TState>
        where TValue : unmanaged
        where TState : struct
    {
        public ActionWithState(TState state, Action<TValue, TState> action)
        {
            this.state = state;
            this.action = action;
        }

        readonly TState state;
        readonly Action<TValue, TState> action;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Invoke(TValue value)
        {
            action(value, state);
        }
    }
}