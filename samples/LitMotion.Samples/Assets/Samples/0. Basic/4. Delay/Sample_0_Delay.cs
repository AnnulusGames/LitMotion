using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_Delay : MonoBehaviour
    {
        [SerializeField] Transform[] targets;

        void Start()
        {
            for (int i = 0; i < targets.Length; i++)
            {
                LMotion.Create(-5f, 5f, 2f)
                    .WithEase(Ease.InOutSine)
                    .WithDelay(i * 0.2f)
                    .BindToPositionX(targets[i]);
            }
        }
    }
}