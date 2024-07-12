using UnityEngine;

namespace Cookie.RPG
{
    public abstract class EquipmentableItemData : ItemData
    {
        // 장착 중인지?
        public bool IsEquiped => _isEquiped;
        // 체력
        public int Health => _health;
        // 방어력
        public int Defense => _defense;
        // 물리 공격력
        public int PhysicsDamage => _physicsDamage;
        // 마법 공격력
        public int MagicDamage => _magicDamage;
        // 이동속도
        public int MoveSpeed => _moveSpeed;

        [SerializeField] private bool _isEquiped;
        [SerializeField] private int _health;
        [SerializeField] private int _defense;
        [SerializeField] private int _magicDamage;
        [SerializeField] private int _physicsDamage;
        [SerializeField] private int _moveSpeed;
    }
}

