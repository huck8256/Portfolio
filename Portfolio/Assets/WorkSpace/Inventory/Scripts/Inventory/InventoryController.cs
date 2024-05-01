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

        ItemSlotUI _targetedSlot;   // Ŀ���� ��ġ�ߴ� Slot
        ItemSlotUI _targetingSlot;  // Ŀ���� ��ġ�� Slot
        ItemSlotUI _selectedSlot;   // Ŀ���� ������ Slot;

        Vector2 _beginDragPointPosition;
        Vector2 _selectetIconPosition;
        private void Start()
        {
            // �θ� Canvas�� GraphicRaycaster �Ҵ�
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
        #region PointerEventData ����
        private void OnEnable() => _pointerEventData = new PointerEventData(EventSystem.current);
        private void OnDisable() => _pointerEventData = null;
        #endregion
        #region MousePosition���� ���� Raycast�� ��, ù��°�� ����� ������� Component ��ȯ 
        T FirstRaycastResultAndGetComponent<T>() where T : Component
        {
            _raycastResultList.Clear();

            if(_pointerEventData != null)
            {
                _pointerEventData.position = Input.mousePosition;
                _graphicRaycaster.Raycast(_pointerEventData, _raycastResultList);
            }
            else
                Debug.Log("EventSystem�� �������� �ʽ��ϴ�.");

            // raycastResultList�� ���� ���� ��� null ��ȯ
            if (_raycastResultList.Count == 0)
                return null;

            // ���� ó��
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
                    // ToolTip ǥ��
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

                    // ���� ItemSlot ����
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

                        // ���콺 ������
                        _beginDragPointPosition = _pointerEventData.position;
                        _selectetIconPosition = _selectedSlot.IconRectTransform.position;

                        // ������ RectTransform ����
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

                    // Up ������ ���� �� ���
                    if(_targetingSlot != null)
                    {
                        // ���� ������ �ƴ� ���
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
                        // ���õ� ������ �������� ������ 1�� �̻��� ���
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
            // ���� ���콺 Ŀ�� ��ġ�� Slot�� ���� ��,
            if (_targetingSlot == null)
            {
                if (_targetedSlot != null)
                {
                    // ToolTip ����
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
                // ������ ���
                if (Input.GetMouseButtonDown(1))
                    _inventory.Use(_targetingSlot.Index);
            }
        }
    }
}

