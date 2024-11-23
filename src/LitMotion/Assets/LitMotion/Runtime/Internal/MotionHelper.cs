using Unity.Burst;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;

namespace LitMotion
{
    [BurstCompile]
    internal unsafe static class MotionHelper
    {
        [BurstCompile]
        public static void SetTime<TValue, TOptions, TAdapter>(MotionData<TValue, TOptions>* ptr, double time, out TValue result)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var corePtr = (MotionDataCore*)ptr;

            corePtr->Time = math.max(time, 0.0);

            double t;
            bool isCompleted;
            bool isDelayed;
            int completedLoops;
            int clampedCompletedLoops;

            if (Hint.Unlikely(corePtr->Duration <= 0f))
            {
                if (corePtr->DelayType == DelayType.FirstLoop || corePtr->Delay == 0f)
                {
                    var timeSinceStart = time - corePtr->Delay;
                    isCompleted = corePtr->Loops >= 0 && timeSinceStart > 0f;
                    if (isCompleted)
                    {
                        t = 1f;
                        completedLoops = corePtr->Loops;
                    }
                    else
                    {
                        t = 0f;
                        completedLoops = timeSinceStart < 0f ? -1 : 0;
                    }
                    clampedCompletedLoops = corePtr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, corePtr->Loops);
                    isDelayed = timeSinceStart < 0;
                }
                else
                {
                    completedLoops = (int)math.floor(time / corePtr->Delay);
                    clampedCompletedLoops = corePtr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, corePtr->Loops);
                    isCompleted = corePtr->Loops >= 0 && clampedCompletedLoops > corePtr->Loops - 1;
                    isDelayed = !isCompleted;
                    t = isCompleted ? 1f : 0f;
                }
            }
            else
            {
                if (corePtr->DelayType == DelayType.FirstLoop)
                {
                    var timeSinceStart = time - corePtr->Delay;
                    completedLoops = (int)math.floor(timeSinceStart / corePtr->Duration);
                    clampedCompletedLoops = corePtr->Loops < 0 ? math.max(0, completedLoops) : math.clamp(completedLoops, 0, corePtr->Loops);
                    isCompleted = corePtr->Loops >= 0 && clampedCompletedLoops > corePtr->Loops - 1;
                    isDelayed = timeSinceStart < 0f;

                    if (isCompleted)
                    {
                        t = 1f;
                    }
                    else
                    {
                        var currentLoopTime = timeSinceStart - corePtr->Duration * clampedCompletedLoops;
                        t = math.clamp(currentLoopTime / corePtr->Duration, 0f, 1f);
                    }
                }
                else
                {
                    var currentLoopTime = math.fmod(time, corePtr->Duration + corePtr->Delay) - corePtr->Delay;
                    completedLoops = (int)math.floor(time / (corePtr->Duration + corePtr->Delay));
                    clampedCompletedLoops = corePtr->Loops < 0
                        ? math.max(0, completedLoops)
                        : math.clamp(completedLoops, 0, corePtr->Loops);
                    isCompleted = corePtr->Loops >= 0 && clampedCompletedLoops > corePtr->Loops - 1;
                    isDelayed = currentLoopTime < 0;

                    if (isCompleted)
                    {
                        t = 1f;
                    }
                    else
                    {
                        t = math.clamp(currentLoopTime / corePtr->Duration, 0f, 1f);
                    }
                }
            }

            float progress;
            switch (corePtr->LoopType)
            {
                default:
                case LoopType.Restart:
                    progress = GetEasedValue(corePtr, (float)t);
                    break;
                case LoopType.Flip:
                    progress = GetEasedValue(corePtr, (float)t);
                    if ((clampedCompletedLoops + (int)t) % 2 == 1) progress = 1f - progress;
                    break;
                case LoopType.Incremental:
                    progress = GetEasedValue(corePtr, 1f) * clampedCompletedLoops + GetEasedValue(corePtr, (float)math.fmod(t, 1f));
                    break;
                case LoopType.Yoyo:
                    progress = (clampedCompletedLoops + (int)t) % 2 == 1
                        ? GetEasedValue(corePtr, (float)(1f - t))
                        : GetEasedValue(corePtr, (float)t);
                    break;
            }

            var totalDuration = corePtr->DelayType == DelayType.FirstLoop
                ? corePtr->Delay + corePtr->Duration * corePtr->Loops
                : (corePtr->Delay + corePtr->Duration) * corePtr->Loops;

            if (isCompleted)
            {
                corePtr->Status = MotionStatus.Completed;
            }
            else if (isDelayed)
            {
                corePtr->Status = MotionStatus.Delayed;
            }
            else
            {
                corePtr->Status = MotionStatus.Playing;
            }

            var context = new MotionEvaluationContext()
            {
                Progress = progress
            };

            result = default(TAdapter).Evaluate(ref ptr->StartValue, ref ptr->EndValue, ref ptr->Options, context);
        }

        static float GetEasedValue(MotionDataCore* data, float value)
        {
            return data->Ease switch
            {
                Ease.CustomAnimationCurve => data->AnimationCurve.Evaluate(value),
                _ => EaseUtility.Evaluate(value, data->Ease)
            };
        }
    }
}