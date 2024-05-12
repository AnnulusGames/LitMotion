using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using LitMotion.Collections;

namespace LitMotion
{
    internal class MotionBuilderBuffer<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        static MotionBuilderBuffer<TValue, TOptions> PoolRoot = new();

        public static MotionBuilderBuffer<TValue, TOptions> Rent()
        {
            MotionBuilderBuffer<TValue, TOptions> result;
            if (PoolRoot == null)
            {
                result = new();
            }
            else
            {
                result = PoolRoot;
                PoolRoot = PoolRoot.NextNode;
                result.NextNode = null;
            }
            return result;
        }

        public static void Return(MotionBuilderBuffer<TValue, TOptions> buffer)
        {
            buffer.Version++;
            buffer.Data = MotionData<TValue, TOptions>.Default;
            buffer.CallbackData = MotionCallbackData.Default;
            buffer.AnimationCurve = default;
            buffer.Scheduler = default;
            buffer.IsPreserved = default;
            buffer.BindOnSchedule = default;

            if (buffer.Version != ushort.MaxValue)
            {
                buffer.NextNode = PoolRoot;
                PoolRoot = buffer;
            }
        }

        public ushort Version;
        public MotionBuilderBuffer<TValue, TOptions> NextNode;
        public bool IsPreserved;
        public bool BindOnSchedule;

        public MotionData<TValue, TOptions> Data = MotionData<TValue, TOptions>.Default;
        public MotionCallbackData CallbackData = MotionCallbackData.Default;
        public AnimationCurve AnimationCurve;

        public IMotionScheduler Scheduler;
    }

    /// <summary>
    /// Supports construction, scheduling, and binding of motion entities.
    /// </summary>
    /// <typeparam name="TValue">The type of value to animate</typeparam>
    /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
    /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
    public struct MotionBuilder<TValue, TOptions, TAdapter> : IDisposable
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        internal MotionBuilder(MotionBuilderBuffer<TValue, TOptions> buffer)
        {
            this.buffer = buffer;
            this.version = buffer.Version;
        }

        internal ushort version;
        internal MotionBuilderBuffer<TValue, TOptions> buffer;

        /// <summary>
        /// Specify easing for motion.
        /// </summary>
        /// <param name="ease">The type of easing</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithEase(Ease ease)
        {
            CheckEaseType(ease);
            CheckBuffer();
            buffer.Data.Core.Ease = ease;
            return this;
        }

        /// <summary>
        /// Specify easing for motion.
        /// </summary>
        /// <param name="animationCurve">Animation curve</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithEase(AnimationCurve animationCurve)
        {
            CheckBuffer();
            buffer.AnimationCurve = animationCurve;
            buffer.Data.Core.Ease = Ease.CustomAnimationCurve;
            return this;
        }

        /// <summary>
        /// Specify the delay time when the motion starts.
        /// </summary>
        /// <param name="delay">Delay time (seconds)</param>
        /// <param name="delayType">Delay type</param>
        /// <param name="skipValuesDuringDelay">Whether to skip updating values during the delay time</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithDelay(float delay, DelayType delayType = DelayType.FirstLoop, bool skipValuesDuringDelay = true)
        {
            CheckBuffer();
            buffer.Data.Core.Delay = delay;
            buffer.Data.Core.DelayType = delayType;
            buffer.CallbackData.SkipValuesDuringDelay = skipValuesDuringDelay;
            return this;
        }

        /// <summary>
        /// Specify the number of times the motion is repeated. If specified as less than 0, the motion will continue to play until manually completed or canceled.
        /// </summary>
        /// <param name="loops">Number of loops</param>
        /// <param name="loopType">Behavior at the end of each loop</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithLoops(int loops, LoopType loopType = LoopType.Restart)
        {
            CheckBuffer();
            buffer.Data.Core.Loops = loops;
            buffer.Data.Core.LoopType = loopType;
            return this;
        }

        /// <summary>
        /// Specify special parameters for each motion data.
        /// </summary>
        /// <param name="options">Option value to specify</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithOptions(TOptions options)
        {
            CheckBuffer();
            buffer.Data.Options = options;
            return this;
        }

        /// <summary>
        /// Specify the callback when canceled.
        /// </summary>
        /// <param name="callback">Callback when canceled</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithOnCancel(Action callback)
        {
            CheckBuffer();
            buffer.CallbackData.OnCancelAction += callback;
            return this;
        }

        /// <summary>
        /// Specify the callback when playback ends.
        /// </summary>
        /// <param name="callback">Callback when playback ends</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithOnComplete(Action callback)
        {
            CheckBuffer();
            buffer.CallbackData.OnCompleteAction += callback;
            return this;
        }

        /// <summary>
        /// Cancel Motion when an exception occurs during Bind processing.
        /// </summary>
        /// <param name="cancelOnError">Whether to cancel on error</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithCancelOnError(bool cancelOnError = true)
        {
            CheckBuffer();
            buffer.CallbackData.CancelOnError = cancelOnError;
            return this;
        }

        /// <summary>
        /// Bind values when scheduling the motion.
        /// </summary>
        /// <param name="bindOnSchedule">Whether to bind on sheduling</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithBindOnSchedule(bool bindOnSchedule = true)
        {
            CheckBuffer();
            buffer.BindOnSchedule = bindOnSchedule;
            return this;
        }

        /// <summary>
        /// Specifies the scheduler that schedule the motion.
        /// </summary>
        /// <param name="scheduler">Scheduler</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithScheduler(IMotionScheduler scheduler)
        {
            CheckBuffer();
            buffer.Scheduler = scheduler;
            return this;
        }

        /// <summary>
        /// Create motion and play it without binding it to a specific object.
        /// </summary>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle RunWithoutBinding()
        {
            CheckBuffer();
            SetMotionData();
            var scheduler = buffer.Scheduler;
            return Schedule(scheduler, ref buffer.Data, ref buffer.CallbackData);
        }

        /// <summary>
        /// Create motion and bind it to a specific object, property, etc.
        /// </summary>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle Bind(Action<TValue> action)
        {
            CheckBuffer();
            SetMotionData();
            SetCallbackData(action);
            var scheduler = buffer.Scheduler;
            return Schedule(scheduler, ref buffer.Data, ref buffer.CallbackData);
        }

        /// <summary>
        /// Create motion and bind it to a specific object. Unlike the regular Bind method, it avoids allocation by closure by passing an object.
        /// </summary>
        /// <typeparam name="TState">Type of state</typeparam>
        /// <param name="state">Motion state</param>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle BindWithState<TState>(TState state, Action<TValue, TState> action) where TState : class
        {
            CheckBuffer();
            SetMotionData();
            SetCallbackData(state, action);
            var scheduler = buffer.Scheduler;
            return Schedule(scheduler, ref buffer.Data, ref buffer.CallbackData);
        }

        /// <summary>
        /// Create motion and bind it to a specific object. Unlike the regular Bind method, it avoids allocation by closure by passing an object.
        /// </summary>
        /// <typeparam name="TState1">Type of state</typeparam>
        /// <typeparam name="TState2">Type of state</typeparam>
        /// <param name="state">Motion state</param>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle BindWithState<TState1, TState2>(TState1 state1, TState2 state2, Action<TValue, TState1, TState2> action)
            where TState1 : class
            where TState2 : class
        {
            CheckBuffer();
            SetMotionData();
            SetCallbackData(state1, state2, action);
            var scheduler = buffer.Scheduler;
            return Schedule(scheduler, ref buffer.Data, ref buffer.CallbackData);
        }


        /// <summary>
        /// Create motion and bind it to a specific object. Unlike the regular Bind method, it avoids allocation by closure by passing an object.
        /// </summary>
        /// <typeparam name="TState1">Type of state</typeparam>
        /// <typeparam name="TState2">Type of state</typeparam>
        /// <typeparam name="TState3">Type of state</typeparam>
        /// <param name="state">Motion state</param>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle BindWithState<TState1, TState2, TState3>(TState1 state1, TState2 state2, TState3 state3, Action<TValue, TState1, TState2, TState3> action)
            where TState1 : class
            where TState2 : class
            where TState3 : class
        {
            CheckBuffer();
            SetMotionData();
            SetCallbackData(state1, state2, state3, action);
            var scheduler = buffer.Scheduler;
            return Schedule(scheduler, ref buffer.Data, ref buffer.CallbackData);
        }

        /// <summary>
        /// Preserves the internal buffer and prevents the builder from being automatically destroyed after creating the motion data.
        /// Calling this allows you to create the motion multiple times, but you must call the Dispose method to destroy the builder after use.
        /// </summary>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> Preserve()
        {
            CheckBuffer();
            buffer.IsPreserved = true;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionHandle Schedule(IMotionScheduler scheduler, ref MotionData<TValue, TOptions> data, ref MotionCallbackData callbackData)
        {
            if (buffer.BindOnSchedule && callbackData.UpdateAction != null)
            {
                callbackData.InvokeUnsafe(
                    default(TAdapter).Evaluate(
                        ref data.StartValue,
                        ref data.EndValue,
                        ref data.Options,
                        new() { Progress = data.Core.Ease switch
                            {
                                Ease.CustomAnimationCurve => data.Core.AnimationCurve.Evaluate(0f),
                                _ => EaseUtility.Evaluate(0f, data.Core.Ease)
                            }
                        }
                ));
            }

            MotionHandle handle;

            if (scheduler == null)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    handle = EditorMotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData);
                }
                else if (MotionScheduler.DefaultScheduler == MotionScheduler.Update) // avoid virtual method call
                {
                    handle = MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, PlayerLoopTiming.Update);
                }
                else
                {
                    handle = MotionScheduler.DefaultScheduler.Schedule<TValue, TOptions, TAdapter>(ref data, ref callbackData);
                }
#else
                if (MotionScheduler.DefaultScheduler == MotionScheduler.Update) // avoid virtual method call
                {
                    handle = MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, PlayerLoopTiming.Update);
                }
                else
                {
                    handle = MotionScheduler.DefaultScheduler.Schedule<TValue, TOptions, TAdapter>(ref data, ref callbackData);
                }
#endif
            }
            else
            {
                handle = scheduler.Schedule<TValue, TOptions, TAdapter>(ref data, ref callbackData);
            }

            if (MotionTracker.EnableTracking)
            {
                MotionTracker.AddTracking(handle, scheduler);
            }

            if (!buffer.IsPreserved) Dispose();

            return handle;
        }

        /// <summary>
        /// Dispose this builder. You need to call this manually after calling Preserve or if you have never created a motion data.
        /// </summary>
        public void Dispose()
        {
            if (buffer == null) return;
            MotionBuilderBuffer<TValue, TOptions>.Return(buffer);
            buffer = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetMotionData()
        {
            buffer.Data.Core.Status = MotionStatus.Scheduled;

            if (buffer.AnimationCurve != null)
            {
#if LITMOTION_COLLECTIONS_2_0_OR_NEWER
                buffer.Data.Core.AnimationCurve = new NativeAnimationCurve(buffer.AnimationCurve, Allocator.Temp);
#else
                buffer.Data.Core.AnimationCurve = new UnsafeAnimationCurve(buffer.AnimationCurve, Allocator.Temp);
#endif
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetCallbackData(Action<TValue> action)
        {
            buffer.CallbackData.UpdateAction = action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetCallbackData<TState>(TState state, Action<TValue, TState> action)
            where TState : class
        {
            buffer.CallbackData.StateCount = 1;
            buffer.CallbackData.State1 = state;
            buffer.CallbackData.UpdateAction = action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetCallbackData<TState1, TState2>(TState1 state1, TState2 state2, Action<TValue, TState1, TState2> action)
            where TState1 : class
            where TState2 : class
        {
            buffer.CallbackData.StateCount = 2;
            buffer.CallbackData.State1 = state1;
            buffer.CallbackData.State2 = state2;
            buffer.CallbackData.UpdateAction = action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetCallbackData<TState1, TState2, TState3>(TState1 state1, TState2 state2, TState3 state3, Action<TValue, TState1, TState2, TState3> action)
            where TState1 : class
            where TState2 : class
            where TState3 : class
        {
            buffer.CallbackData.StateCount = 3;
            buffer.CallbackData.State1 = state1;
            buffer.CallbackData.State2 = state2;
            buffer.CallbackData.State3 = state3;
            buffer.CallbackData.UpdateAction = action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void CheckBuffer()
        {
            if (buffer == null || buffer.Version != version) throw new InvalidOperationException("MotionBuilder is either not initialized or has already run a Build (or Bind). If you want to build or bind multiple times, call Preseve() for MotionBuilder.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void CheckEaseType(Ease ease)
        {
            if (ease is Ease.CustomAnimationCurve) throw new ArgumentException($"Ease.{ease} cannot be specified directly.");
        }
    }
}