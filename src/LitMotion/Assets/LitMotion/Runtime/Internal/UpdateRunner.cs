using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace LitMotion
{
    internal interface IUpdateRunner
    {
        public void Update(float deltaTime, float unscaledDeltaTime);
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

        public unsafe void Update(float deltaTime, float unscaledDeltaTime)
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
                    DeltaTime = deltaTime,
                    UnscaledDeltaTime = unscaledDeltaTime,
                    Output = output,
                    CompletedIndexList = completedIndexList.AsParallelWriter()
                };
                job.Schedule(count, 16).Complete();

                // invoke delegates
                for (int i = 0; i < storage.callbacksArray.Length; i++)
                {
                    var status = (dataPtr + i)->Status;
                    if (status is MotionStatus.Playing or MotionStatus.Completed)
                    {
                        var callbacks = storage.callbacksArray[i];
                        try
                        {
                            if (output.Length > i) callbacks.InvokeUnsafe(output[i]);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogException(ex);
                        }

                        if (status is MotionStatus.Completed)
                        {
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
            }

            storage.RemoveAll(completedIndexList);
        }

        public void Reset()
        {
            storage.Reset();
        }
    }
}