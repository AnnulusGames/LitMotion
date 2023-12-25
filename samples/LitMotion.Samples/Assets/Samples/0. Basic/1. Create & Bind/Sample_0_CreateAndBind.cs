using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_0_CreateAndBind : MonoBehaviour
    {
        [SerializeField] Transform targetTransform;
        [SerializeField] SpriteRenderer targetSpriteRenderer;
        [SerializeField] Text targetText;

        void Start()
        {
            LMotion.Create(-5f, 5f, 5f)
                .BindToPositionX(targetTransform);

            LMotion.Create(Color.red, Color.blue, 5f)
                .BindToColor(targetSpriteRenderer);

            LMotion.Create(0f, 10f, 5f)
                .Bind(x => targetText.text = x.ToString());
        }
    }
}