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
        [ReadOnly] public double Time;
        [ReadOnly] public double UnscaledTime;
        [ReadOnly] public double Realtime;

        [WriteOnly] public NativeList<int>.ParallelWriter CompletedIndexList;
        [WriteOnly] public NativeArray<TValue> Output;

        public void Execute([AssumeRange(0, int.MaxValue)] int index)
        {
            var ptr = DataPtr + index;

            if (Hint.Likely(ptr->Status is MotionStatus.Scheduled or MotionStatus.Delayed or MotionStatus.Playing))
            {
                var currentTime = ptr->TimeKind switch
                {
                    MotionTimeKind.Time => Time,
                    MotionTimeKind.UnscaledTime => UnscaledTime,
                    MotionTimeKind.Realtime => Realtime,
                    _ => default
                };

                var motionTime = currentTime - ptr->StartTime;

                double t;
                bool isCompleted;
                bool isDelayed;
                int completedLoops;
                int clampedCompletedLoops;

                if (Hint.Unlikely(ptr->Duration <= 0f))
                {
                    if (ptr->DelayType == DelayType.FirstLoop || ptr->Delay == 0f)
                    {
                        var time = motionTime - ptr->Delay;
                        isCompleted = ptr->Loops >= 0 && time > 0f;
                        if (isCompleted)
                        {
                            t = 1f;
                            completedLoops = ptr->Loops;
                        }
                        else
                        {
                            t = 0f;
                            completedLoops = time < 0f ? -1 : 0;
                        }
                        clampedCompletedLoops = ptr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, ptr->Loops);
                        isDelayed = time < 0;
                    }
                    else
                    {
                        completedLoops = (int)math.floor(motionTime / ptr->Delay);
                        clampedCompletedLoops = ptr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, ptr->Loops);
                        isCompleted = ptr->Loops >= 0 && clampedCompletedLoops > ptr->Loops - 1;
                        isDelayed = !isCompleted;
                        t = isCompleted ? 1f : 0f;
                    }
                }
                else
                {
                    if (ptr->DelayType == DelayType.FirstLoop)
                    {
                        var time = motionTime - ptr->Delay;
                        completedLoops = (int)math.floor(time / ptr->Duration);
                        clampedCompletedLoops = ptr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, ptr->Loops);
                        isCompleted = ptr->Loops >= 0 && clampedCompletedLoops > ptr->Loops - 1;
                        isDelayed = time < 0f;

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
                    else
                    {
                        var currentLoopTime = math.fmod(motionTime, ptr->Duration + ptr->Delay) - ptr->Delay;
                        completedLoops = (int)math.floor(motionTime / (ptr->Duration + ptr->Delay));
                        clampedCompletedLoops = ptr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, ptr->Loops);
                        isCompleted = ptr->Loops >= 0 && clampedCompletedLoops > ptr->Loops - 1;
                        isDelayed = currentLoopTime < ptr->Delay;

                        if (isCompleted)
                        {
                            t = 1f;
                        }
                        else
                        {
                            t = math.clamp(currentLoopTime / ptr->Duration, 0f, 1f);
                        }
                    }
                }

                float progress;
                switch (ptr->LoopType)
                {
                    default:
                    case LoopType.Restart:
                        progress = EaseUtility.Evaluate((float)t, ptr->Ease);
                        break;
                    case LoopType.Yoyo:
                        progress = EaseUtility.Evaluate((float)t, ptr->Ease);
                        if ((clampedCompletedLoops + (int)t) % 2 == 1) progress = 1f - progress;
                        break;
                    case LoopType.Incremental:
                        progress = EaseUtility.Evaluate(1f, ptr->Ease) * clampedCompletedLoops + EaseUtility.Evaluate((float)math.fmod(t, 1f), ptr->Ease);
                        break;
                }

                var totalDuration = ptr->DelayType == DelayType.FirstLoop
                    ? ptr->Delay + ptr->Duration * ptr->Loops
                    : (ptr->Delay + ptr->Duration) * ptr->Loops;

                if (ptr->Loops > 0 && motionTime >= totalDuration)
                {
                    ptr->Status = MotionStatus.Completed;
                }
                else if (isDelayed)
                {
                    ptr->Status = MotionStatus.Delayed;
                }
                else
                {
                    ptr->Status = MotionStatus.Playing;
                }

                var context = new MotionEvaluationContext()
                {
                    Progress = progress
                };

                Output[index] = default(TAdapter).Evaluate(ref ptr->StartValue, ref ptr->EndValue, ref ptr->Options, context);
            }
            else if (ptr->Status is MotionStatus.Completed or MotionStatus.Canceled)
            {
                CompletedIndexList.AddNoResize(index);
                ptr->Status = MotionStatus.Disposed;
            }
        }
    }
}