using System;
using System.Runtime.CompilerServices;
using LitMotion.Collections;

namespace LitMotion
{
    internal static class MotionStorageManager
    {
        static FastListCore<IMotionStorage> storageList = new(16);

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

        public static float GetMotionPlaybackSpeed(MotionHandle handle)
        {
            CheckStorageId(handle);
            return storageList[handle.StorageId].GetPlaybackSpeed(handle);
        }

        public static void SetMotionPlaybackSpeed(MotionHandle handle, float value)
        {
            if (value < 0f) throw new ArgumentOutOfRangeException("Playback speed must be 0 or greater.");
            CheckStorageId(handle);
            storageList[handle.StorageId].SetPlaybackSpeed(handle, value);
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

        // For MotionTracker
        public static (Type ValueType, Type OptionsType, Type AdapterType) GetMotionType(MotionHandle handle)
        {
            CheckStorageId(handle);
            var storageType = storageList[handle.StorageId].GetType();
            var valueType = storageType.GenericTypeArguments[0];
            var optionsType = storageType.GenericTypeArguments[1];
            var adapterType = storageType.GenericTypeArguments[2];
            return (valueType, optionsType, adapterType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void CheckStorageId(in MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= CurrentStorageId)
                throw new ArgumentException("Invalid storage id.");
        }
    }
}