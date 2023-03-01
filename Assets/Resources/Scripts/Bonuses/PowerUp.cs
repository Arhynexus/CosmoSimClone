using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class PowerUp : Entity
    {
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.GetComponentInParent<SpaceShip>();
            BonusesGrabber grabber = collision.GetComponentInParent<BonusesGrabber>();

            if (grabber != null) return;

            if (ship != null && ship == Player.Instance.ActiveShip)
            {
                OnPickedUp(ship);

                Destroy(gameObject);
            }
        }
        protected abstract void OnPickedUp(SpaceShip ship);

    }
}


