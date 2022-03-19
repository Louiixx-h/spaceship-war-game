using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class InstantiateMenu : MonoBehaviourPun
    {
        [SerializeField] private GameObject m_Menu;
        [SerializeField] private PhotonView m_PhotonView;

        private void Awake()
        {
            if (!m_PhotonView.IsMine) return;

            var menu = PhotonNetwork.Instantiate(
                m_Menu.name,
                transform.position,
                transform.rotation,
                0
            );

            transform.parent = menu.transform;
        }
    }
}