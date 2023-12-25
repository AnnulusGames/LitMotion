using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal static class MotionStorageManager
    {
        static readonly MinimumList<IMotionStorage> storageList = new();

        public static int CurrentStorageId { get; private set; }

        public static void AddStorage<TValue, TOptions, TAdapter>(MotionStorage<TValue, TOptions, TAdapter> storage)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            storageList.Add(storage);
            CurrentStorageId++;
        }

        public static void CompleteMotion(MotionHandle handle)
        {
            CheckStorageId(handle);
            storageList[handle.StorageId].Complete(handle);
        }

        public static void CancelMotion(MotionHandle handle)
        {
            CheckStorageId(handle);
            storageList[handle.StorageId].Cancel(handle);
        }

        public static bool IsActive(MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= CurrentStorageId) return false;
            return storageList[handle.StorageId].IsActive(handle);
        }

        public static MotionCallbackData GetMotionCallbacks(MotionHandle handle)
        {
            CheckStorageId(handle);
            return storageList[handle.StorageId].GetMotionCallbacks(handle);
        }

        public static void SetMotionCallbacks(MotionHandle handle, MotionCallbackData callbacks)
        {
            CheckStorageId(handle);
            storageList[handle.StorageId].SetMotionCallbacks(handle, callbacks);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void CheckStorageId(in MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= CurrentStorageId)
                throw new ArgumentException("Invalid storage id.");
        }
    }
}