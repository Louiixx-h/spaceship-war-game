using Assets.Scripts.Data;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : GameManager<UIManager>
    {
        public UIGame uiGame;
        public UIJoystick uiJoystick;
        public UIDamage uiDamage;
        public UIDialog uiDialog;
        public UIRevive uiRevive;
        public UIGameOver uiGameOver;
        public UIMenu uiMenu;
        public UIRoomList uiRoomList;
        public UILoading uiLoading;
        public UILogin uiLogin;

        [SerializeField] private GameObject m_backgroundImage;
        [SerializeField] private GameObject m_GameOver;
        [SerializeField] private PhotonView m_PhotonView;

        private void Start()
        {
            NetworkController.Instance.OnJoinedRoomEvent += OnLoginSucessful;   
        }

        public void HideAll()
        {
            uiGame.Hide();
            uiJoystick.Hide();
            uiDialog.Hide();
            uiRevive.Hide();
            uiMenu.Hide();
            uiRoomList.Hide();
            uiLoading.Hide();
            uiLogin.Hide();
            if(uiGameOver != null) uiGameOver.Hide();
        }

        void OnLoginSucessful()
        {
            if (!m_PhotonView.IsMine && uiGameOver != null) return;

            var pos = transform.position;
            var rot = transform.rotation;
            var panelGameOver = PhotonNetwork.Instantiate(
                m_GameOver.name,
                pos,
                rot,
                0
            );

            m_PhotonView.RPC("AttachGameOverRPC", RpcTarget.AllBuffered, panelGameOver.name);
        }

        [PunRPC]
        void AttachGameOverRPC(string name)
        {
            GameObject panelGameOver = GameObject.Find(name);
            panelGameOver.SetActive(false);
            panelGameOver.transform.parent = gameObject.transform;
            uiGameOver = panelGameOver.GetComponent<UIGameOver>();
        }

        public void ShowBackgroundImage()
        {
            m_backgroundImage.SetActive(true);
        }

        public void HideBackgroundImage()
        {
            m_backgroundImage.SetActive(false);
        }
    }
}