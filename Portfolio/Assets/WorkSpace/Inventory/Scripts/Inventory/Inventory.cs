using UnityEngine;
using UnityEngine.Events;

namespace Cookie.RPG
{
    public class Inventory : MonoBehaviour
    {
        public int MaxCapacity { get => _maxCapacity;}
        public Item[] Items { get => _items;}
        public UnityEvent OnUpdateItem { get => _onUpdateItem;}

        [SerializeField] Transform _dropItemPosition;  // 주인
        [SerializeField] int _maxCapacity;   // 최대 용량
        [SerializeField] ItemData[] _itemDatas; // 보유 아이템 데이터

        UnityEvent _onUpdateItem = new UnityEvent();

        Item[] _items;
        private void Awake()
        {
            _items = new Item[_maxCapacity];
        }
        private void Start()
        {
            // 기존에 가지고 있던 아이템 추가
            InitItem();
        }
        public void SwapItem(int index, int index2)
        {
            Item temp = _items[index];
            _items[index] = _items[index2];
            _items[index2] = temp;
        }
        public void AddItem(ItemData itemData, int amount = 1)
        {
            int index = 0;

            // 수량이 있는 아이템
            if (itemData is CountableItemData countableItemData)
            {
                // 남은 개수가 0개 일 때 까지 반복
                while(amount > 0)
                {
                    // 같은 아이템이고, 개수가 여유 있는 슬롯 찾기
                    index = FindCountableItemSlotIndex(countableItemData);

                    // 없을 경우
                    if (index == -1)
                    {
                        // 비어있는 슬롯 찾기
                        index = FindEmptySlotIndex();

                        // 인벤토리가 가득 차 있을 경우
                        if (index == -1)
                        {
                            DropItem(itemData, amount);
                            Debug.Log("인벤토리가 가득 차 있습니다.");
                            return;
                        }
                        else
                        {
                            // 아이템 생성
                            CountableItem countableItem = countableItemData.CreateItem() as CountableItem;
                            countableItem.SetAmount(amount);

                            // 인벤토리 추가
                            _items[index] = countableItem;

                            // 남은 개수 계산
                            amount = (amount > countableItemData.MaxAmount) ? (amount - countableItemData.MaxAmount) : 0;

                            Debug.Log($"{itemData.Name} {countableItem.Amount}개 획득");
                        }
                    }
                    else
                    {
                        if (_items[index] is CountableItem countableItem)
                        {
                            // 같은 아이템에 개수 추가 후, 남은 개수 반환
                            Debug.Log($"{countableItem.Data.Name} {amount}개 추가 획득");
                            amount = countableItem.AddAmountAndGetExcess(amount);
                        }
                    }
                }
                
            }
            // 수량이 없는 아이템
            else
            {
                // 비어있는 슬롯 찾기
                index = FindEmptySlotIndex();

                // 인벤토리가 가득 차 있을 경우
                if (index == -1)
                {
                    DropItem(itemData, amount);
                    Debug.Log("인벤토리가 가득 차 있습니다.");
                    return;
                }
                else
                {
                    // 아이템 생성
                    _items[index] = itemData.CreateItem();

                    amount--;
                }
            }
            _onUpdateItem.Invoke();
        }
        public void Use(int index)
        {
            if (_items[index] == null) return;

            if(_items[index] is IUsableItem usableItem)
            {
                if (usableItem.Use())
                {
                    if (_items[index] is CountableItem countableItem)
                    {
                        Debug.Log($"{countableItem.Data.Name} 사용");

                        if (countableItem.IsEmpty)
                        {
                            _items[index] = null;
                            Debug.Log($"{countableItem.Data.Name}을 모두 사용하였습니다.");
                        }
                    }
                    _onUpdateItem.Invoke();
                }
            }
        }
        public void RemoveItem(int index, int amount = 1)
        {
            if (Items[index] is CountableItem countableItem)
            {
                DropItem(countableItem.Data, amount);

                countableItem.SetAmount(countableItem.Amount - amount);

                if (countableItem.Amount == 0)
                    Items[index] = null;
            }
            else
            {
                DropItem(Items[index].Data);
                Items[index] = null;
            }

            _onUpdateItem.Invoke();
        }
        public void DropItem(ItemData itemData, int amount = 1)
        {
            Debug.Log($"{itemData.Name} {amount}개 버림");
            if(itemData.DropItemPrefab != null)
            {
                GameObject dropItemObj = Instantiate(itemData.DropItemPrefab);
                if (dropItemObj.TryGetComponent<DropItem>(out DropItem dropItem))
                {
                    dropItem.Amount = amount;
                    dropItem.transform.position = _dropItemPosition.position;
                }
                else
                    Debug.Log("is not DropItem");
            }
        }
        void InitItem()
        {
            for(int i = 0; i < _itemDatas.Length; i++)
            {
                if (_itemDatas[i] != null)
                    AddItem(_itemDatas[i]);
            }
        }
        int FindItemSlotIndex(ItemData itemData)
        {
            for(int i = 0; i < _maxCapacity; i++)
            {
                if(_items[i] != null)
                {
                    if (_items[i].Data == itemData)
                        return i;
                }
            }
            return -1;
        }
        int FindCountableItemSlotIndex(CountableItemData countableItemData)
        {
            for (int i = 0; i < _maxCapacity; i++)
            {
                if (_items[i] != null)
                {
                    if(_items[i].Data == countableItemData)
                    {
                        if (_items[i] is CountableItem countableItem)
                        {
                            if (countableItem.Amount < countableItem.MaxAmount)
                                return i;
                        }
                    }
                }
            }
            return -1;
        }
        int FindEmptySlotIndex()
        {
            for(int i = 0; i < _maxCapacity; i++)
            {
                if (_items[i] == null)
                    return i;
            }

            return -1;
        }
    }
}

