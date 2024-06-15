using UnityEngine;

namespace Cookie.RPG
{
    public class EquipmentUI : MonoBehaviour
    {
        [SerializeField] Equipment _equipment;

        Transform _window;

        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void Init()
        {
            _window = transform.GetChild(0);
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
