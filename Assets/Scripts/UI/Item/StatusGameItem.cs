using Assets.Scripts.Data;
using Newtonsoft.Json;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class StatusGameItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_Nickname;
        [SerializeField] private TextMeshProUGUI m_Score;
        [SerializeField] private PhotonView m_PhotonView;

        public void SetInfo(DataPlayer player)
        {
            string json = JsonConvert.SerializeObject(player);
            m_PhotonView.RPC("SetInfoRPC", RpcTarget.All, json);
        }

        [PunRPC]
        public void SetInfoRPC(string playerJson)
        {
            var player = JsonConvert.DeserializeObject<DataPlayer>(playerJson);
            m_Nickname.text = player.nickname;
            m_Score.text = "Score - " + player.score;
        }
    }
}