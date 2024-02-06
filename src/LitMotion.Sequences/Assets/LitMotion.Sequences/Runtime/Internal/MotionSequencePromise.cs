using System;

namespace LitMotion.Sequences
{
    internal sealed class MotionSequencePromise : ILinkedPoolNode<MotionSequencePromise>
    {
        sealed class MotionCompletionSource : ILinkedPoolNode<MotionCompletionSource>
        {
            static LinkedPool<MotionCompletionSource> pool;

            MotionCompletionSource()
            {
                onCancelCallbackDelegate = OnCanceledCallback;
                onCompleteCallbackDelegate = OnCompleteCallback;
            }

            public static MotionCompletionSource Create(MotionHandle handle, MotionSequencePromise promise)
            {
                if (!pool.TryGet(out var source)) source = new();

                source.promise = promise;

                var callbacks = MotionStorageManager.GetMotionCallbacks(handle);
                source.originalCancelAction = callbacks.OnCancelAction;
                source.originalCompleteAction = callbacks.OnCompleteAction;
                callbacks.OnCancelAction = source.onCancelCallbackDelegate;
                callbacks.OnCompleteAction = source.onCompleteCallbackDelegate;
                MotionStorageManager.SetMotionCallbacks(handle, callbacks);

                if (source.originalCancelAction == source.onCancelCallbackDelegate)
                {
                    source.originalCancelAction = null;
                }
                if (source.originalCompleteAction == source.onCompleteCallbackDelegate)
                {
                    source.originalCompleteAction = null;
                }

                return source;
            }

            public static void Return(MotionCompletionSource source)
            {
                source.promise = default;
                source.originalCancelAction = default;
                source.originalCompleteAction = default;
                pool.Return(source);
            }

            public ref MotionCompletionSource NextNode => ref nextNode;
            MotionCompletionSource nextNode;

            MotionSequencePromise promise;
            Action originalCompleteAction;
            Action originalCancelAction;

            readonly Action onCompleteCallbackDelegate;
            readonly Action onCancelCallbackDelegate;


            void OnCompleteCallback()
            {
                try
                {
                    originalCompleteAction?.Invoke();
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()(ex);
                }
                finally
                {
                    promise.IncrementCount();
                    Return(this);
                }
            }

            void OnCanceledCallback()
            {
                try
                {
                    originalCancelAction?.Invoke();
                }
                catch (Exception ex)
                {
                    MotionDispatcher.GetUnhandledExceptionHandler()(ex);
                }
                finally
                {
                    promise.IncrementCount();
                    Return(this);
                }
            }
        }

        static LinkedPool<MotionSequencePromise> pool;

        public ref MotionSequencePromise NextNode => ref nextNode;
        MotionSequencePromise nextNode;

        int handleCount;
        int completedCount;

        object state;
        Action<object> continuation;

        MotionSequencePromise() { }

        public static MotionSequencePromise Create(ReadOnlySpan<MotionHandle> handles, object state, Action<object> continuation)
        {
            if (!pool.TryGet(out var promise)) promise = new();

            promise.state = state;
            promise.continuation = continuation;

            promise.handleCount = handles.Length;
            foreach (var handle in handles)
            {
                MotionCompletionSource.Create(handle, promise);
            }

            return promise;
        }

        void IncrementCount()
        {
            completedCount++;
            if (handleCount <= completedCount)
            {
                continuation.Invoke(state);
            }
        }
    }
}