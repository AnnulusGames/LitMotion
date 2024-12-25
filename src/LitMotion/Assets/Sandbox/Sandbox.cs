using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Sandbox : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Slider slider;

    MotionHandle handle;

    void Start()
    {
        handle = LSequence.Create()
            .Append(LMotion.Create(-5f, 5f, 0.5f).BindToPositionX(target))
            .Append(LMotion.Create(0f, 5f, 0.5f).BindToPositionY(target))
            .Append(LMotion.Create(-2f, 2f, 1f).BindToPositionZ(target))
            .Append(LMotion.Create(5f, 0f, 0.5f).BindToPositionX(target))
            .Append(LMotion.Create(5f, 0f, 0.5f).BindToPositionY(target))
            .Append(LMotion.Create(2f, 0f, 1f).BindToPositionZ(target))
            .Run()
            .Preserve()
            .AddTo(this);

        slider.maxValue = (float)handle.TotalDuration;
    }

    void Update()
    {
        handle.Time = slider.value;
    }
}
