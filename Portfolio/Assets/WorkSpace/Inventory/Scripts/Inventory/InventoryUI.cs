using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Cookie.RPG
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] GameObject _slotPrefab;

        Transform _window;
        Transform _headerArea;
        Transform _contentArea;

        Button _closeButton;

        ItemSlotUI[] _itemSlotUIs;
        int _maxCapacity;
        bool _isOpen => _window.gameObject.activeSelf;
        void Start()
        {
            Init();
            CreateSlot();
            AddListener();
        }
        public void Window()
        {
            if (_window.gameObject.activeSelf)
                UIManager.Instance.RemoveShowingPopUp(_window.gameObject);
            else
                UIManager.Instance.AddListAndShowPopUp(_window.gameObject);
        }
        void Init()
        {
            _window = transform.GetChild(0);
            _headerArea = _window.GetChild(0);
            _contentArea = _window.GetChild(1);

            _closeButton = _headerArea.GetChild(1).GetComponent<Button>();

            _maxCapacity = _inventory.MaxCapacity;
            _itemSlotUIs = new ItemSlotUI[_maxCapacity];
        }
        void AddListener()
        {
            // exit 버튼 설정
            _closeButton.onClick.AddListener(() => UIManager.Instance.RemoveShowingPopUp(_window.gameObject));

            // OnEnable 이벤트 설정
            if (_window.TryGetComponent<LifeCycleEvent>(out LifeCycleEvent lifeCycleEvent))
                lifeCycleEvent.onEnableEvent.AddListener(() => UpdateSlot());
            else
                _window.AddComponent<LifeCycleEvent>().onEnableEvent.AddListener(() => UpdateSlot());

            // Inventory Update시, UI Update
            _inventory.OnUpdateItem.AddListener(() =>
            {
                if (_isOpen)
                    UpdateSlot();
            });
        }
        void CreateSlot()
        {
            for(int i = 0; i < _maxCapacity; i++)
            {
                _itemSlotUIs[i] = Instantiate(_slotPrefab, _contentArea).GetComponent<ItemSlotUI>();
                _itemSlotUIs[i].SetSlotIndex(i);
            }
        }
        void UpdateSlot()
        {
            for(int i = 0; i < _maxCapacity; i++)
            {
                // 비어있지 않을 경우
                if(_inventory.Items[i] != null)
                {
                    // 셀 수 있는  아이템일 경우
                    if (_inventory.Items[i] is CountableItem countableItem)
                        _itemSlotUIs[i].SetItem(countableItem.Data.IconSprite, countableItem.Amount);
                    else
                        _itemSlotUIs[i].SetItem(_inventory.Items[i].Data.IconSprite);
                }
                else
                {
                    _itemSlotUIs[i].SetItem();
                }
            }
        }
    }
}