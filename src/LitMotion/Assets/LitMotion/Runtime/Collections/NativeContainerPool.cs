using System;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion.Collections
{
    static class NativeContainerPoolInitializer
    {
        static void InitPools()
        {
            NativeContainerPool<NativeAnimationCurve>.Init(allocator => new NativeAnimationCurve(allocator));
        }

        static void Dispose()
        {
            NativeContainerPool<NativeAnimationCurve>.Dispose();
        }

#if !UNITY_EDITOR
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
            InitPools();
            UnityEngine.Application.quitting += Dispose;
        }
#endif

#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void InitEditor()
        {
            InitPools();
            AssemblyReloadEvents.beforeAssemblyReload += Dispose;
        }
#endif
    }

    public static class NativeContainerPool<T>
        where T : unmanaged
    {
        const int InitialSize = 128 * 1024;

        static AllocatorHelper<RewindableAllocator> allocatorHelper;

        // not a NativeQueue<T> because native containers cannot be nested
        static UnsafeQueue<T> pool;
        static Func<AllocatorManager.AllocatorHandle, T> factory;

        public static void Init(Func<AllocatorManager.AllocatorHandle, T> factory)
        {
            allocatorHelper = new AllocatorHelper<RewindableAllocator>(Allocator.Persistent);
            allocatorHelper.Allocator.Initialize(InitialSize, true);
            
            pool = new UnsafeQueue<T>(allocatorHelper.Allocator.Handle);
            NativeContainerPool<T>.factory = factory;
        }

        public static T Alloc()
        {
            if (!pool.TryDequeue(out var result))
            {
                result = factory(allocatorHelper.Allocator.Handle);
            }

            return result;
        }
        
        public static void Dispose()
        {
            allocatorHelper.Allocator.Dispose();
            allocatorHelper.Dispose();
        }
    }
}
