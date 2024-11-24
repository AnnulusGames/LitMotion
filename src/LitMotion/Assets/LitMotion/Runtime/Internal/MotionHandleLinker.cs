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
        public void Register(MotionHandle handle, LinkBehavior linkBehaviour)
        {
            switch (linkBehaviour)
            {
                case LinkBehavior.CancelOnDestroy:
                    cancelOnDestroyList.Add(handle);
                    break;
                case LinkBehavior.CancelOnDisable:
                    cancelOnDisableList.Add(handle);
                    break;
                case LinkBehavior.CompleteOnDisable:
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
