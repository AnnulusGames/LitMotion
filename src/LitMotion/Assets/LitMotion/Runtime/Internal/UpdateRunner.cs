using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace LitMotion
{
    internal interface IUpdateRunner
    {
        public void Update(double time, double unscaledTime, double realtime);
        public void Reset();
    }

    internal sealed class UpdateRunner<TValue, TOptions, TAdapter> : IUpdateRunner
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        public UpdateRunner(MotionStorage<TValue, TOptions, TAdapter> storage, double time, double unscaledTime, double realtime)
        {
            this.storage = storage;
            prevTime = time;
            prevUnscaledTime = unscaledTime;
            prevRealtime = realtime;
        }

        readonly MotionStorage<TValue, TOptions, TAdapter> storage;

        double prevTime;
        double prevUnscaledTime;
        double prevRealtime;

        public unsafe void Update(double time, double unscaledTime, double realtime)
        {
            var count = storage.Count;
            using var output = new NativeArray<TValue>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            using var completedIndexList = new NativeList<int>(count, Allocator.TempJob);

            var deltaTime = time - prevTime;
            var unscaledDeltaTime = unscaledTime - prevUnscaledTime;
            var realDeltaTime = realtime - prevRealtime;
            prevTime = time;
            prevUnscaledTime = unscaledTime;
            prevRealtime = realtime;

            fixed (MotionData<TValue, TOptions>* dataPtr = storage.dataArray)
            {
                // update data
                var job = new MotionUpdateJob<TValue, TOptions, TAdapter>()
                {
                    DataPtr = dataPtr,
                    DeltaTime = deltaTime,
                    UnscaledDeltaTime = unscaledDeltaTime,
                    RealDeltaTime = realDeltaTime,
                    Output = output,
                    CompletedIndexList = completedIndexList.AsParallelWriter()
                };
                job.Schedule(count, 16).Complete();

                // invoke delegates
                var callbackSpan = storage.GetCallbacksSpan();
                var outputPtr = (TValue*)output.GetUnsafePtr();
                for (int i = 0; i < callbackSpan.Length; i++)
                {
                    var status = (dataPtr + i)->Core.Status;
                    ref var callbackData = ref callbackSpan[i];
                    if (status == MotionStatus.Playing || (status == MotionStatus.Delayed && !callbackData.SkipValuesDuringDelay))
                    {
                        try
                        {
                            callbackData.InvokeUnsafe(outputPtr[i]);
                        }
                        catch (Exception ex)
                        {
                            MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                            if (callbackData.CancelOnError)
                            {
                                (dataPtr + i)->Core.Status = MotionStatus.Canceled;
                                callbackData.OnCancelAction?.Invoke();
                            }
                        }
                    }
                    else if (status == MotionStatus.Completed)
                    {
                        try
                        {
                            callbackData.InvokeUnsafe(outputPtr[i]);
                        }
                        catch (Exception ex)
                        {
                            MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                            if (callbackData.CancelOnError)
                            {
                                (dataPtr + i)->Core.Status = MotionStatus.Canceled;
                                callbackData.OnCancelAction?.Invoke();
                                continue;
                            }
                        }

                        try
                        {
                            callbackData.OnCompleteAction?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                        }
                    }
                }
            }

            storage.RemoveAll(completedIndexList);
        }

        public void Reset()
        {
            storage.Reset();
        }
    }
}