using UnityEngine;

namespace Cookie.RPG
{
    public abstract class EquipmentItem : Item
    {
        public EquipmentableItemData EquipmentableItemData { get; private set; }
        public EquipmentItem(EquipmentableItemData data) : base(data)
        {
            EquipmentableItemData = data;
        }
    }
}

