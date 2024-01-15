using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace LitMotion
{
    internal interface IUpdateRunner
    {
        public void Update(double time, double unscaledTime);
        public void Reset();
    }

    internal sealed class UpdateRunner<TValue, TOptions, TAdapter> : IUpdateRunner
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        public UpdateRunner(MotionStorage<TValue, TOptions, TAdapter> storage)
        {
            this.storage = storage;
        }

        readonly MotionStorage<TValue, TOptions, TAdapter> storage;

        public unsafe void Update(double time, double unscaledTime)
        {
            var count = storage.Count;
            using var output = new NativeArray<TValue>(count, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
            using var completedIndexList = new NativeList<int>(count, Allocator.TempJob);

            fixed (MotionData<TValue, TOptions>* dataPtr = storage.dataArray)
            {
                // update data
                var job = new MotionUpdateJob<TValue, TOptions, TAdapter>()
                {
                    DataPtr = dataPtr,
                    Time = time,
                    UnscaledTime = unscaledTime,
                    Output = output,
                    CompletedIndexList = completedIndexList.AsParallelWriter()
                };
                job.Schedule(count, 16).Complete();

                // invoke delegates
                var callbackSpan = storage.GetCallbacksSpan();
                var outputPtr = (TValue*)output.GetUnsafePtr();
                for (int i = 0; i < callbackSpan.Length; i++)
                {
                    var status = (dataPtr + i)->Status;
                    if (status == MotionStatus.Playing)
                    {
                        ref var callbacks = ref callbackSpan[i];
                        try
                        {
                            callbacks.InvokeUnsafe(outputPtr[i]);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                            if (callbacks.CancelOnError)
                            {
                                (dataPtr + i)->Status = MotionStatus.Canceled;
                                callbacks.OnCancelAction?.Invoke();
                            }
                        }
                    }
                    else if (status == MotionStatus.Completed)
                    {
                        ref var callbacks = ref callbackSpan[i];
                        try
                        {
                            callbacks.InvokeUnsafe(outputPtr[i]);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                            if (callbacks.CancelOnError)
                            {
                                (dataPtr + i)->Status = MotionStatus.Canceled;
                                callbacks.OnCancelAction?.Invoke();
                                continue;
                            }
                        }

                        try
                        {
                            callbacks.OnCompleteAction?.Invoke();
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
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