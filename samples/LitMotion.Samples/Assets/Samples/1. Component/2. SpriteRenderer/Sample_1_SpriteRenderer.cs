using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_1_SpriteRenderer : MonoBehaviour
    {
        [SerializeField] SpriteRenderer target;

        CompositeMotionHandle handles;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                handles?.Complete();
                handles = new();

                LMotion.Create(Color.red, target.color, 0.3f)
                    .WithEase(Ease.OutQuad)
                    .BindToColor(target)
                    .AddTo(handles);

                LMotion.Create(Vector3.one * 2f, Vector3.one, 0.3f)
                    .WithEase(Ease.OutQuad)
                    .BindToLocalScale(target.transform)
                    .AddTo(handles);

                LMotion.Create(0f, 180f, 0.3f)
                    .WithEase(Ease.OutQuad)
                    .BindToEulerAnglesZ(target.transform)
                    .AddTo(handles);
            }
        }
    }
}