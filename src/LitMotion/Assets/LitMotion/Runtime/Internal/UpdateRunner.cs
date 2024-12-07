using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

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

            fixed (MotionData<TValue, TOptions>* dataPtr = storage.GetDataSpan())
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
                var managedDataSpan = storage.GetManagedDataSpan();
                var outputPtr = (TValue*)output.GetUnsafePtr();
                for (int i = 0; i < managedDataSpan.Length; i++)
                {
                    var currentDataPtr = dataPtr + i;

                    if (currentDataPtr->Core.SkipUpdate) continue;

                    var status = currentDataPtr->Core.Status;
                    ref var managedData = ref managedDataSpan[i];
                    if (status is MotionStatus.Playing or MotionStatus.Completed || (status == MotionStatus.Delayed && !managedData.SkipValuesDuringDelay))
                    {
                        try
                        {
                            managedData.UpdateUnsafe(outputPtr[i]);
                        }
                        catch (Exception ex)
                        {
                            MotionDispatcher.GetUnhandledExceptionHandler()?.Invoke(ex);
                            if (managedData.CancelOnError)
                            {
                                currentDataPtr->Core.Status = MotionStatus.Canceled;
                                managedData.OnCancelAction?.Invoke();
                            }
                        }

                        if (dataPtr->Core.WasLoopCompleted)
                        {
                            managedData.InvokeOnLoopComplete(dataPtr->Core.ComplpetedLoops);
                        }

                        if (status is MotionStatus.Completed && currentDataPtr->Core.WasStatusChanged)
                        {
                            managedData.InvokeOnComplete();
                        }
                    }
                }
            }

            storage.RemoveAll(completedIndexList);
        }

        public void Reset()
        {
            prevTime = 0;
            prevUnscaledTime = 0;
            prevRealtime = 0;
            storage.Reset();
        }
    }
}