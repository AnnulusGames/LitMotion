using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_Callback : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] Text text;

        void Start()
        {
            LMotion.Create(-5f, 5f, 2f)
                .WithEase(Ease.InOutSine)
                .WithOnComplete(() => text.text = "Complete!")
                .BindToPositionX(target);
        }
    }
}