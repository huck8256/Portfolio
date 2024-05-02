using UnityEngine;

namespace Cookie.RPG
{
    public class DropItem : MonoBehaviour
    {
        [SerializeField] ItemData _itemData;
        [SerializeField] int _amount = 1;
        [SerializeField] Outline _outline;
        public ItemData ItemData { get => _itemData; }
        public int Amount { get => _amount; set => _amount = value; }

        public void DestroyItem() => Destroy(this.gameObject);
        private void Awake()
        {
            gameObject.tag = "Item";
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
                _outline.OutlineWidth = 10f;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
                _outline.OutlineWidth = 0f;
        }
    }
}
