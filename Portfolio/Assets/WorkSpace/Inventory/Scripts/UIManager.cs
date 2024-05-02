using System.Collections.Generic;
using UnityEngine;

namespace Cookie.RPG
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get => _instance; }
        static UIManager _instance;

        List<GameObject> _showingPopUpList = new List<GameObject>();
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
        }
        private void Start()
        {
            AutoCursorVisible();
        }
        private void Update()
        {
            PopUpClose();
        }
        void PopUpClose()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_showingPopUpList.Count != 0)
                    RemoveShowingPopUp(_showingPopUpList[_showingPopUpList.Count - 1]);
            }
        }
        void AutoCursorVisible()
        {
            if (_showingPopUpList.Count == 0)
            {
                SetCursor(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                SetCursor(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
        public void AddListAndShowPopUp(GameObject popUp)
        {
            popUp.SetActive(true);
            _showingPopUpList.Add(popUp);
            AutoCursorVisible();
        }
        public void RemoveShowingPopUp(GameObject popUp)
        {
            popUp.SetActive(false);
            _showingPopUpList.Remove(popUp);
            AutoCursorVisible();
        }
        public void SetCursor(bool isVisible)
        {
            Cursor.visible = isVisible;
        }
    }
}
