using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace LitMotion.Sequences
{
    // Original implementation: https://github.com/Cysharp/ValueTaskSupplement/blob/master/src/ValueTaskSupplement/ValueTaskEx.WhenAll_Array.cs
    internal static class ValueTaskHelper
    {
        static readonly Action<object> AvailableContinuation = _ => { };
        static readonly Action<object> CompletedContinuation = _ => { };

        public static ValueTask WhenAll(MinimumList<ValueTask> tasks)
        {
            var promise = new WhenAllPromiseAll();
            promise.Run(tasks.AsSpan());
            return new ValueTask(promise, 0);
        }

        sealed class WhenAllPromiseAll : IValueTaskSource
        {
            static readonly ContextCallback execContextCallback = ExecutionContextCallback;
            static readonly SendOrPostCallback syncContextCallback = SynchronizationContextCallback;

            int taskCount = 0;
            int completedCount = 0;
            ExceptionDispatchInfo exception;
            Action<object> continuation = AvailableContinuation;
            Action<object> invokeContinuation;
            object state;
            SynchronizationContext syncContext;
            ExecutionContext execContext;

            public void Run(ReadOnlySpan<ValueTask> tasks)
            {
                taskCount = tasks.Length;

                var i = 0;
                foreach (var task in tasks)
                {
                    var awaiter = task.GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        try
                        {
                            awaiter.GetResult();
                        }
                        catch (Exception ex)
                        {
                            exception = ExceptionDispatchInfo.Capture(ex);
                            return;
                        }
                        TryInvokeContinuationWithIncrement();
                    }
                    else
                    {
                        RegisterContinuation(awaiter, i);
                    }

                    i++;
                }
            }

            void RegisterContinuation(ValueTaskAwaiter awaiter, int index)
            {
                awaiter.UnsafeOnCompleted(() =>
                {
                    try
                    {
                        awaiter.GetResult();
                    }
                    catch (Exception ex)
                    {
                        exception = ExceptionDispatchInfo.Capture(ex);
                        TryInvokeContinuation();
                        return;
                    }
                    TryInvokeContinuationWithIncrement();
                });
            }

            void TryInvokeContinuationWithIncrement()
            {
                if (Interlocked.Increment(ref completedCount) == taskCount)
                {
                    TryInvokeContinuation();
                }
            }

            void TryInvokeContinuation()
            {
                var c = Interlocked.Exchange(ref continuation, CompletedContinuation);
                if (c != AvailableContinuation && c != CompletedContinuation)
                {
                    var spinWait = new SpinWait();
                    while (state == null) // worst case, state is not set yet so wait.
                    {
                        spinWait.SpinOnce();
                    }

                    if (execContext != null)
                    {
                        invokeContinuation = c;
                        ExecutionContext.Run(execContext, execContextCallback, this);
                    }
                    else if (syncContext != null)
                    {
                        invokeContinuation = c;
                        syncContext.Post(syncContextCallback, this);
                    }
                    else
                    {
                        c(state);
                    }
                }
            }

            public void GetResult(short token)
            {
                if (exception != null)
                {
                    exception.Throw();
                }
            }

            public ValueTaskSourceStatus GetStatus(short token)
            {
                return (completedCount == taskCount) ? ValueTaskSourceStatus.Succeeded
                    : (exception != null) ? ((exception.SourceException is OperationCanceledException) ? ValueTaskSourceStatus.Canceled : ValueTaskSourceStatus.Faulted)
                    : ValueTaskSourceStatus.Pending;
            }

            public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
            {
                var c = Interlocked.CompareExchange(ref this.continuation, continuation, AvailableContinuation);
                if (c == CompletedContinuation)
                {
                    continuation(state);
                    return;
                }

                if (c != AvailableContinuation)
                {
                    throw new InvalidOperationException("does not allow multiple await.");
                }

                if (state == null)
                {
                    throw new InvalidOperationException("invalid state.");
                }

                if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) == ValueTaskSourceOnCompletedFlags.FlowExecutionContext)
                {
                    execContext = ExecutionContext.Capture();
                }
                if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) == ValueTaskSourceOnCompletedFlags.UseSchedulingContext)
                {
                    syncContext = SynchronizationContext.Current;
                }
                this.state = state;

                if (GetStatus(token) != ValueTaskSourceStatus.Pending)
                {
                    TryInvokeContinuation();
                }
            }

            static void ExecutionContextCallback(object state)
            {
                var self = (WhenAllPromiseAll)state;
                if (self.syncContext != null)
                {
                    self.syncContext.Post(syncContextCallback, self);
                }
                else
                {
                    var invokeContinuation = self.invokeContinuation!;
                    var invokeState = self.state;
                    self.invokeContinuation = null;
                    self.state = null;
                    invokeContinuation(invokeState);
                }
            }

            static void SynchronizationContextCallback(object state)
            {
                var self = (WhenAllPromiseAll)state;
                var invokeContinuation = self.invokeContinuation!;
                var invokeState = self.state;
                self.invokeContinuation = null;
                self.state = null;
                invokeContinuation(invokeState);
            }
        }

    }
}