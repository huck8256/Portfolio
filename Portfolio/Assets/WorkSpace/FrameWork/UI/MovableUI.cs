using UnityEngine;
using UnityEngine.EventSystems;

namespace Cookie.RPG
{
    public class MovableUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform target;

        Vector2 originPosition;
        Vector2 downPosition;
        public void OnPointerDown(PointerEventData eventData)
        {
            // Target UI 위치 저장
            originPosition = target.position;
            // Pointer 위치 저장
            downPosition = eventData.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            // Pointer의 움직인 거리만큼 이동
            target.position = originPosition + (eventData.position - downPosition);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}


