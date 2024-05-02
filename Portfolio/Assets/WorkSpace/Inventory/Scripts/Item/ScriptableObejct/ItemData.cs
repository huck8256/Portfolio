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
        [SerializeField] private string _name;    // 아이템 이름
        [Multiline]
        [SerializeField] private string _toolTip; // 아이템 설명
        [SerializeField] private Sprite _iconSprite; // 아이템 아이콘
        [SerializeField] private GameObject _dropItemPrefab; // 바닥에 떨어질 때 생성할 프리팹

        public abstract Item CreateItem();
    }
}
