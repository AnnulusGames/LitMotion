using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class Sandbox : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        LMotion.Create(0f, 5f, 2f)
            .WithEase(Ease.OutQuint)
            .WithLoops(-1, LoopType.Yoyo)
            .BindToPositionX(target)
            .AddTo(target);
    }
}
