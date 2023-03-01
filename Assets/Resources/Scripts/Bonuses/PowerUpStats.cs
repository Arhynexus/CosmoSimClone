using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class PowerUpStats : PowerUp
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            AddHealth,
            AddShield,
            AddArmor
        }

        [SerializeField] private EffectType m_EffectType;

        [SerializeField] private float m_Value;
        
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int)m_Value);
            }
            if (m_EffectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int)m_Value);
            }
            if (m_EffectType == EffectType.AddShield)
            {
                ship.RestoreShield((int)m_Value);
            }
            if (m_EffectType == EffectType.AddArmor)
            {
                ship.RestoreArmor((int)m_Value);
            }
            if (m_EffectType == EffectType.AddHealth)
            {
                ship.RestoreHealth((int)m_Value);
            }
        }


    }
}


