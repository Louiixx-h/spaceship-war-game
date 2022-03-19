using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform m_OriginCircle;
    [SerializeField] private LayerMask m_LayerPlayer;

    [Header("Bullet")]
    [SerializeField] private GameObject m_Bullet;
    [SerializeField] private GameObject m_SpawnBullet;

    float m_Radiu = 8f;
    float m_Time = 0.2f;
    bool canShoot = true;

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
            m_OriginCircle.position,
            m_Radiu,
            m_LayerPlayer
        );
        if(colliders.Length > 0)
        {
            Vector3 pos = colliders[0].gameObject.transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector2.down, transform.position - pos);
            if(canShoot) Shooting();
        }

        if(m_Time <= 0)
        {
            canShoot = true;
        }
        else
        {
            m_Time -= Time.deltaTime;
        }
    }

    void Shooting()
    {
        Instantiate(
            m_Bullet,
            m_SpawnBullet.transform.position,
            m_SpawnBullet.transform.rotation
        );
        canShoot = false;
        m_Time = 0.2f;
    }
}
