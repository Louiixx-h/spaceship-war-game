using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay
{
    public class StationController : MonoBehaviourPun, IDamageable
    {
        [SerializeField] private PhotonView m_PhotonView;

        [Space(8)]

        [Header("UI")]
        [SerializeField] private Image m_ImageLife;
        [SerializeField] private GameObject m_SpawnTurret;

        [Header("TURRET")]
        [SerializeField] private GameObject m_Turret;

        float m_CurrentLife = 0;
        float m_MaxLife = 200;
        Transform[] m_Spawns;

        private void Start()
        {
            SetTurrets();
            m_PhotonView.RPC("HealthManager", RpcTarget.AllViaServer, m_MaxLife);
        }

        [PunRPC]
        public void HealthManager(float value)
        {
            if (!m_PhotonView.IsMine) return;
            m_CurrentLife += value;
            m_ImageLife.fillAmount = m_CurrentLife / m_MaxLife;
            if (m_CurrentLife <= 0) m_PhotonView.RPC("Death", RpcTarget.AllViaServer);
        }

        [PunRPC]
        void Death()
        {
            if (!m_PhotonView.IsMine) return;
            Destroy(gameObject);
        }

        public void TakeDamage(float value)
        {
            if (!m_PhotonView.IsMine) return;
            m_PhotonView.RPC("HealthManager", RpcTarget.AllViaServer, value);
        }

        public void SetTurrets()
        {
            m_Spawns = m_SpawnTurret.GetComponentsInChildren<Transform>();
            foreach (var spawn in m_Spawns) {
                Instatiate(m_Turret, spawn);
            }
        }

        public void Instatiate(GameObject gameObject, Transform transform)
        {
            if (!m_PhotonView.IsMine) return;
            var obj = PhotonNetwork.Instantiate(
                gameObject.name,
                transform.position,
                transform.rotation,
                0
            );
            gameObject.transform.parent = obj.transform;
        }
    }
}
