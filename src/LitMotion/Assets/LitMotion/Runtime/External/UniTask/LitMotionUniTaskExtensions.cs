#if LITMOTION_SUPPORT_UNITASK
using System.Threading;
using Cysharp.Threading.Tasks;

namespace LitMotion
{
    /// <summary>
    /// Provides extension methods for UniTask integration.
    /// </summary>
    public static class LitMotionUniTaskExtensions
    {
        /// <summary>
        /// Convert motion handle to UniTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static UniTask ToUniTask(this MotionHandle handle, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(UniTaskMotionTaskSource.Create(handle, CancelBehavior.Cancel, true, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Convert motion handle to UniTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancelBehavior">Behavior when canceling</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static UniTask ToUniTask(this MotionHandle handle, CancelBehavior cancelBehavior, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(UniTaskMotionTaskSource.Create(handle, cancelBehavior, true, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Convert motion handle to UniTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancelBehavior">Behavior when canceling</param>
        /// <param name="cancelAwaitOnMotionCanceled">Whether to link MotionHandle.Cancel() to task cancellation</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static UniTask ToUniTask(this MotionHandle handle, CancelBehavior cancelBehavior, bool cancelAwaitOnMotionCanceled, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return UniTask.CompletedTask;
            return new UniTask(UniTaskMotionTaskSource.Create(handle, cancelBehavior, cancelAwaitOnMotionCanceled, cancellationToken, out var token), token);
        }

        /// <summary>
        /// Create a motion data and bind it to AsyncReactiveProperty.
        /// </summary>
        /// <typeparam name="TValue">The type of value to animate</typeparam>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="progress">Target object that implements IProgress</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAsyncReactiveProperty<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder, AsyncReactiveProperty<TValue> reactiveProperty)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            Error.IsNull(reactiveProperty);
            return builder.Bind(reactiveProperty, static (x, target) =>
            {
                target.Value = x;
            });
        }
    }
}
#endif