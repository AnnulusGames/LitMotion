using LitMotion;
using LitMotion.Extensions;
using LitMotion.Sequences;
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
            .Run();
    }

    void Update()
    {
        handle.Time = slider.value;
    }
}
