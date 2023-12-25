using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_Easing : MonoBehaviour
    {
        [SerializeField] Transform target1;
        [SerializeField] Transform target2;
        [SerializeField] Transform target3;

        void Start()
        {
            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.Linear)
                .BindToPositionX(target1);

            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.InQuad)
                .BindToPositionX(target2);

            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.OutBounce)
                .BindToPositionX(target3);
        }
    }
}