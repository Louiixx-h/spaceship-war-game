using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UILoading : MonoBehaviour, IGUI
    {
        [SerializeField] private Image m_Image;
        float m_Amount = 0;
        bool m_IsClockwise = true;

        void Start()
        {
            m_Image.fillClockwise = m_IsClockwise;
        }

        void Update()
        {
            if (m_Amount <= 0f)
            {
                m_Image.fillClockwise = true;
                m_IsClockwise = true;
            }
            else if (m_Amount >= 2f)
            {
                m_Image.fillClockwise = false;
                m_IsClockwise = false;
            }

            if (m_IsClockwise)
            {
                m_Amount += Time.deltaTime;
            }
            else
            {
                m_Amount -= Time.deltaTime;
            }
            m_Image.fillAmount = m_Amount / 2;
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