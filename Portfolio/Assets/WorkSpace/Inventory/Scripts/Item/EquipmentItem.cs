using UnityEngine;

namespace Cookie.RPG
{
    public abstract class EquipmentItem : Item
    {
        public EquipmentItemData EquipmentItemData { get; private set; }
        public EquipmentItem(EquipmentItemData data) : base(data)
        {
            EquipmentItemData = data;
        }
    }
}

