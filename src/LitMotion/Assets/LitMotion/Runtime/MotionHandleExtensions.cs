using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace LitMotion
{
    /// <summary>
    /// Provides methods for manipulating the motion entity pointed to by MotionHandle.
    /// </summary>
    public static class MotionHandleExtensions
    {
        /// <summary>
        /// Checks if a motion is currently playing.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <returns>True if motion is active, otherwise false.</returns>
        public static bool IsActive(this MotionHandle handle)
        {
            return MotionStorageManager.IsActive(handle);
        }

        /// <summary>
        /// Complete motion.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        public static void Complete(this MotionHandle handle)
        {
            MotionStorageManager.CompleteMotion(handle);
        }

        /// <summary>
        /// Cancel motion.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        public static void Cancel(this MotionHandle handle)
        {
            MotionStorageManager.CancelMotion(handle);
        }

        /// <summary>
        /// Add this motion handle to CompositeMotionHandle.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="compositeMotionHandle">target CompositeMotionHandle</param>
        public static MotionHandle AddTo(this MotionHandle handle, CompositeMotionHandle compositeMotionHandle)
        {
            compositeMotionHandle.Add(handle);
            return handle;
        }

        /// <summary>
        /// Link the motion lifecycle to the target object.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="target">Target object</param>
        public static MotionHandle AddTo(this MotionHandle handle, GameObject target)
        {
            GetOrAddComponent<MotionHandleLinker>(target).Register(handle, LinkBehaviour.CancelOnDestroy);
            return handle;
        }

        /// <summary>
        /// Link the motion lifecycle to the target object.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="target">Target object</param>
        /// <param name="linkBehaviour">Link behaviour</param>
        public static MotionHandle AddTo(this MotionHandle handle, GameObject target, LinkBehaviour linkBehaviour)
        {
            GetOrAddComponent<MotionHandleLinker>(target).Register(handle, linkBehaviour);
            return handle;
        }

        /// <summary>
        /// Link the motion lifecycle to the target object.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="target">Target object</param>
        public static MotionHandle AddTo(this MotionHandle handle, Component target)
        {
            GetOrAddComponent<MotionHandleLinker>(target.gameObject).Register(handle, LinkBehaviour.CancelOnDestroy);
            return handle;
        }

        /// <summary>
        /// Link the motion lifecycle to the target object.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="target">Target object</param>
        /// <param name="linkBehaviour">Link behaviour</param>
        public static MotionHandle AddTo(this MotionHandle handle, Component target, LinkBehaviour linkBehaviour)
        {
            GetOrAddComponent<MotionHandleLinker>(target.gameObject).Register(handle, linkBehaviour);
            return handle;
        }

#if UNITY_2022_2_OR_NEWER
        /// <summary>
        /// Link the motion lifecycle to the target object.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="target">Target object</param>
        public static MotionHandle AddTo(this MotionHandle handle, MonoBehaviour target)
        {
            target.destroyCancellationToken.Register(() =>
            {
                if (handle.IsActive()) handle.Cancel();
            }, false);
            return handle;
        }
#endif

        static TComponent GetOrAddComponent<TComponent>(GameObject target) where TComponent : Component
        {
            if (!target.TryGetComponent<TComponent>(out var component))
            {
                component = target.AddComponent<TComponent>();
            }
            return component;
        }

        /// <summary>
        /// Convert MotionHandle to IDisposable.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <returns></returns>
        public static IDisposable ToDisposable(this MotionHandle handle)
        {
            return new MotionHandleDisposable(handle);
        }

        /// <summary>
        /// Wait for the motion to finish in a coroutine.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <returns></returns>
        public static IEnumerator ToYieldInteraction(this MotionHandle handle)
        {
            while (handle.IsActive())
            {
                yield return null;
            }
        }

        public static MotionAwaiter GetAwaiter(this MotionHandle handle)
        {
            return new MotionAwaiter(handle);
        }

        /// <summary>
        /// Convert motion handle to ValueTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static ValueTask ToValueTask(this MotionHandle handle, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return default;
            var source = ValueTaskMotionConfiguredSource.Create(handle, CancelBehaviour.CancelAndCancelAwait, cancellationToken, out var token);
            return new ValueTask(source, token);
        }

        /// <summary>
        /// Convert motion handle to ValueTask.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancelBehaviour">Behavior when canceling</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static ValueTask ToValueTask(this MotionHandle handle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return default;
            var source = ValueTaskMotionConfiguredSource.Create(handle, cancelBehaviour, cancellationToken, out var token);
            return new ValueTask(source, token);
        }

#if UNITY_2023_1_OR_NEWER
        /// <summary>
        /// Convert motion handle to Awaitable.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static Awaitable ToAwaitable(this MotionHandle handle, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return AwaitableMotionConfiguredSource.CompletedSource.Awaitable;
            return AwaitableMotionConfiguredSource.Create(handle, CancelBehaviour.CancelAndCancelAwait, cancellationToken).Awaitable;
        }

        /// <summary>
        /// Convert motion handle to Awaitable.
        /// </summary>
        /// <param name="handle">This motion handle</param>
        /// <param name="cancelBehaviour">Behavior when canceling</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns></returns>
        public static Awaitable ToAwaitable(this MotionHandle handle, CancelBehaviour cancelBehaviour, CancellationToken cancellationToken = default)
        {
            if (!handle.IsActive()) return AwaitableMotionConfiguredSource.CompletedSource.Awaitable;
            return AwaitableMotionConfiguredSource.Create(handle, cancelBehaviour, cancellationToken).Awaitable;
        }
#endif

    }

    internal sealed class MotionHandleDisposable : IDisposable
    {
        public MotionHandleDisposable(MotionHandle handle) => this.handle = handle;
        public readonly MotionHandle handle;

        public void Dispose()
        {
            if (handle.IsActive()) handle.Cancel();
        }
    }
}