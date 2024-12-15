using System;
using System.Runtime.CompilerServices;
using LitMotion.Collections;

namespace LitMotion
{
    internal static class MotionManager
    {
        static FastListCore<IMotionStorage> list;

        public static int MotionTypeCount { get; private set; }

        public static void Register<TValue, TOptions, TAdapter>(MotionStorage<TValue, TOptions, TAdapter> storage)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            list.Add(storage);
            MotionTypeCount++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref MotionDataCore GetDataRef(MotionHandle handle, MotionStoragePermission permission)
        {
            CheckTypeId(handle);
            return ref list[handle.StorageId].GetDataRef(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref ManagedMotionData GetManagedDataRef(MotionHandle handle, MotionStoragePermission permission)
        {
            CheckTypeId(handle);
            return ref list[handle.StorageId].GetManagedDataRef(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MotionDebugInfo GetDebugInfo(MotionHandle handle)
        {
            CheckTypeId(handle);
            return list[handle.StorageId].GetDebugInfo(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Complete(MotionHandle handle, MotionStoragePermission permission)
        {
            CheckTypeId(handle);
            list[handle.StorageId].Complete(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryComplete(MotionHandle handle, MotionStoragePermission permission)
        {
            if (handle.StorageId < 0 || handle.StorageId >= MotionTypeCount) return false;
            return list[handle.StorageId].TryComplete(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Cancel(MotionHandle handle, MotionStoragePermission permission)
        {
            CheckTypeId(handle);
            list[handle.StorageId].Cancel(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryCancel(MotionHandle handle, MotionStoragePermission permission)
        {
            if (handle.StorageId < 0 || handle.StorageId >= MotionTypeCount) return false;
            return list[handle.StorageId].TryCancel(handle, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsActive(MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= MotionTypeCount) return false;
            return list[handle.StorageId].IsActive(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPlaying(MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= MotionTypeCount) return false;
            return list[handle.StorageId].IsPlaying(handle);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetTime(MotionHandle handle, double time, MotionStoragePermission permission)
        {
            CheckTypeId(handle);
            list[handle.StorageId].SetTime(handle, time, permission);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddToSequence(MotionHandle handle, out double motionDuration)
        {
            CheckTypeId(handle);
            list[handle.StorageId].AddToSequence(handle, out motionDuration);
        }

        // For MotionTracker
        public static (Type ValueType, Type OptionsType, Type AdapterType) GetMotionType(MotionHandle handle)
        {
            CheckTypeId(handle);
            var storageType = list[handle.StorageId].GetType();
            var valueType = storageType.GenericTypeArguments[0];
            var optionsType = storageType.GenericTypeArguments[1];
            var adapterType = storageType.GenericTypeArguments[2];
            return (valueType, optionsType, adapterType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static void CheckTypeId(in MotionHandle handle)
        {
            if (handle.StorageId < 0 || handle.StorageId >= MotionTypeCount)
            {
                throw new ArgumentException("Invalid type id.");
            }
        }
    }
}