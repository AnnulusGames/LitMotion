#if LITMOTION_SUPPORT_UNITASK
using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace LitMotion
{
    internal sealed class UniTaskMotionConfiguredSource : MotionConfiguredSourceBase ,IUniTaskSource, ITaskPoolNode<UniTaskMotionConfiguredSource>
    {
        static UniTaskMotionConfiguredSource()
        {
            TaskPool.RegisterSizeGetter(typeof(UniTaskMotionConfiguredSource), () => pool.Size);
        }

        UniTaskMotionConfiguredSource() : base() { }

        static TaskPool<UniTaskMotionConfiguredSource> pool;
        UniTaskMotionConfiguredSource nextNode;
        public ref UniTaskMotionConfiguredSource NextNode => ref nextNode;

        UniTaskCompletionSourceCore<AsyncUnit> core;

        public static IUniTaskSource Create(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                OnCanceledTokenReceived(motionHandle, cancelBehaviour);
                return AutoResetUniTaskCompletionSource.CreateFromCanceled(cancellationToken, out token);
            }

            if (!pool.TryPop(out var result))
            {
                result = new UniTaskMotionConfiguredSource();
            }

            result.Initialize(motionHandle, cancelBehaviour, cancellationToken);

            TaskTracker.TrackActiveTask(result, 3);

            token = result.core.Version;
            return result;
        }

        protected override void SetTaskCanceled(CancellationToken cancellationToken)
        {
            core.TrySetCanceled(cancellationToken);
        }

        protected override void SetTaskCompleted()
        {
            core.TrySetResult(AsyncUnit.Default);
        }
        
        public void GetResult(short token)
        {
            try
            {
                core.GetResult(token);
            }
            finally
            {
                TryReturn();
            }
        }

        public UniTaskStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public UniTaskStatus UnsafeGetStatus()
        {
            return core.UnsafeGetStatus();
        }

        public void OnCompleted(Action<object> continuation, object state, short token)
        {
            core.OnCompleted(continuation, state, token);
        }

        bool TryReturn()
        {
            TaskTracker.RemoveTracking(this);
            core.Reset();
            
            DisposeRegistration();
            RestoreOriginalCallback();
            ResetFields();

            return pool.TryPush(this);
        }
    }
}
#endif