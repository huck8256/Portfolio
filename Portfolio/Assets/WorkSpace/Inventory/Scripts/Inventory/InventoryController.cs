using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Cookie.RPG
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] DragItemUI _dragItemUI;
        [SerializeField] ItemToolTipUI _itemToolTipUI;
        [SerializeField] DropItemPopUp _dropItemPopUp;
        [SerializeField] InputAmountDropItemPopUp _inputAmountDropItemPopUp;
        GraphicRaycaster _graphicRaycaster;
        PointerEventData _pointerEventData;
        List<RaycastResult> _raycastResultList = new List<RaycastResult>();

        ItemSlotUI _targetedSlot;   // 커서가 위치했던 Slot
        ItemSlotUI _targetingSlot;  // 커서가 위치한 Slot
        ItemSlotUI _selectedSlot;   // 커서가 선택한 Slot;

        Vector2 _beginDragPointPosition;
        Vector2 _selectetIconPosition;
        private void Start()
        {
            // 부모 Canvas의 GraphicRaycaster 할당
            _graphicRaycaster = _inventory.GetComponentInParent<GraphicRaycaster>();
        }
        private void Update()
        {
            _targetingSlot = FirstRaycastResultAndGetComponent<ItemSlotUI>();

            OnPointerEnter();
            OnPointerDown();
            OnPointerDrag();
            OnPointerUp();
            OnPointerExit();
            OnMouseRightButtonDown();
        }
        #region PointerEventData 갱신
        private void OnEnable() => _pointerEventData = new PointerEventData(EventSystem.current);
        private void OnDisable() => _pointerEventData = null;
        #endregion
        #region MousePosition값에 따라 Raycast를 쏴, 첫번째로 검출된 결과값의 Component 반환 
        T FirstRaycastResultAndGetComponent<T>() where T : Component
        {
            _raycastResultList.Clear();

            if(_pointerEventData != null)
            {
                _pointerEventData.position = Input.mousePosition;
                _graphicRaycaster.Raycast(_pointerEventData, _raycastResultList);
            }
            else
                Debug.Log("EventSystem이 존재하지 않습니다.");

            // raycastResultList에 값이 없을 경우 null 반환
            if (_raycastResultList.Count == 0)
                return null;

            // 예외 처리
            if (_raycastResultList[0].gameObject.TryGetComponent<T>(out T component))
                return component;
            else
                return null;
        }
        #endregion

        void OnPointerEnter()
        {
            if (_targetingSlot != null)
            {
                if(_inventory.Items[_targetingSlot.Index] != null)
                {
                    // ToolTip 표시
                    _itemToolTipUI.Show(_inventory.Items[_targetingSlot.Index].Data, _targetingSlot.IconRectTransform);
                }
                else
                {
                    _itemToolTipUI.Hide();
                }
                if (_targetingSlot != _targetedSlot)
                {
                    _targetingSlot.OnPointerEnter.Invoke();

                    if (_targetedSlot != null)
                        _targetedSlot.OnPointerExit.Invoke();

                    // 현재 ItemSlot 저장
                    _targetedSlot = _targetingSlot;
                }
            }
        }
        void OnPointerDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_targetingSlot != null)
                {
                    if(_targetingSlot.HasItem)
                    {
                        _selectedSlot = _targetingSlot;

                        // 마우스 포지션
                        _beginDragPointPosition = _pointerEventData.position;
                        _selectetIconPosition = _selectedSlot.IconRectTransform.position;

                        // 아이콘 RectTransform 설정
                        _dragItemUI.SetItemRectTransform(_selectedSlot.IconRectTransform);

                        _dragItemUI.SetItem(_selectedSlot.Icon.sprite, _selectedSlot.Amount);
                        _dragItemUI.ShowItem();
                    }
                }
            }
        }
        void OnPointerDrag()
        {
            if(_selectedSlot != null)
            {
                _dragItemUI.transform.position = _selectetIconPosition + (_pointerEventData.position - _beginDragPointPosition);
            }
        }
        void OnPointerUp()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if(_selectedSlot != null)
                {
                    _dragItemUI.HideItem();

                    // Up 지점이 슬롯 일 경우
                    if(_targetingSlot != null)
                    {
                        // 같은 슬롯이 아닐 경우
                        if(_targetingSlot != _selectedSlot)
                        { 
                            Sprite spriteTemp = _selectedSlot.Icon.sprite;
                            int amountTemp = _selectedSlot.Amount;

                            _inventory.SwapItem(_selectedSlot.Index, _targetingSlot.Index);
                            _selectedSlot.SetItem(_targetingSlot.Icon.sprite, _targetingSlot.Amount);
                            _targetingSlot.SetItem(spriteTemp, amountTemp);
                        }
                    }
                    else
                    {
                        // 선택된 슬롯의 아이템의 개수가 1개 이상일 경우
                        if(_selectedSlot.Amount > 1)
                            _inputAmountDropItemPopUp.Show(_selectedSlot.Index);
                        else
                            _dropItemPopUp.Show(_selectedSlot.Index);
                    }
                    _selectedSlot = null;
                }
            }
        }
        void OnPointerExit()
        {
            // 현재 마우스 커서 위치에 Slot이 없을 때,
            if (_targetingSlot == null)
            {
                if (_targetedSlot != null)
                {
                    // ToolTip 숨김
                    _itemToolTipUI.Hide();

                    _targetedSlot.OnPointerExit.Invoke();

                    _targetedSlot = null;
                }
            }
        }
        void OnMouseRightButtonDown()
        {
            if (_targetingSlot != null)
            {
                // 아이템 사용
                if (Input.GetMouseButtonDown(1))
                    _inventory.Use(_targetingSlot.Index);
            }
        }
    }
}

