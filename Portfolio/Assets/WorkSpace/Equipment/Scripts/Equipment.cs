using UnityEngine;

namespace Cookie.RPG
{
    public enum EquipmentType
    {
        Helmet,
        Armor,
        Pants,
        Boots,
        Weapon,
        Shield
    }

    public class Equipment : MonoBehaviour
    {
        public Item[] Items { get => _items; }

        Item[] _items;
        public Item Equip(Item item)
        {
            return item; 
        }
    }
}
