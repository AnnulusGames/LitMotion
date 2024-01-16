using System;
using System.Collections.Generic;

namespace LitMotion
{
    public static class MotionTracker
    {
        public static bool EnableTracking = false;
        // public static bool EnableStackTrace = false;

        public static IReadOnlyList<TrackingState> Items => trackings;
        static readonly List<TrackingState> trackings = new(16);

        internal static void AddTracking(MotionHandle motionHandle)
        {
            var state = TrackingState.Create();
            (state.ValueType, state.OptionsType, state.AdapterType) = MotionStorageManager.GetMotionType(motionHandle);

            var callbackData = MotionStorageManager.GetMotionCallbacks(motionHandle);
            callbackData.OnCompleteAction += state.ReleaseDelegate;
            callbackData.OnCancelAction += state.ReleaseDelegate;
            MotionStorageManager.SetMotionCallbacks(motionHandle, callbackData);

            trackings.Add(state);
        }

        public sealed class TrackingState
        {
            static readonly Stack<TrackingState> pool = new(16);

            TrackingState()
            {
                ReleaseDelegate = Release;
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

            public readonly Action ReleaseDelegate;

            public void Release()
            {
                trackings.Remove(this);
                ValueType = default;
                OptionsType = default;
                AdapterType = default;
                pool.Push(this);
            }
        }
    }
}