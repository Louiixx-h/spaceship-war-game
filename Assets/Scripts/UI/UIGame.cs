using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIGame : MonoBehaviour, IGUI
    {
        [SerializeField] private Image m_LifePlayerImage;
        [SerializeField] private Image m_EnergyPlayerImage;
        [SerializeField] private TextMeshProUGUI m_Nickname;
        [SerializeField] private TextMeshProUGUI m_ScorePlayer;

        public void SetNickName(string nickname)
        {
            m_Nickname.text = nickname;
        }

        public void HealthManager(float value)
        {
            m_LifePlayerImage.fillAmount = value/100;
        }

        public void EnergyManager(float value)
        {
            m_EnergyPlayerImage.fillAmount = value/100;
        }

        internal void UpdateScore(int value)
        {
            int score = Int32.Parse(m_ScorePlayer.text);
            int result = value + score;
            m_ScorePlayer.text = result.ToString();
        }

        public void OnConnected()
        {
            Show();
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