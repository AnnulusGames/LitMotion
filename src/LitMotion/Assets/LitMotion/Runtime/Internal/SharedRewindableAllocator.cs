using System.Runtime.CompilerServices;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal static class SharedRewindableAllocator<TKey>
    {
        const int InitialSize = 128 * 1024;
        static bool isCreated;
        static bool isInitialized;
        static AllocatorHelper<RewindableAllocator> allocatorHelper;

        public static ref RewindableAllocator Allocator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Create();
                return ref allocatorHelper.Allocator;
            }
        }

        static void Create()
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

            if (isCreated) return;

            allocatorHelper = new AllocatorHelper<RewindableAllocator>(Unity.Collections.Allocator.Persistent);
            allocatorHelper.Allocator.Initialize(InitialSize, true);

            isCreated = true;
        }

        static void Dispose()
        {
            if (!isCreated) return;

            allocatorHelper.Allocator.Dispose();
            allocatorHelper.Dispose();
            isCreated = false;
        }
    }
}