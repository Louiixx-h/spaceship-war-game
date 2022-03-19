using Assets.Scripts.Data;
using Assets.Scripts.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UILogin : MonoBehaviour, IGUI
    {
        [SerializeField] private Button m_LoginButton;
        [SerializeField] private TMP_InputField m_PlayerNickNameInput;

        private void Start()
        {
            m_LoginButton.onClick.AddListener(() =>
            {
                DoLogin();
            });
        }

        public void DoLogin()
        {
            if (m_PlayerNickNameInput.text == "")
            {
                UIManager.Instance
                    .uiDialog
                    .SetMessage("O campo de nick name está vazio!")
                    .Show();
                return;
            }
            UIManager.Instance.uiLogin.Hide();
            UIManager.Instance.uiLoading.Show();
            string nickname = m_PlayerNickNameInput.text;
            NetworkController.Instance.ConnectToServerWithNickName(nickname);
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