using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal static class RewindableAllocatorFactory
    {
        const int InitialSize = 128 * 1024;
        static bool isInitialized;
        static readonly Stack<AllocatorHelper<RewindableAllocator>> allocators = new();

        public static AllocatorHelper<RewindableAllocator> CreateAllocator()
        {
            Initialize();

            var allocatorHelper = new AllocatorHelper<RewindableAllocator>(Allocator.Persistent);
            allocatorHelper.Allocator.Initialize(InitialSize, true);
            allocators.Push(allocatorHelper);

            return allocatorHelper;
        }

        static void Initialize()
        {
            if (!isInitialized)
            {
#if UNITY_EDITOR
                AssemblyReloadEvents.beforeAssemblyReload += Dispose;
#else
                UnityEngine.Application.quitting += Dispose;
#endif
                isInitialized = true;
            }
        }

        static void Dispose()
        {
            while (allocators.TryPop(out var allocatorHelper))
            {
                allocatorHelper.Allocator.Dispose();
                allocatorHelper.Dispose();
            }
        }
    }
}