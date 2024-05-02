using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

namespace Cookie.RPG
{
    public class InputAmountDropItemPopUp : MonoBehaviour
    {
        [SerializeField] Inventory _inventory;

        [SerializeField] TMP_Text _name;
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] Button _minus;
        [SerializeField] Button _plus;
        [SerializeField] Button _min;
        [SerializeField] Button _max;
        [SerializeField] Button _oK;
        [SerializeField] Button _cancel;

        public UnityEvent<int> OnAmountDropItem;
        int _maxAmount;
        int _dropItemIndex;
        private void Start()
        {
            _minus.onClick.AddListener(() =>
            {
                int.TryParse(_inputField.text, out int amount);

                if (amount > 1)
                {
                    int nextAmount = --amount;

                    _inputField.text = nextAmount.ToString();
                }
            });
            _plus.onClick.AddListener(() =>
            {
                int.TryParse(_inputField.text, out int amount);

                if (amount < _maxAmount)
                {
                    int nextAmount = ++amount;

                    _inputField.text = nextAmount.ToString();
                }
            });
            _min.onClick.AddListener(() => _inputField.text = "1");
            _max.onClick.AddListener(() => _inputField.text = _maxAmount.ToString());
            _inputField.onValueChanged.AddListener(str =>
            {
                int.TryParse(str, out int amount);

                if (amount < 1)
                    amount = 1;
                else if (amount > _maxAmount)
                    amount = _maxAmount;

                _inputField.text = amount.ToString();
            });
            _cancel.onClick.AddListener(() => Hide());
            _oK.onClick.AddListener(() =>
            {
                _inventory.RemoveItem(_dropItemIndex, int.Parse(_inputField.text));
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

            if (_inventory.Items[index] is CountableItem countableItem)
            {
                _name.text = countableItem.Data.Name;
                _maxAmount = countableItem.Amount;
            }
            UIManager.Instance.AddListAndShowPopUp(this.gameObject);

            _inputField.Select();
        }
        public void Hide()
        {
            UIManager.Instance.RemoveShowingPopUp(this.gameObject);
        }
    }

}
