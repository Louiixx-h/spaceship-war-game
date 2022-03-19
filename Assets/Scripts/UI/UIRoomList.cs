using Assets.Scripts.UI;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIRoomList : MonoBehaviour, IGUI
    {
        [SerializeField] private ListView m_ListViewUI;
        [SerializeField] private Button m_ExitListRoomButton;

        void Start()
        {
            m_ExitListRoomButton.onClick.AddListener(() =>
            {
                ExitRoomList();
            });
        }

        public void ExitRoomList()
        {
            UIManager.Instance.uiMenu.Show();
            UIManager.Instance.uiRoomList.Hide();
        }

        public void SetListRoom(List<RoomInfo> roomList)
        {
            m_ListViewUI.SetListView(roomList);
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