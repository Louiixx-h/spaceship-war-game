using Assets.Scripts.Data;
using Assets.Scripts.UI;
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class PlayerController : MonoBehaviourPun, IDamageable, IHittedTarget
    {
        [SerializeField] private Rigidbody2D m_Rb;
        [SerializeField] private PhotonView m_PhotonView;
        [SerializeField] private bool m_IsMobile;

        [Header("BULLET")]
        [SerializeField] private GameObject m_Bullet;
        [SerializeField] private GameObject m_SpawnBullet;

        [Header("ULTIMATE")]
        [SerializeField] private GameObject m_Orb;
        [SerializeField] private GameObject m_Power;

        [Header("CONTROLLER")]
        private FixedJoystick m_Joystick;

        int   m_NumberLifePlayer = 1;
        float m_MaxLife          = 100;
        float m_CurrentLife      = 0f;
        
        float m_CurrentEnergy    = 0f;
        float m_MaxEnergy        = 100f;
        float m_EnergyDash       = -10f;
        float m_EnergyUltimate   = -80f;

        float m_MoveSpeed        = 400f;
        float m_DashSpeed        = 100f;
        
        bool  m_IsDeath          = false;
        bool  m_IsUltimate = false;

        float m_TimeForUltimateGrow = 1f; 
        float m_TimeUltimateLife    = 0.7f;
        IEnumerator m_UltimateCoroutine;

        private void Start()
        {
            if (!m_PhotonView.IsMine) return;
            m_Joystick = UIManager.Instance.uiJoystick.m_Joystick;
            m_PhotonView.RPC("SetNickName", RpcTarget.All);
            m_PhotonView.RPC("DisableUltimate", RpcTarget.All);
            m_PhotonView.RPC("EnergyManager", RpcTarget.All, m_MaxLife);
            m_PhotonView.RPC("HealthManager", RpcTarget.All, m_MaxEnergy);
        }

        void Update()
        {
            if (!m_PhotonView.IsMine && m_IsDeath) return;
            Shooting();
            HandleDash();
            MovementPlayer();
            HandleSuperShot();
            if (!m_IsUltimate)
            {
                RotataionPlayer();
            }
        }

        [PunRPC]
        public void SetNickName()
        {
            if (!m_PhotonView.IsMine) return;
            string nickname = m_PhotonView.Owner.NickName;
            UIManager.Instance.uiGame.SetNickName(nickname);
        }

        public void TakeDamage(float value)
        {
            m_PhotonView.RPC("HealthManager", RpcTarget.All, value);
        }

        public void TakeLife(float value)
        {
            m_PhotonView.RPC("HealthManager", RpcTarget.All, value);
        }

        [PunRPC]
        void HealthManager(float value)
        {
            if (!m_PhotonView.IsMine) return;
            m_CurrentLife += value;
            UIManager.Instance.uiGame.HealthManager(m_CurrentLife);
            if (m_CurrentLife <= 0)
            {
                GameOver();
            }
        }

        public void SetEnergy(float value)
        {
            m_PhotonView.RPC("EnergyManager", RpcTarget.AllViaServer, value);
        }

        [PunRPC]
        void EnergyManager(float value)
        {
            if (!m_PhotonView.IsMine) return;
            m_CurrentEnergy += value;
            UIManager.Instance.uiGame.EnergyManager(m_CurrentEnergy);
        }

        [PunRPC]
        void EnableUltimate()
        {
            if (!m_PhotonView.IsMine) return;
            m_Power.SetActive(true);
            m_Orb.SetActive(true);
        }

        [PunRPC]
        void DisableUltimate()
        {
            if (!m_PhotonView.IsMine) return;
            m_Power.SetActive(false);
            m_Orb.SetActive(false);
        }

        public void GameOver()
        {
            m_PhotonView.RPC("GameOverRPC", RpcTarget.AllBuffered);
        }

        [PunRPC]
        void GameOverRPC()
        {
            
            m_IsDeath = true;
            gameObject.SetActive(false);
            
            if (m_PhotonView.IsMine) m_NumberLifePlayer--;
            
            if (m_NumberLifePlayer > 0)
            {
                if (!m_PhotonView.IsMine) return;
                UIManager.Instance.uiRevive.Show();
                UIManager.Instance.uiRevive.OnCountFinished += Revive;
            }
            else
            {
                Destroy(gameObject);
                UIManager.Instance.uiGameOver.Show();
            }
        }

        void Revive()
        {
            m_PhotonView.RPC("ReviveRPC", RpcTarget.AllBuffered);
        }

        [PunRPC]
        public void ReviveRPC()
        {
            m_Orb.SetActive(false);
            m_Power.SetActive(false);
            gameObject.SetActive(true);

            if (!m_PhotonView.IsMine) return;
            m_IsDeath       = false;
            m_CurrentEnergy = 0;
            m_CurrentLife   = 0;
            
            TakeLife(m_MaxLife);
            SetEnergy(m_MaxEnergy);
            UIManager.Instance.uiRevive.Hide();
        }

        void Shooting()
        {
            if (!m_PhotonView.IsMine) return;
            if (Input.GetMouseButtonDown(0))
            {
                PhotonNetwork.Instantiate(
                    m_Bullet.name,
                    m_SpawnBullet.transform.position,
                    m_SpawnBullet.transform.rotation,
                    0
                );
            }
        }

        void HandleSuperShot()
        {
            if (m_CurrentEnergy < (m_EnergyUltimate * -1)) return;
            if (Input.GetMouseButton(1))
            {
                m_MoveSpeed *= 0.01f;
                LoadEnergy();
            }
            if (Input.GetMouseButtonUp(1))
            {
                SetEnergy(m_EnergyUltimate);
                FireSuperShot();
            }
        }

        void LoadEnergy()
        {
            m_Orb.SetActive(true);
        }

        void FireSuperShot()
        {
            m_Power.SetActive(true);
            m_UltimateCoroutine = UltimateLife();
            StartCoroutine(m_UltimateCoroutine);
        }

        IEnumerator UltimateLife()
        {
            var targetScale = new Vector3(3, 30, 0);
            var time = m_TimeForUltimateGrow;

            while (time > 0) {
                m_Power.transform.localScale = Vector3.Lerp(
                    m_Power.transform.localScale,
                    targetScale,
                    m_TimeForUltimateGrow
                );
                time -= Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(m_TimeUltimateLife);

            m_Power.SetActive(false);
            m_Orb.SetActive(false);
            m_Power.transform.localScale = new Vector3(3, 0, 0);
            m_MoveSpeed = 400f;

            yield return null;
        }

        void MovementPlayer()
        {
            float x = 0;
            float y = 0;
            if (m_IsMobile)
            {
                x = m_Joystick.Horizontal;
                y = m_Joystick.Vertical;
            }
            else
            {
                x = Input.GetAxisRaw("Horizontal");
                y = Input.GetAxisRaw("Vertical");
            }

            m_Rb.velocity = new Vector2(x, y) * m_MoveSpeed * Time.deltaTime;
        }

        void HandleDash()
        {
            if (Input.GetKeyDown(KeyCode.E) && m_CurrentEnergy > 0)
            {
                SetEnergy(m_EnergyDash);
                m_Rb.AddRelativeForce(Vector2.up * m_DashSpeed, ForceMode2D.Force);
            }
        }

        void RotataionPlayer()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x, 
                mousePosition.y - transform.position.y
            );
            transform.up = direction;
        }

        public void Invoke()
        {
            ScoreController.Instance
                .UpdateScore(
                    10, 
                    m_PhotonView.Owner
                );
        }
    }
}