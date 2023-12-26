using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using Unity.Mathematics;

namespace LitMotion
{
    /// <summary>
    /// A job that updates the status of the motion data and outputs the current value.
    /// </summary>
    /// <typeparam name="TValue">The type of value to animate</typeparam>
    /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
    /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
    [BurstCompile]
    public unsafe struct MotionUpdateJob<TValue, TOptions, TAdapter> : IJobParallelFor
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        [NativeDisableUnsafePtrRestriction] public MotionData<TValue, TOptions>* DataPtr;
        [ReadOnly] public float DeltaTime;
        [ReadOnly] public float UnscaledDeltaTime;

        [WriteOnly] public NativeList<int>.ParallelWriter CompletedIndexList;
        [WriteOnly] public NativeArray<TValue> Output;

        public void Execute([AssumeRange(0, int.MaxValue)] int index)
        {
            Hint.Assume(DeltaTime >= 0f);
            Hint.Assume(UnscaledDeltaTime >= 0f);

            var ptr = DataPtr + index;

            if (Hint.Likely(ptr->Status is MotionStatus.Scheduled or MotionStatus.Delayed or MotionStatus.Playing))
            {
                ptr->Time += ptr->IgnoreTimeScale ? UnscaledDeltaTime : DeltaTime;
                var time = math.max(0f, ptr->Time - ptr->Delay);
                if (ptr->Loops >= 0 && time >= ptr->Duration * ptr->Loops)
                {
                    ptr->Status = MotionStatus.Completed;
                }
                else if (ptr->Time < ptr->Delay)
                {
                    ptr->Status = MotionStatus.Delayed;
                }
                else
                {
                    ptr->Status = MotionStatus.Playing;
                }

                float t;
                bool isCompleted;
                int clampedCompletedLoops;

                if (Hint.Unlikely(ptr->Duration <= 0f))
                {
                    isCompleted = time > 0f;
                    if (isCompleted)
                    {
                        t = 1f;
                        clampedCompletedLoops = math.max(0, ptr->Loops);
                    }
                    else
                    {
                        t = 0f;
                        clampedCompletedLoops = 0;
                    }
                }
                else
                {
                    var completedLoops = (int)math.floor(time / ptr->Duration);
                    clampedCompletedLoops = ptr->Loops < 0 ? completedLoops : math.min(completedLoops, ptr->Loops);
                    isCompleted = ptr->Loops >= 0 && clampedCompletedLoops >= ptr->Loops;

                    if (isCompleted)
                    {
                        t = 1f;
                    }
                    else
                    {
                        var currentLoopTime = time - ptr->Duration * clampedCompletedLoops;
                        t = math.clamp(currentLoopTime / ptr->Duration, 0f, 1f);
                    }
                }
                
                float progress = EaseUtility.Evaluate(t, ptr->Ease);
                switch (ptr->LoopType)
                {
                    case LoopType.Yoyo:
                        // ReSharper disable once CompareOfFloatsByEqualityOperator
                        if (((clampedCompletedLoops & 1) == 1) ^ (t == 1f)) progress = 1f - progress;
                        break;
                    case LoopType.Incremental:
                        progress = t == 1f ? clampedCompletedLoops : clampedCompletedLoops + progress;
                        break;
                }

                var context = new MotionEvaluationContext()
                {
                    Progress = progress
                };

                Output[index] = default(TAdapter).Evaluate(ptr->StartValue, ptr->EndValue, ptr->Options, context);
            }
            else if (ptr->Status is MotionStatus.Completed or MotionStatus.Canceled)
            {
                CompletedIndexList.AddNoResize(index);
                ptr->Status = MotionStatus.Disposed;
            }
        }
    }
}