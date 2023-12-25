using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_CancelAndComplete : MonoBehaviour
    {
        [SerializeField] Transform target1;
        [SerializeField] Transform target2;

        MotionHandle handle1;
        MotionHandle handle2;

        void Start()
        {
            handle1 = LMotion.Create(-5f, 5f, 2f)
                .WithLoops(999)
                .BindToPositionX(target1);

            handle2 = LMotion.Create(-5f, 5f, 2f)
                .WithLoops(999)
                .BindToPositionX(target2);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (handle1.IsActive()) handle1.Cancel();
                if (handle2.IsActive()) handle2.Complete();
            }
        }
    }
}