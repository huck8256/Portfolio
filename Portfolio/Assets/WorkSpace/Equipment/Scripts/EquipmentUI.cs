using UnityEngine;
using UnityEngine.UI;

namespace Cookie.RPG
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] Equipment _equipment;

        Transform _window;
        Transform _headerArea;
        Transform _contentArea;

        Button _closeButton;

        void Start()
        {
            Init();
            AddListener();
        }

        private void Init()
        {
            _window = transform.GetChild(0);
            _headerArea = _window.GetChild(0);
            _contentArea = _window.GetChild(1);

            _closeButton = _headerArea.GetChild(1).GetComponent<Button>();
        }
        void AddListener()
        {
            // exit 버튼 설정
            _closeButton.onClick.AddListener(() => UIManager.Instance.RemoveShowingPopUp(_window.gameObject));
        }
        public void Window()
        {
            if (_window.gameObject.activeSelf)
                UIManager.Instance.RemoveShowingPopUp(_window.gameObject);
            else
                UIManager.Instance.AddListAndShowPopUp(_window.gameObject);
        }
    }
}
