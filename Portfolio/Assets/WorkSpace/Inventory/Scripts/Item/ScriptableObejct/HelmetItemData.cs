using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cookie.RPG
{
    [CreateAssetMenu(fileName = "Equipment_", menuName = "Scriptable Object/Item Data/Equipmentable/Helmet", order = int.MaxValue)]
    public class HelmetItemData : EquipmentableItemData
    {
        public override Item CreateItem()
        {
            return new HelmetItem(this);
        }
    }
}
