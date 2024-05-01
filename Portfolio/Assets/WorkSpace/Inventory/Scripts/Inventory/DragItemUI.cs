using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Cookie.RPG
{
    public class DragItemUI : MonoBehaviour
    {
        [SerializeField] RectTransform _iconRectTransform;
        [SerializeField] Image _icon;
        [SerializeField] TMP_Text _amount;

        public void SetItemRectTransform(RectTransform rectTransform)
        {
            _iconRectTransform.position = rectTransform.position;
            _iconRectTransform.sizeDelta = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        }
        public void SetItem(Sprite sprite, int amount)
        {
            _icon.sprite = sprite;

            if (amount > 1)
                _amount.text = amount.ToString();
            else
                _amount.text = "";
        }
        public void ShowItem() => gameObject.SetActive(true);
        public void HideItem() => gameObject.SetActive(false);
    }
}
