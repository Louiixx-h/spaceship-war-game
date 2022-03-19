using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIDialog : MonoBehaviour, IGUI
    {
        [SerializeField] private TextMeshProUGUI m_Message;
        [SerializeField] private Button m_Confirm;

        private void Start()
        {
            m_Confirm.onClick.AddListener(() =>
            {
                Hide();
            });
        }

        public UIDialog SetMessage(string message)
        {
            m_Message.text = message;
            return this;
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