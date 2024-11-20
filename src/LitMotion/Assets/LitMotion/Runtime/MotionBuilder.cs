using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LitMotion
{
    internal sealed class MotionBuilderBuffer<TValue, TOptions>
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
            buffer.IsPreserved = false;
            buffer.BindOnSchedule = false;

            buffer.StartValue = default;
            buffer.EndValue = default;
            buffer.Options = default;

            buffer.Duration = default;
            buffer.Ease = default;
            buffer.AnimationCurve = default;
            buffer.TimeKind = default;
            buffer.Delay = default;
            buffer.Loops = 1;
            buffer.LoopType = default;

            buffer.State = default;
            buffer.UpdateAction = default;
            buffer.OnCompleteAction = default;
            buffer.OnCancelAction = default;

            buffer.CancelOnError = default;
            buffer.SkipValuesDuringDelay = default;

            buffer.Scheduler = default;

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

        public TValue StartValue;
        public TValue EndValue;
        public TOptions Options;
        public float Duration;
        public Ease Ease;
        public MotionTimeKind TimeKind;
        public float Delay;
        public int Loops = 1;
        public DelayType DelayType;
        public LoopType LoopType;
        public bool CancelOnError;
        public bool SkipValuesDuringDelay;
        public object State;
        public object UpdateAction;
        public Action OnCompleteAction;
        public Action OnCancelAction;
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
            buffer.Ease = ease;
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
            buffer.Ease = Ease.CustomAnimationCurve;
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
            buffer.Delay = delay;
            buffer.DelayType = delayType;
            buffer.SkipValuesDuringDelay = skipValuesDuringDelay;
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
            buffer.Loops = loops;
            buffer.LoopType = loopType;
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
            buffer.Options = options;
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
            buffer.OnCancelAction += callback;
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
            buffer.OnCompleteAction += callback;
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
            buffer.CancelOnError = cancelOnError;
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
            return ScheduleCore();
        }

        /// <summary>
        /// Create motion and bind it to a specific object, property, etc.
        /// </summary>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle Bind(Action<TValue> action)
        {
            CheckBuffer();
            SetCallbackData(action);
            return ScheduleCore();
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
            SetCallbackData(state, action);
            return ScheduleCore();
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
        internal MotionHandle ScheduleCore()
        {
            MotionHandle handle;

            if (buffer.Scheduler == null)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    handle = EditorMotionDispatcher.Schedule(ref this);
                }
                else if (MotionScheduler.DefaultScheduler == MotionScheduler.Update) // avoid virtual method call
                {
                    handle = MotionDispatcher.Schedule(ref this, PlayerLoopTiming.Update);
                }
                else
                {
                    handle = MotionScheduler.DefaultScheduler.Schedule(ref this);
                }
#else
                if (MotionScheduler.DefaultScheduler == MotionScheduler.Update) // avoid virtual method call
                {
                    handle = MotionDispatcher.Schedule(ref this, PlayerLoopTiming.Update);
                }
                else
                {
                    handle = MotionScheduler.DefaultScheduler.Schedule(ref this);
                }
#endif
            }
            else
            {
                handle = buffer.Scheduler.Schedule(ref this);
            }

            if (MotionTracker.EnableTracking)
            {
                MotionTracker.AddTracking(handle, buffer.Scheduler);
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
        internal readonly void SetCallbackData(Action<TValue> action)
        {
            buffer.UpdateAction = action;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void SetCallbackData<TState>(TState state, Action<TValue, TState> action)
            where TState : class
        {
            buffer.State = state;
            buffer.UpdateAction = action;
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