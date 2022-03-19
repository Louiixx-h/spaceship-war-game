using Assets.Scripts.Data;
using Assets.Scripts.Room;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIGameOver : MonoBehaviourPun, IGUI
    {
        [SerializeField] private Button m_ExitButton;
        [SerializeField] private ListViewStatusGame m_ListViewStatusGame;
        [SerializeField] private PhotonView m_PhotonView;

        private void Start()
        {
            m_ExitButton.onClick.AddListener(() =>
            {
                Game.Instance.endGame
                    .DeletePlayers()
                    .DeleteRoom();
                Hide();
                UIManager.Instance.uiMenu.Show();
            });
        }

        public void Show()
        {
            m_PhotonView.RPC("ShowRPC", RpcTarget.AllBuffered);
            m_PhotonView.RPC("SetListRPC", RpcTarget.AllBuffered);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        [PunRPC]
        void ShowRPC()
        {
            gameObject.SetActive(true);
        }
        
        [PunRPC]
        void SetListRPC()
        {
            if (!m_PhotonView.IsMine) return;
            List<DataPlayer> players = new List<DataPlayer>(); 
            var playersDictionary = PhotonNetwork.CurrentRoom.Players;

            foreach(KeyValuePair<int, Player> player in playersDictionary)
            {
                var name = player.Value.NickName;
                var score = ScoreController.Instance.GetScore(player.Value);
                players.Add(new DataPlayer(name, score));
            }
            m_ListViewStatusGame.SetListView(players);
        }
    }
}