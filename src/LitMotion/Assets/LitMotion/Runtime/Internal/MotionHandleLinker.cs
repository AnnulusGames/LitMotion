using System.Collections.Generic;
using UnityEngine;

namespace LitMotion
{
    [DisallowMultipleComponent]
    [AddComponentMenu("")]
    internal sealed class MotionHandleLinker : MonoBehaviour
    {
        readonly List<MotionHandle> handleList = new(8);

        public void Register(MotionHandle handle)
        {
            handleList.Add(handle);
        }

        void OnDestroy()
        {
            for (int i = 0; i < handleList.Count; i++)
            {
                var handle = handleList[i];
                if (handle.IsActive()) handle.Cancel();
            }
        }
    }
}
