using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Cookie.RPG
{
    public class ItemSlotUI : MonoBehaviour
    {
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _amount;

        public UnityEvent OnPointerEnter;
        public UnityEvent OnPointerExit;
        public int Index { get; private set; }
        public bool HasItem => Icon.sprite != null;
        public Image Icon { get => _icon; private set => _icon = value; }
        public int Amount { get; private set; }
        public RectTransform IconRectTransform { get; private set; }

        GameObject _highlight;
        void Start()
        {
            Init();
            AddListener();
        }
        private void Init()
        {
            _highlight = transform.GetChild(0).gameObject;
            IconRectTransform = _icon.rectTransform;
        }
        private void AddListener()
        {
            OnPointerEnter.AddListener(() => _highlight.SetActive(true));
            OnPointerExit.AddListener(() => _highlight.SetActive(false));
        }
        public void SetSlotIndex(int index) => Index = index;
        public void SetItem(Sprite itemSprite = null, int ItemAmount = 1)
        {
            // Sprite
            SetIcon(itemSprite);
            // Amount
            SetAmount(ItemAmount);
        }
        void SetIcon(Sprite sprite) => _icon.sprite = sprite;
        void SetAmount(int amount)
        {
            Amount = amount;

            if (Amount > 1)
                _amount.text = Amount.ToString();
            else
                _amount.text = "";
        }
    }
}
