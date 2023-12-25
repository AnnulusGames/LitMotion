using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_1_Transform : MonoBehaviour
    {
        [SerializeField] Transform target1;
        [SerializeField] Transform target2;
        [SerializeField] Transform target3;

        void Start()
        {
            // Position
            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.InOutSine)
                .BindToPositionX(target1);

            // Position + Rotation
            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.InOutSine)
                .BindToPositionX(target2);
            LMotion.Create(0f, 180f, 3f)
                .WithEase(Ease.InOutSine)
                .BindToEulerAnglesZ(target2);

            // Position + Rotation + Scale
            LMotion.Create(-5f, 5f, 3f)
                .WithEase(Ease.InOutSine)
                .BindToPositionX(target3);
            LMotion.Create(0f, 180f, 3f)
                .WithEase(Ease.InOutSine)
                .BindToEulerAnglesZ(target3);
            LMotion.Create(new Vector3(1f, 1f, 1f), new Vector3(1.5f, 1.5f, 1.5f), 3f)
                .WithEase(Ease.InOutSine)
                .BindToLocalScale(target3);
        }
    }
}