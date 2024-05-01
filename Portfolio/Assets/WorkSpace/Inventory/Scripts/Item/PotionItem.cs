namespace Cookie.RPG
{
    public class PotionItem : CountableItem, IUsableItem
    {
        public PotionItem(PotionItemData data, int amount = 1) : base(data, amount) { }

        public bool Use()
        {
            Amount--;
            return true;
        }
    }
}

