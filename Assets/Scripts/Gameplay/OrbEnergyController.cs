using Assets.Scripts.Gameplay;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class OrbEnergyController : MonoBehaviour
    {
        float m_Energy = 20;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 8)
            {
                var player = collision.gameObject.GetComponent<PlayerController>();
                if (player == null) return;
                player.SetEnergy(m_Energy);
                Destroy(gameObject);
            }
        }
    }
}