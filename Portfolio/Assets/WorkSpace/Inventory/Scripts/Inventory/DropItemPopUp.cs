using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Cookie.RPG
{
    public class DropItemPopUp : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] TMP_Text _name;
        [SerializeField] Button _oK;
        [SerializeField] Button _cancle;

        public UnityEvent OnDropEvent;

        int _dropItemIndex;

        private void Start()
        {
            _cancle.onClick.AddListener(() => Hide());
            _oK.onClick.AddListener(() =>
            {
                _inventory.RemoveItem(_dropItemIndex);
                Hide();
            });
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                _oK.onClick.Invoke();
        }
        public void Show(int index)
        {
            _dropItemIndex = index;
            _name.text = _inventory.Items[index].Data.Name;
            UIManager.Instance.AddListAndShowPopUp(this.gameObject);
        }
        public void Hide()
        {
            UIManager.Instance.RemoveShowingPopUp(this.gameObject);
        }
    }

}
