using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UITextDamage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_TextDamage;

        float m_InitialTextSize = 1.3f;
        float m_TweenTime = 0.7f;
        float m_LifeTime = 1f;

        public void SetDamage(float value)
        {
            LeanTween.cancel(m_TextDamage.gameObject);

            m_TextDamage.text = value.ToString();
            m_TextDamage.gameObject.transform.localScale = Vector2.one * m_InitialTextSize;

            LeanTween.scale(m_TextDamage.gameObject, Vector2.one, m_TweenTime);

            Destroy(gameObject, m_LifeTime);
        }
    }
}