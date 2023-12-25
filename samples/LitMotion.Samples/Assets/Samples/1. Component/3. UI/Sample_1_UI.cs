using UnityEngine;
using UnityEngine.UI;
using LitMotion;
using LitMotion.Extensions;

namespace LitMotionSamples
{
    public class Sample_1_UI : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] ButtonEventTrigger buttonTrigger;

        [Header("Hover")]
        [SerializeField] HoverEventTrigger hoverTrigger;
        [SerializeField] Image fillImage;

        void Start()
        {
            var buttonTransform = (RectTransform)buttonTrigger.transform;
            var buttonSize = buttonTransform.sizeDelta;
            buttonTrigger.onPointerDown.AddListener(_ =>
            {
                LMotion.Create(buttonSize, buttonSize - new Vector2(10f, 10f), 0.08f)
                    .BindToSizeDelta(buttonTransform);
            });
            buttonTrigger.onPointerUp.AddListener(_ =>
            {
                LMotion.Create(buttonSize - new Vector2(10f, 10f), buttonSize, 0.08f)
                    .BindToSizeDelta(buttonTransform);
            });

            hoverTrigger.onPointerEnter.AddListener(_ =>
            {
                fillImage.fillOrigin = 0;
                LMotion.Create(0f, 1f, 0.1f)
                    .BindToFillAmount(fillImage);
            });
            hoverTrigger.onPointerExit.AddListener(_ =>
            {
                fillImage.fillOrigin = 1;
                LMotion.Create(1f, 0f, 0.1f)
                    .BindToFillAmount(fillImage);
            });
        }
    }
}