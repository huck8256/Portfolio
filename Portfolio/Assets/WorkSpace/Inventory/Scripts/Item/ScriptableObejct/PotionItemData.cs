using UnityEngine;

namespace Cookie.RPG
{
    [CreateAssetMenu(fileName = "Potion_", menuName = "Scriptable Object/Item Data/Countable/Potion", order = int.MaxValue)]
    public class PotionItemData : CountableItemData
    {
        public int Value => _value;

        [SerializeField] int _value;
        public override Item CreateItem()
        {
            return new PotionItem(this);
        }
    }
}
