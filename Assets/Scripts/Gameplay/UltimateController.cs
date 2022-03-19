using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class UltimateController : MonoBehaviour
    {
        [SerializeField] private Transform m_Point;
        float m_Damage = -20;
        float m_Time   = 0.4f;
        float m_Reset = 0;

        void Update()
        {   
            m_Reset -= Time.deltaTime;
            if (m_Reset > 0) return;
            DoDamage();
        }

        void DoDamage()
        {
            var point = m_Point.position;
            var size = transform.localScale;
            var angle = transform.rotation.z;
            var colliders = Physics2D.OverlapBoxAll(point, size, angle);
            
            foreach (var coll in colliders)
            {
                var obj = coll.gameObject;
                var damageable = obj.GetComponent<IDamageable>();
                
                if (damageable == null) return;
                
                damageable.TakeDamage(m_Damage);
                m_Reset = m_Time;
            }
        }

        private void ShowTextDamage(Vector2 point, float value)
        {
            UIManager.Instance
                .uiDamage
                .ShowTextDamage(
                    point,
                    value
                );
        }
    }
}