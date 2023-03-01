using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{



    public class Buffs : PowerUp
    {
        public enum BuffsType
        {
            IncreaseVelocity,
            IncreaseAcceleration,
            Unvulnerable,
            ShieldRegeneration,
            ArmorRegeneration,
            HealthRegeneration,
            EnergyRegeneration
        }

       [SerializeField] private BuffsType m_type;
       [SerializeField] private int m_Duration;
       [SerializeField] private int amount;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.GetComponentInParent<SpaceShip>();
            BonusesGrabber grabber = collision.GetComponentInParent<BonusesGrabber>();
            Target target = collision.GetComponentInParent<Target>();

            if (target != null) return;

            if (grabber != null) return;

            if (ship != null && Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);
            }
        }

        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_type == BuffsType.IncreaseVelocity)
            {
                ship.AddMaxLinearVelocity(m_Duration, amount);
            }
            if (m_type == BuffsType.IncreaseAcceleration)
            {
                ship.AddThrust(m_Duration, amount);
            }
            if (m_type == BuffsType.Unvulnerable)
            {
                ship.UnvulnerableIsActive(m_Duration);
            }
            if (m_type == BuffsType.ShieldRegeneration)
            {
                
            }
            if (m_type == BuffsType.ArmorRegeneration)
            {
                
            }
            if (m_type == BuffsType.HealthRegeneration)
            {

            }
            if (m_type == BuffsType.EnergyRegeneration)
            {

            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}