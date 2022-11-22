using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class PowerUpWeapon : PowerUp
    {
        [SerializeField] private TurretProperties m_TurretPropperties;
        
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(m_TurretPropperties);
        }
    }
}


