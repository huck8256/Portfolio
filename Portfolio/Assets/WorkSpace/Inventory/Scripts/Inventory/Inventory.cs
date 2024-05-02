using UnityEngine;
using UnityEngine.Events;

namespace Cookie.RPG
{
    public class Inventory : MonoBehaviour
    {
        public int MaxCapacity { get => _maxCapacity;}
        public Item[] Items { get => _items;}
        public UnityEvent OnUpdateItem { get => _onUpdateItem;}

        [SerializeField] Transform _dropItemPosition;  // ����
        [SerializeField] int _maxCapacity;   // �ִ� �뷮
        [SerializeField] ItemData[] _itemDatas; // ���� ������ ������

        UnityEvent _onUpdateItem = new UnityEvent();

        Item[] _items;
        private void Awake()
        {
            _items = new Item[_maxCapacity];
        }
        private void Start()
        {
            // ������ ������ �ִ� ������ �߰�
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

            // ������ �ִ� ������
            if (itemData is CountableItemData countableItemData)
            {
                // ���� ������ 0�� �� �� ���� �ݺ�
                while(amount > 0)
                {
                    // ���� �������̰�, ������ ���� �ִ� ���� ã��
                    index = FindCountableItemSlotIndex(countableItemData);

                    // ���� ���
                    if (index == -1)
                    {
                        // ����ִ� ���� ã��
                        index = FindEmptySlotIndex();

                        // �κ��丮�� ���� �� ���� ���
                        if (index == -1)
                        {
                            DropItem(itemData, amount);
                            Debug.Log("�κ��丮�� ���� �� �ֽ��ϴ�.");
                            return;
                        }
                        else
                        {
                            // ������ ����
                            CountableItem countableItem = countableItemData.CreateItem() as CountableItem;
                            countableItem.SetAmount(amount);

                            // �κ��丮 �߰�
                            _items[index] = countableItem;

                            // ���� ���� ���
                            amount = (amount > countableItemData.MaxAmount) ? (amount - countableItemData.MaxAmount) : 0;

                            Debug.Log($"{itemData.Name} {countableItem.Amount}�� ȹ��");
                        }
                    }
                    else
                    {
                        if (_items[index] is CountableItem countableItem)
                        {
                            // ���� �����ۿ� ���� �߰� ��, ���� ���� ��ȯ
                            Debug.Log($"{countableItem.Data.Name} {amount}�� �߰� ȹ��");
                            amount = countableItem.AddAmountAndGetExcess(amount);
                        }
                    }
                }
                
            }
            // ������ ���� ������
            else
            {
                // ����ִ� ���� ã��
                index = FindEmptySlotIndex();

                // �κ��丮�� ���� �� ���� ���
                if (index == -1)
                {
                    DropItem(itemData, amount);
                    Debug.Log("�κ��丮�� ���� �� �ֽ��ϴ�.");
                    return;
                }
                else
                {
                    // ������ ����
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
                        Debug.Log($"{countableItem.Data.Name} ���");

                        if (countableItem.IsEmpty)
                        {
                            _items[index] = null;
                            Debug.Log($"{countableItem.Data.Name}�� ��� ����Ͽ����ϴ�.");
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
            Debug.Log($"{itemData.Name} {amount}�� ����");
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

