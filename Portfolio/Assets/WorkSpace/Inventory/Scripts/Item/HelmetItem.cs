namespace Cookie.RPG
{
    public class HelmetItem : EquipmentItem, IEquipmentableItem 
    {
        public HelmetItem(EquipmentableItemData data) : base(data) { }

        public bool Equip()
        {
            return true;
        }
    }
}
