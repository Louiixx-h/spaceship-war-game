using Assets.Scripts.Data;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class RoomItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_RoomName;
        [SerializeField] private TextMeshProUGUI m_RoomPlayersCount;

        public void SetRoomInfo(RoomInfo room)
        {
            m_RoomName.text = room.Name;
            m_RoomPlayersCount.text = room.PlayerCount + "/" + room.MaxPlayers;
            GetComponent<Button>().onClick.AddListener(() =>
            {
                NetworkController.Instance.EnterRandomRoomWithName(room.Name);
            });
        }
    }
}