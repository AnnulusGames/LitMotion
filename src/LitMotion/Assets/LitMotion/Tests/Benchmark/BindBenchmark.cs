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
        public void Benchmark_Bind_Box_StartUp()
        {
            BenchmarkHelper.MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(state), (x, box) =>
                        {
                            DoNothing(x, box.Value);
                        })
                        .AddTo(handles);
                }, () => handles.Cancel());
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_ActionWithState_StartUp()
        {
            BenchmarkHelper.MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(new ActionWithState<float, int>(state, static (x, state) => DoNothing(x, state)), (x, state) => state.Invoke(x))
                        .AddTo(handles);
                }, () => handles.Cancel());
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_WrappedAction_StartUp()
        {
            BenchmarkHelper.MeasureStartUp(() =>
                {
                    int state = 0;
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(state), static (x, state) => DoNothing(x, state), (float x, Box<int> box, Action<float, int> action) => action(x, box.Value))
                        .AddTo(handles);
                }, () => handles.Cancel());
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Closure_Startup()
        {
            BenchmarkHelper.MeasureStartUp(() =>
            {
                int state = 0;
                LMotion.Create(0f, 1f, 3f)
                    .Bind(x =>
                    {
                        DoNothing(x, state);
                    })
                    .AddTo(handles);
            }, () => handles.Cancel());
        }

        [UnityTest]
        [Performance]
        public IEnumerator Benchmark_Bind_Box_Update()
        {
            return BenchmarkHelper.MeasureUpdate(() =>
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
            return BenchmarkHelper.MeasureUpdate(() =>
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
            return BenchmarkHelper.MeasureUpdate(() =>
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
            return BenchmarkHelper.MeasureUpdate(() =>
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
        public void Benchmark_Bind_Box_GC()
        {
            BenchmarkHelper.MeasureGC(() =>
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

            handles.Cancel();
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_WrappedAction_GC()
        {
            BenchmarkHelper.MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(Box.Create(i), static (x, state) => DoNothing(x, state), (float x, Box<int> box, Action<float, int> action) => action(x, box.Value))
                        .AddTo(handles);
                }
            });

            handles.Cancel();
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_ActionWithState_GC()
        {
            BenchmarkHelper.MeasureGC(() =>
            {
                for (int i = 0; i < 10000; i++)
                {
                    LMotion.Create(0f, 1f, 3f)
                        .Bind(new ActionWithState<float, int>(i, static (x, state) => DoNothing(x, state)), (x, action) => action.Invoke(x))
                        .AddTo(handles);
                }
            });

            handles.Cancel();
        }

        [Test]
        [Performance]
        public void Benchmark_Bind_Closure_GC()
        {
            BenchmarkHelper.MeasureGC(() =>
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