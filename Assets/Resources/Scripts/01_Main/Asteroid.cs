using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{



    public class Asteroid : Destructible
    {
        [SerializeField] private ImpactEffect[] m_DeathImpactEffects;
        
        protected override void Start()
        {
            base.Start();
        }

        private void VisualEffectsOnDead()
        {
            if (m_DeathImpactEffects != null)
            {
                for (int i = 0; i < m_DeathImpactEffects.Length; i++)
                {
                    Instantiate(m_DeathImpactEffects[i], transform.position, Quaternion.identity);
                }
            }
        }

        protected override void OnDeath()
        {
            VisualEffectsOnDead();
            base.OnDeath();
        }
        void Update()
        {

        }
    }
}
