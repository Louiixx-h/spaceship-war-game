using Assets.Scripts.Data;
using Assets.Scripts.UI;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D m_Rb;
        [SerializeField] private GameObject m_HitEffect;
        [SerializeField] private GameObject m_ShootEffect;
        [SerializeField] private PhotonView m_PhotonView;

        public IHittedTarget m_HittedTarget;

        float m_BulletSpeed  = 100f;
        float m_TimeLife     = 1.3f;
        float m_BulletDamage = -10;

        private void Start()
        {
            Instantiate(m_ShootEffect, transform.position - new Vector3(0, 0, 5), Quaternion.identity);
            Destroy(gameObject, m_TimeLife);
        }

        void Update()
        {
            m_Rb.AddForce(transform.up * m_BulletSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!m_PhotonView.IsMine) return;
            Instantiate(m_HitEffect, transform.position, Quaternion.identity);
            DoDamage(collision);
            m_PhotonView.RPC("Hitted", RpcTarget.AllViaServer);
        }

        private void ShowTextDamage(float value)
        {
            UIManager.Instance
                .uiDamage
                .ShowTextDamage(
                    transform.position, 
                    value
                );
        }

        void DoDamage(Collider2D collision)
        {
            var damage = m_BulletDamage;
            var gameObjectPlayer = collision.gameObject;
            var damageable = gameObjectPlayer.GetComponent<IDamageable>();
            if (damageable == null) return;
            damageable.TakeDamage(damage);
            ShowTextDamage(damage);
            ScoreController.Instance.UpdateScore(10, m_PhotonView.Owner);
        }

        [PunRPC]
        void Hitted()
        {
            Destroy(gameObject);
        }
    }
}