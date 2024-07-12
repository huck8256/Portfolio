using UnityEngine;

namespace Cookie.RPG
{
    interface IEquipmentableItem
    {
        // 아이템 장착 : 성공 여부 리턴
        bool Equip();
    }
}
