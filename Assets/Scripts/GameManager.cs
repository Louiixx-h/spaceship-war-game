using Assets.Scripts.Gameplay;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager<T> : MonoBehaviourPun where T : MonoBehaviourPun
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)FindObjectOfType(typeof(T));
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}