using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace LitMotionSamples
{
    public class HoverEventTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent<PointerEventData> onPointerEnter;
        public UnityEvent<PointerEventData> onPointerExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit.Invoke(eventData);
        }
    }
}