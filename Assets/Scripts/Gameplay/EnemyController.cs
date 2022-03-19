using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Gameplay
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("UI")]
        [SerializeField] private Image m_ImageLife;

        float m_CurrentLife = 0;
        float m_MaxLife = 200;

        private void Start()
        {
            HealthManager(m_MaxLife);
        }

        public void HealthManager(float value)
        {
            m_CurrentLife += value;
            m_ImageLife.fillAmount = m_CurrentLife / m_MaxLife;
            if (m_CurrentLife <= 0) Death();
        }

        void Death()
        {
            Destroy(gameObject);
        }

        public void TakeDamage(float value)
        {
            HealthManager(value);
        }
    }
}
