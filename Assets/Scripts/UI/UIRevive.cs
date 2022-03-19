using Assets.Scripts;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIRevive : MonoBehaviour, IGUI
    {
        [SerializeField] private TextMeshProUGUI m_TextReviveCount;

        public delegate void CountFinishedDelegate();
        public event CountFinishedDelegate OnCountFinished;

        void OnEnable()
        {
            StartCoroutine(CountNumber());
        }

        IEnumerator CountNumber()
        {
            int time = 5;

            do
            {
                m_TextReviveCount.text = time.ToString();
                time--;
                yield return new WaitForSeconds(1);
            } while (time >= 0);
            OnCountFinished.Invoke();
            yield return null;
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