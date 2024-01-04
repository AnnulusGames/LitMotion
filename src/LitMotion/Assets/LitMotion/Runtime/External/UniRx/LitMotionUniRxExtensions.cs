#if LITMOTION_SUPPORT_UNIRX
using System;
using UniRx;
using UnityEngine.Assertions;

namespace LitMotion
{
    public static class LitMotionUniRxExtensions
    {
        public static IObservable<TValue> ToObservable<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            var subject = new Subject<TValue>();
            var callbacks = builder.BuildCallbackData(subject, (x, subject) => subject.OnNext(x));
            callbacks.OnCompleteAction = builder.buffer.OnComplete;
            callbacks.OnCompleteAction += () => subject.OnCompleted();
            var scheduler = builder.buffer.Scheduler;
            var entity = builder.BuildMotionData();

            builder.Schedule(scheduler, entity, callbacks);
            return subject;
        }

        public static MotionHandle BindToReactiveProperty<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, ReactiveProperty<TValue> reactiveProperty)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Error.IsNull(reactiveProperty);
            return builder.BindWithState(reactiveProperty, (x, target) =>
            {
                target.Value = x;
            });
        }
    }
}
#endif