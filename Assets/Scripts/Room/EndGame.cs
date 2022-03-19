using Assets.Scripts.Data;
using Assets.Scripts.Gameplay;
using Photon.Pun;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private PhotonView m_PhotonView;

    public EndGame DeletePlayers()
    {
        m_PhotonView.RPC("DeletePlayersRPC", RpcTarget.AllBuffered);
        return this;
    }

    [PunRPC]
    void DeletePlayersRPC()
    {
        var players = FindObjectsOfType<PlayerController>();
        if (players == null) return;
        foreach (PlayerController p in players)
        {
            Destroy(p.gameObject);
        }
    }

    public EndGame DeleteRoom()
    {
        m_PhotonView.RPC("DeleteRoomRPC", RpcTarget.AllBuffered);
        return this;
    }

    [PunRPC]
    void DeleteRoomRPC()
    {
        var maps = FindObjectsOfType<MapService>();
        if (maps == null) return;
        foreach (MapService m in maps)
        {
            Destroy(m.gameObject);
        }
        NetworkController.Instance.LeaveRoom();
    }
}
