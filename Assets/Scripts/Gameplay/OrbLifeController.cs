using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class OrbLifeController : MonoBehaviour
    {
        float m_Life = 20;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 8)
            {
                var player = collision.gameObject.GetComponent<PlayerController>();
                if (player == null) return;
                player.TakeLife(m_Life);
                Destroy(gameObject);
            }
        }
    }
}