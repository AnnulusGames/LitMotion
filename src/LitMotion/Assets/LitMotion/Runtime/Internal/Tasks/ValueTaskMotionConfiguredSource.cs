using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using LitMotion.Collections;

namespace LitMotion
{
    internal sealed class ValueTaskMotionConfiguredSource : MotionConfiguredSourceBase, IValueTaskSource, ILinkedPoolNode<ValueTaskMotionConfiguredSource>
    {
        static LinkedPool<ValueTaskMotionConfiguredSource> pool;

        ValueTaskMotionConfiguredSource nextNode;
        public ref ValueTaskMotionConfiguredSource NextNode => ref nextNode;

        ManualResetValueTaskSourceCore<object> core;

        static ValueTaskMotionConfiguredSource FromCanceled(out short token)
        {
            if (canceledSource == null)
            {
                canceledSource = new();
            }

            canceledSource.core.Reset();
            canceledSource.core.SetException(new OperationCanceledException());

            token = canceledSource.Version;
            return canceledSource;
        }
        static ValueTaskMotionConfiguredSource canceledSource;

        ValueTaskMotionConfiguredSource() : base() { }

        public static IValueTaskSource Create(MotionHandle motionHandle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken, out short token)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                OnCanceledTokenReceived(motionHandle, cancelBehaviour);
                return FromCanceled(out token);
            }

            if (!pool.TryPop(out var result))
            {
                result = new ValueTaskMotionConfiguredSource();
            }

            result.Initialize(motionHandle, cancelBehaviour, cancellationToken);

            token = result.core.Version;
            return result;
        }

        public short Version => core.Version;

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

        public ValueTaskSourceStatus GetStatus(short token)
        {
            return core.GetStatus(token);
        }

        public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            core.OnCompleted(continuation, state, token, flags);
        }

        bool TryReturn()
        {
            core.Reset();

            DisposeRegistration();
            RestoreOriginalCallback();
            ResetFields();

            return pool.TryPush(this);
        }

        protected override void SetTaskCanceled(CancellationToken cancellationToken)
        {
            core.SetException(new OperationCanceledException());
        }

        protected override void SetTaskCompleted()
        {
            core.SetResult(null);
        }
    }
}