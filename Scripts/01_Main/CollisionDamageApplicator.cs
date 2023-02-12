using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    /// <summary>
    /// Наносит урон кораблю при столкновении с космическими объектами(планеты, астероиды и т.д.)
    /// </summary>
    public class CollisionDamageApplicator : MonoBehaviour
    {
        public static string IgnoreTag = "WorldBoundary";
        /// <summary>
        /// Множитель урона от скорости полета корабля
        /// </summary>
        [SerializeField] private float m_VelocityDamageModifier;
        /// <summary>
        /// Урон, получаемый при столкновении с объектом
        /// </summary>
        [SerializeField] private float m_DamageConstant;

        private void Start()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            
            if (collision.transform.tag == IgnoreTag) return;



            var destructible = transform.GetComponentInParent<Destructible>();

            if (destructible != null)
            {
                
                if (m_VelocityDamageModifier <= 0) m_VelocityDamageModifier = 10;
                destructible.ApplyDamage((int)m_DamageConstant * 
                                         (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                destructible.ChangeHitPoints.Invoke();
            }
        }
    }
}


