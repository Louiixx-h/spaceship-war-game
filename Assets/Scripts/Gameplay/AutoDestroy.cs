using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float m_Time;

    private void Start()
    {
        Destroy(gameObject, m_Time);
    }
}
