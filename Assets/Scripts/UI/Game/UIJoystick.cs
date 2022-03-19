using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIJoystick : MonoBehaviour, IGUI
    {
        [Header("CONTROLLER")]
        public FixedJoystick m_Joystick;

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