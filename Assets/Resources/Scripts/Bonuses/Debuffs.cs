using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{

    public enum DebuffEffectType
    {
        DisableShield,
        DownArmorResistance,
        ThrustersOff
    }


    public class Debuffs : PowerUp
    {
        [SerializeField] private DebuffEffectType m_DebuffType;
        /// <summary>
        /// Длительность дебаффа
        /// </summary>
        [SerializeField] private int m_DebuffDuration;

        [SerializeField] private int m_DebuffAmount;

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
            if (m_DebuffType == DebuffEffectType.DisableShield)
            {
                ship.DisableShields(m_DebuffDuration, m_DebuffAmount);
            }
            if (m_DebuffType == DebuffEffectType.DownArmorResistance)
            {
                ship.DownResistanceArmor(m_DebuffDuration, m_DebuffAmount);
            }
            if (m_DebuffType == DebuffEffectType.ThrustersOff)
            {

            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
