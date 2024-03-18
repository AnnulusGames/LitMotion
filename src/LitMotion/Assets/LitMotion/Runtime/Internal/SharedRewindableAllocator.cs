using System.Runtime.CompilerServices;
using Unity.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion
{
    internal static class SharedRewindableAllocator
    {
#if UNITY_EDITOR
        [InitializeOnLoadMethod]
        static void InitEditor()
        {
            Create();
            AssemblyReloadEvents.beforeAssemblyReload += Dispose;
        }
#else
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        static void Init()
        {
            Create();
            UnityEngine.Application.quitting += Dispose;
        }
#endif

        const int InitialSize = 128 * 1024;
        static AllocatorHelper<RewindableAllocator> allocatorHelper;

        public static ref RewindableAllocator Allocator
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref allocatorHelper.Allocator;
            }
        }

        static void Create()
        {
            allocatorHelper = new AllocatorHelper<RewindableAllocator>(Unity.Collections.Allocator.Persistent);
            allocatorHelper.Allocator.Initialize(InitialSize, true);
        }

        static void Dispose()
        {
            allocatorHelper.Allocator.Dispose();
            allocatorHelper.Dispose();
        }
    }
}