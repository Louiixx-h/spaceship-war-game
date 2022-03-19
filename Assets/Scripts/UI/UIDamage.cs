using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIDamage : MonoBehaviour
    {
        [SerializeField] private GameObject m_TextDamage;

        public void ShowTextDamage(Vector2 position, float value)
        {
            var obj = Instantiate(m_TextDamage, position, Quaternion.identity);
            var uiTextDamage = obj.GetComponent<UITextDamage>();
            if (uiTextDamage == null) return;
            uiTextDamage.SetDamage(value);
        }
    }
}