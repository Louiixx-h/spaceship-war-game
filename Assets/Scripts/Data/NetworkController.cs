using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Assets.Scripts.UI;

namespace Assets.Scripts.Data
{
    public class NetworkController : MonoBehaviourPunCallbacks 
    {
        public static NetworkController Instance { get; private set; }

        [Header("PLAYER")] 
        [SerializeField] private GameObject _player;

        [Header("MAP")]
        [SerializeField] private GameObject m_Map;
        [SerializeField] private Transform m_Origin;

        public delegate void OnJoinedRoomDelegate();
        public event OnJoinedRoomDelegate OnJoinedRoomEvent;

        byte      m_GameMaxPlayer = 6;
        string    m_GameModeKey   = "gameModeKey";
        string    m_RoomMode      = "PvP";
        Hashtable m_GameMode      = new Hashtable();


        private void Awake()
        {
            if(Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            UIManager.Instance.HideAll();
            UIManager.Instance.uiLogin.Show();
            UIManager.Instance.ShowBackgroundImage();
        }

        public void CreateRoom(string roomName)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.MaxPlayers = m_GameMaxPlayer;
            roomOptions.CustomRoomProperties = m_GameMode;
            roomOptions.CustomRoomPropertiesForLobby = new string[] { m_GameModeKey };
            
            PhotonNetwork.CreateRoom(
                roomName,
                roomOptions,
                TypedLobby.Default
            );
        }

        public void FindRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom(m_GameMode, m_GameMaxPlayer);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
            PhotonNetwork.LeaveRoom();
        }

        public void EnterRandomRoomWithName(string roomName)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

        public void ConnectToServerWithNickName(string nickname)
        {
            PhotonNetwork.NickName = nickname;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster() 
        {
            UIManager.Instance.HideAll();
            UIManager.Instance.uiLoading.Show();
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby() 
        {
            UIManager.Instance.HideAll();
            UIManager.Instance.uiMenu.Show();
            m_GameMode.Add(m_GameModeKey, m_RoomMode);
        }

        public override void OnJoinRandomFailed(
            short returnCode, 
            string message
        ) {            
            int id = Random.Range(100, 1000) + Random.Range(100, 1000);
            string roomName = "room" + id;
            CreateRoom(roomName);
        }

        public override void OnJoinedRoom() {
            
            OnJoinedRoomEvent.Invoke();

            foreach (Player player in PhotonNetwork.PlayerList) {
                Hashtable playerCustom = new Hashtable();
                playerCustom.Add("Lives", 3);
                playerCustom.Add("Score", 0);

                player.SetCustomProperties(playerCustom);
            }

            UIManager.Instance.HideAll();
            UIManager.Instance.HideBackgroundImage();
            UIManager.Instance.uiGame.Show();

            PhotonNetwork.Instantiate(
                m_Map.name,
                m_Origin.position,
                m_Origin.rotation,
                0
            );

            PhotonNetwork.Instantiate(
                _player.name, 
                _player.transform.position, 
                _player.transform.rotation, 
                0
            );
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            UIManager.Instance.uiRoomList.SetListRoom(roomList);
        }
    }
}