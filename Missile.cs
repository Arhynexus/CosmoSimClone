using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Missile : Projectile
    {
        [SerializeField] private float m_Radius;

        private Transform m_Target;
        private float m_RotateSpeed = 200f;
        private Rigidbody2D rb;
        
        protected override void Start()
        {
            base.Start();
            rb = GetComponent<Rigidbody2D>();
        }
        protected override void Update()
        {
            base.Update();
            if (m_Target != null) SearchAndDestroy();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (m_Target != null) return;
            if (m_Target == null)
            {
                Enemy enemy = collision.transform.root.GetComponent<Enemy>();

                if (enemy != null)
                {
                    SetTarget(enemy.transform);
                }
            }
             
        }

        private void SearchAndDestroy()
        {
            Vector2 direction = (Vector2)m_Target.position - rb.position;
            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb.angularVelocity = -rotateAmount * m_RotateSpeed;

            rb.velocity = transform.up;
        }

        private void SetTarget(Transform target)
        {
            m_Target = target.transform.GetComponent<Transform>();
        }

        protected override void OnProjectileLifeEnd(Collider2D collider, Vector2 point)
        {
            base.OnProjectileLifeEnd(collider, point);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif
    }
}


