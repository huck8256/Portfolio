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
            // Target UI ��ġ ����
            originPosition = target.position;
            // Pointer ��ġ ����
            downPosition = eventData.position;
        }
        public void OnDrag(PointerEventData eventData)
        {
            // Pointer�� ������ �Ÿ���ŭ �̵�
            target.position = originPosition + (eventData.position - downPosition);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
        }
    }
}


