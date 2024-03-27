using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LitMotion
{
    /// <summary>
    /// Provides functionality for tracking active motions.
    /// </summary>
    public static class MotionTracker
    {
        public static bool EnableTracking = false;
        public static bool EnableStackTrace = false;

        public static IReadOnlyList<TrackingState> Items => trackings;
        static readonly List<TrackingState> trackings = new(16);

        public static void AddTracking(MotionHandle motionHandle, IMotionScheduler scheduler, int skipFrames = 3)
        {
            var state = TrackingState.Create();
            (state.ValueType, state.OptionsType, state.AdapterType) = MotionStorageManager.GetMotionType(motionHandle);
            state.Scheduler = scheduler;
            state.CreationTime = DateTime.UtcNow;
#if UNITY_EDITOR
            state.CreatedOnEditor = UnityEditor.EditorApplication.isPlaying;
#endif

            if (EnableStackTrace) state.StackTrace = new StackTrace(skipFrames, true);

            ref var callbackData = ref MotionStorageManager.GetMotionCallbackDataRef(motionHandle);
            state.OriginalOnCompleteCallback = callbackData.OnCompleteAction;
            callbackData.OnCompleteAction = state.OnCompleteDelegate;
            state.OriginalOnCancelCallback = callbackData.OnCancelAction;
            callbackData.OnCancelAction = state.OnCancelDelegate;

            trackings.Add(state);
        }

        public static void Clear()
        {
            trackings.Clear();
        }

        public sealed class TrackingState
        {
            static readonly Stack<TrackingState> pool = new(16);

            TrackingState()
            {
                OnCompleteDelegate = OnComplete;
                OnCancelDelegate = OnCancel;
            }

            public static TrackingState Create()
            {
                if (!pool.TryPop(out var state))
                {
                    state = new();
                }
                return state;
            }

            public Type ValueType;
            public Type OptionsType;
            public Type AdapterType;
            public IMotionScheduler Scheduler;
            public DateTime CreationTime;
            public StackTrace StackTrace;
            public bool CreatedOnEditor;
            public Action OriginalOnCompleteCallback;
            public Action OriginalOnCancelCallback;

            public readonly Action OnCompleteDelegate;
            public readonly Action OnCancelDelegate;

            void OnComplete()
            {
                try
                {
                    OriginalOnCompleteCallback?.Invoke();
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                }
                Release();
            }

            void OnCancel()
            {
                try
                {
                    OriginalOnCancelCallback?.Invoke();
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                }
                Release();
            }

            void Release()
            {
                trackings.Remove(this);
                ValueType = default;
                OptionsType = default;
                AdapterType = default;
                Scheduler = default;
                CreationTime = default;
                StackTrace = default;
                CreatedOnEditor = default;
                OriginalOnCompleteCallback = default;
                OriginalOnCancelCallback = default;
                pool.Push(this);
            }
        }
    }
}