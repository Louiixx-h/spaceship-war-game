using Assets.Scripts.Data;
using Newtonsoft.Json;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ListViewStatusGame : MonoBehaviourPun
    {
        [SerializeField] private GameObject m_StatusItem;
        [SerializeField] private PhotonView m_PhotonView;

        public void SetListView(List<DataPlayer> players)
        {
            string json = JsonConvert.SerializeObject(players);
            m_PhotonView.RPC("InstantiateItemsRPC", RpcTarget.AllBuffered, json);
        }

        [PunRPC]
        void InstantiateItemsRPC(string playersJson)
        {
            var players = JsonConvert.DeserializeObject<List<DataPlayer>>(playersJson);
            var playerList = players;
            foreach (var player in playerList)
            {
                GameObject cardObj = PhotonNetwork.Instantiate(
                    m_StatusItem.name, 
                    transform.position, 
                    transform.rotation, 
                    0
                );
                cardObj.transform.parent = gameObject.transform;
                cardObj.GetComponent<StatusGameItem>().SetInfo(player);
            }
        }
    }
}