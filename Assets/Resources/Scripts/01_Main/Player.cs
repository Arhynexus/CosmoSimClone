using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_Lives;
        [SerializeField] private GameObject m_ShipPrefab;

        [SerializeField] private MovementController m_MovementController;
        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private PlayerHUD_UI hudUI;
        [SerializeField] private Transform m_SpawnPoint;

        private SpaceShip m_Ship;



        public SpaceShip ActiveShip => m_Ship;

        protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null) Destroy(m_Ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            m_Lives--;

            if (m_Lives > 0) Respawn();

            if (m_Lives == 0)
            {
                m_Ship.EventOnDeath.RemoveAllListeners();
                m_Ship.ChangeHitPoints.RemoveAllListeners();
                LevelSequenceController.Instance.FinishCurrentLevel(false);
                return;
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip, m_SpawnPoint.transform.position, Quaternion.identity);

                m_Ship = newPlayerShip.GetComponent<SpaceShip>();
                m_Ship.EventOnDeath.AddListener(OnShipDeath);

                m_MovementController.SetTargetShip(m_Ship);
                m_CameraController.SetTarget(m_Ship.transform);
                m_Ship.InitOffence();
                m_Ship.SetStartHitPoints();
                hudUI.SetActiveShip();
            }
        }
    }
}

