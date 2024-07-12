using UnityEngine;

namespace Cookie.RPG
{
    public abstract class EquipmentableItemData : ItemData
    {
        // ���� ������?
        public bool IsEquiped => _isEquiped;
        // ü��
        public int Health => _health;
        // ����
        public int Defense => _defense;
        // ���� ���ݷ�
        public int PhysicsDamage => _physicsDamage;
        // ���� ���ݷ�
        public int MagicDamage => _magicDamage;
        // �̵��ӵ�
        public int MoveSpeed => _moveSpeed;

        [SerializeField] private bool _isEquiped;
        [SerializeField] private int _health;
        [SerializeField] private int _defense;
        [SerializeField] private int _magicDamage;
        [SerializeField] private int _physicsDamage;
        [SerializeField] private int _moveSpeed;
    }
}

