using System.Runtime.CompilerServices;
using UnityEngine;
using LitMotion.Collections;

namespace LitMotion
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    internal sealed class MotionHandleLinker : MonoBehaviour
    {
        FastListCore<MotionHandle> cancelOnDestroyList;
        FastListCore<MotionHandle> cancelOnDisableList;
        FastListCore<MotionHandle> completeOnDisableList;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Register(MotionHandle handle, LinkBehaviour linkBehaviour)
        {
            switch (linkBehaviour)
            {
                case LinkBehaviour.CancelOnDestroy:
                    cancelOnDestroyList.Add(handle);
                    break;
                case LinkBehaviour.CancelOnDisable:
                    cancelOnDisableList.Add(handle);
                    break;
                case LinkBehaviour.CompleteOnDisable:
                    completeOnDisableList.Add(handle);
                    break;
            }
        }

        void OnDisable()
        {
            var cancelSpan = cancelOnDisableList.AsSpan();
            for (int i = 0; i < cancelSpan.Length; i++)
            {
                ref var handle = ref cancelSpan[i];
                if (handle.IsActive()) handle.Cancel();
            }

            var completeSpan = completeOnDisableList.AsSpan();
            for (int i = 0; i < completeSpan.Length; i++)
            {
                ref var handle = ref completeSpan[i];
                if (handle.IsActive()) handle.Complete();
            }
        }

        void OnDestroy()
        {
            var span = cancelOnDestroyList.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                ref var handle = ref span[i];
                if (handle.IsActive()) handle.Cancel();
            }
        }
    }
}
