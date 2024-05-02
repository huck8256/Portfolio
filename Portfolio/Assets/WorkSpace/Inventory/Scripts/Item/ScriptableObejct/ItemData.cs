using UnityEngine;

namespace Cookie.RPG
{
    public abstract class ItemData : ScriptableObject
    {
        public int ID => _iD;
        public string Name => _name;
        public string Tooltip => _toolTip;
        public Sprite IconSprite => _iconSprite;
        public GameObject DropItemPrefab => _dropItemPrefab;

        [SerializeField] private int _iD;
        [SerializeField] private string _name;    // ������ �̸�
        [Multiline]
        [SerializeField] private string _toolTip; // ������ ����
        [SerializeField] private Sprite _iconSprite; // ������ ������
        [SerializeField] private GameObject _dropItemPrefab; // �ٴڿ� ������ �� ������ ������

        public abstract Item CreateItem();
    }
}
