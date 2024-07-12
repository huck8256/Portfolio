using UnityEngine;

namespace Cookie.RPG
{
    public abstract class CountableItem : Item
    {
        public CountableItemData CountableItemData { get; private set; }
        public int Amount { get; protected set; }
        public int MaxAmount => CountableItemData.MaxAmount;
        public bool IsMax => Amount >= CountableItemData.MaxAmount;
        public bool IsEmpty => Amount <= 0;
        public CountableItem(CountableItemData data, int amount = 1) : base(data)
        {
            CountableItemData = data;
            SetAmount(amount);
        }
        public void SetAmount(int amount)
        {
            Amount = Mathf.Clamp(amount, 0, MaxAmount);
        }
        public int AddAmountAndGetExcess(int amount)
        {
            int nextAmount = Amount + amount;
            SetAmount(nextAmount);

            return (nextAmount > MaxAmount) ? (nextAmount - MaxAmount) : 0;
        }
    }
}

