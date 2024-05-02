using TMPro;
using UnityEngine;

namespace Cookie.RPG
{
    public class ItemToolTipUI : MonoBehaviour
    {
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] TMP_Text _name;
        [SerializeField] TMP_Text _information;


        public void Show(ItemData itemData, RectTransform rectTransform)
        {
            _rectTransform.position = rectTransform.position + new Vector3(rectTransform.rect.width * 0.5f, -rectTransform.rect.height * 0.5f, 0f);
            _name.text = itemData.Name;
            _information.text = itemData.Tooltip;

            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
