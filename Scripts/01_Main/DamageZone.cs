using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{



    public class DamageZone : MonoBehaviour
    {

        [SerializeField] private int damage;
        [SerializeField] private float DamageRate;
        private new AudioSource audio;

        private Destructible destructible;

        private float Timer = 0.1f;

        private void Start()
        {
            Timer = DamageRate;
            DamageRate = 0;
            audio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            //if (destructible == null) return;
            //DamageRate -= Time.deltaTime;
            //if (destructible != null)
            //{
            //    if (DamageRate <= 0)
            //    {
            //        destructible.ApplyDamage(damage);
            //
            //        if (audio != null)
            //        {
            //            audio.Play();
            //        }
            //        DamageRate = Timer;
            //    }
            //}

            Timer -= Time.deltaTime;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            BonusesGrabber bonuses = collision.GetComponentInParent<BonusesGrabber>();
            if (bonuses != null) return;
            Destructible destructible = collision.GetComponentInParent<Destructible>();
            Target target = collision.GetComponentInParent<Target>();
            if (target != null) return;
            if (destructible != null)
            {
                destructible.ApplyDamage(damage);
            }
           
        }
      

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Destructible>() == destructible)
                destructible = null;
        }
    }
}
