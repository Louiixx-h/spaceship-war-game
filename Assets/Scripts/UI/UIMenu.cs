using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIMenu : MonoBehaviour, IGUI
    {
        [SerializeField] private TMP_InputField m_RoomName;
        [SerializeField] private Button m_CreateRoomButton;
        [SerializeField] private Button m_FindRandomGameButton;
        [SerializeField] private Button m_ListRoomButton;
        [SerializeField] private Button m_ExitGameButton;

        private void Start()
        {
            m_ListRoomButton.onClick.AddListener(() =>
            {
                ListRoom();
            });
            m_CreateRoomButton.onClick.AddListener(() =>
            {
                CreateRoom();
            });
            m_FindRandomGameButton.onClick.AddListener(() =>
            {
                FindRandomRoom();
            });
            m_ExitGameButton.onClick.AddListener(() =>
            {
                ExitGame();
            });
        }

        public void CreateRoom()
        {
            string roomName = m_RoomName.text;
            if(roomName.Equals(""))
            {
                UIManager.Instance
                    .uiDialog
                    .SetMessage("O nome da sala está vazio!")
                    .Show();
                return;
            }
            UIManager.Instance.uiMenu.Hide();
            UIManager.Instance.uiLoading.Show();
            NetworkController.Instance.CreateRoom(roomName);
        }

        public void FindRandomRoom()
        {
            UIManager.Instance.uiMenu.Hide();
            UIManager.Instance.uiLoading.Show();
            NetworkController.Instance.FindRandomRoom();
        }

        public void ListRoom()
        {
            UIManager.Instance.uiMenu.Hide();
            UIManager.Instance.uiRoomList.Show();
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}