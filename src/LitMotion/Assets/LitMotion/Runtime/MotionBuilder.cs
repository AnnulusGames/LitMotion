using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal class MotionBuilderBuffer<TValue, TOptions>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
    {
        static MotionBuilderBuffer()
        {
            pool = new(4);
            for (int i = 0; i < 4; i++) pool.Push(new());
        }

        static readonly Stack<MotionBuilderBuffer<TValue, TOptions>> pool;

        public static MotionBuilderBuffer<TValue, TOptions> Rent()
        {
            if (!pool.TryPop(out var result)) result = new();
            return result;
        }

        public static void Return(MotionBuilderBuffer<TValue, TOptions> buffer)
        {
            buffer.Version++;
            buffer.Duration = default;
            buffer.Ease = default;
            buffer.Delay = default;
            buffer.Loops = 1;
            buffer.LoopType = default;
            buffer.StartValue = default;
            buffer.EndValue = default;
            buffer.Options = default;
            buffer.Scheduler = default;
            buffer.OnComplete = default;
            buffer.OnCancel = default;
            buffer.CancelOnError = default;
            buffer.IsPreserved = default;

            if (buffer.Version != ushort.MaxValue)
            {
                pool.Push(buffer);
            }
        }

        public ushort Version;

        public float Duration;
        public Ease Ease;
        public float Delay;
        public int Loops = 1;
        public LoopType LoopType;

        public TValue StartValue;
        public TValue EndValue;
        public TOptions Options;

        public IMotionScheduler Scheduler;

        public Action OnComplete;
        public Action OnCancel;
        public bool CancelOnError;
        public bool IsPreserved;
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
            CheckBuffer();
            buffer.Ease = ease;
            return this;
        }

        /// <summary>
        /// Specify the delay time when the motion starts.
        /// </summary>
        /// <param name="delay">Delay time (seconds)</param>
        /// <returns>This builder to allow chaining multiple method calls.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly MotionBuilder<TValue, TOptions, TAdapter> WithDelay(float delay)
        {
            CheckBuffer();
            buffer.Delay = delay;
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
            buffer.OnCancel += callback;
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
            buffer.OnComplete += callback;
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
            var callbacks = BuildCallbackData();
            var scheduler = buffer.Scheduler;
            var data = BuildMotionData();
            return Schedule(scheduler, ref data, ref callbacks);
        }

        /// <summary>
        /// Create motion and bind it to a specific object, property, etc.
        /// </summary>
        /// <param name="action">Action that handles binding</param>
        /// <returns>Handle of the created motion data.</returns>
        public MotionHandle Bind(Action<TValue> action)
        {
            CheckBuffer();
            var callbacks = BuildCallbackData(action);
            callbacks.OnCompleteAction = buffer.OnComplete;
            var scheduler = buffer.Scheduler;
            var data = BuildMotionData();
            return Schedule(scheduler, ref data, ref callbacks);
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
            var callbacks = BuildCallbackData(state, action);
            var scheduler = buffer.Scheduler;
            var data = BuildMotionData();
            return Schedule(scheduler, ref data, ref callbacks);
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
            var callbacks = BuildCallbackData(state1, state2, action);
            var scheduler = buffer.Scheduler;
            var data = BuildMotionData();
            return Schedule(scheduler, ref data, ref callbacks);
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
            var callbacks = BuildCallbackData(state1, state2, state3, action);
            var scheduler = buffer.Scheduler;
            var data = BuildMotionData();
            return Schedule(scheduler, ref data, ref callbacks);
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
        internal readonly MotionHandle Schedule(IMotionScheduler scheduler, ref MotionData<TValue, TOptions> data, ref MotionCallbackData callbackData)
        {
            if (scheduler == null)
            {
#if UNITY_EDITOR
                if (!UnityEditor.EditorApplication.isPlaying)
                {
                    return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, UpdateMode.EditorApplicationUpdate);
                }
#endif
                return MotionDispatcher.Schedule<TValue, TOptions, TAdapter>(data, callbackData, UpdateMode.Update);
            }
            else
            {
                return scheduler.Schedule<TValue, TOptions, TAdapter>(ref data, ref callbackData);
            }
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
        internal MotionData<TValue, TOptions> BuildMotionData()
        {
            var data = new MotionData<TValue, TOptions>()
            {
                StartValue = buffer.StartValue,
                EndValue = buffer.EndValue,
                Options = buffer.Options,
                StartTime = buffer.Scheduler == null ? MotionScheduler.Update.Time : buffer.Scheduler.Time,
                Duration = buffer.Duration,
                Ease = buffer.Ease,
                Delay = buffer.Delay,
                Loops = buffer.Loops,
                LoopType = buffer.LoopType,
                Status = MotionStatus.Scheduled,
            };
            if (!buffer.IsPreserved) Dispose();
            return data;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionCallbackData BuildCallbackData()
        {
            var callbacks = new MotionCallbackData
            {
                OnCancelAction = buffer.OnCancel,
                OnCompleteAction = buffer.OnComplete,
                CancelOnError = buffer.CancelOnError
            };
            return callbacks;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionCallbackData BuildCallbackData(Action<TValue> action)
        {
            var callbacks = new MotionCallbackData
            {
                UpdateAction = action,
                OnCancelAction = buffer.OnCancel,
                OnCompleteAction = buffer.OnComplete,
                CancelOnError = buffer.CancelOnError
            };
            return callbacks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionCallbackData BuildCallbackData<TState>(TState state, Action<TValue, TState> action)
            where TState : class
        {
            var callbacks = new MotionCallbackData
            {
                StateCount = 1,
                State1 = state,
                UpdateAction = action,
                OnCancelAction = buffer.OnCancel,
                OnCompleteAction = buffer.OnComplete,
                CancelOnError = buffer.CancelOnError
            };

            return callbacks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionCallbackData BuildCallbackData<TState1, TState2>(TState1 state1, TState2 state2, Action<TValue, TState1, TState2> action)
            where TState1 : class
            where TState2 : class
        {
            var callbacks = new MotionCallbackData
            {
                StateCount = 2,
                State1 = state1,
                State2 = state2,
                UpdateAction = action,
                OnCancelAction = buffer.OnCancel,
                OnCompleteAction = buffer.OnComplete,
                CancelOnError = buffer.CancelOnError
            };

            return callbacks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal MotionCallbackData BuildCallbackData<TState1, TState2, TState3>(TState1 state1, TState2 state2, TState3 state3, Action<TValue, TState1, TState2, TState3> action)
            where TState1 : class
            where TState2 : class
            where TState3 : class
        {
            var callbacks = new MotionCallbackData
            {
                StateCount = 3,
                State1 = state1,
                State2 = state2,
                State3 = state3,
                UpdateAction = action,
                OnCancelAction = buffer.OnCancel,
                OnCompleteAction = buffer.OnComplete,
                CancelOnError = buffer.CancelOnError
            };

            return callbacks;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void CheckBuffer()
        {
            if (buffer == null || buffer.Version != version) throw new InvalidOperationException("MotionBuilder is either not initialized or has already run a Build (or Bind). If you want to build or bind multiple times, call Preseve() for MotionBuilder.");
        }
    }
}