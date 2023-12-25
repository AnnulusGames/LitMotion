using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_Loops : MonoBehaviour
    {
        [SerializeField] Transform target1;
        [SerializeField] Transform target2;
        [SerializeField] Transform target3;

        void Start()
        {
            LMotion.Create(-5f, 5f, 1.5f)
                .WithEase(Ease.OutSine)
                .WithLoops(5, LoopType.Restart)
                .BindToPositionX(target1);

            LMotion.Create(-5f, 5f, 1.5f)
                .WithEase(Ease.OutSine)
                .WithLoops(5, LoopType.Yoyo)
                .BindToPositionX(target2);

            LMotion.Create(-5f, -3f, 1.5f)
                .WithEase(Ease.OutSine)
                .WithLoops(5, LoopType.Incremental)
                .BindToPositionX(target3);
        }
    }
}