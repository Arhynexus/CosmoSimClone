using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class BonusesGrabber : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Radius;


        private void Start()
        {

        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.attachedRigidbody == null) return;

            PowerUp[] powerUps = collision.attachedRigidbody.GetComponents<PowerUp>();

            if (powerUps != null)
            {
                for (int i = 0; i < powerUps.Length; i++)
                {
                    Vector2 dir = transform.position - collision.transform.position;

                    float dist = dir.magnitude;
                    if (dist < m_Radius)
                    {
                        {
                            Vector2 force = dir.normalized * m_Force * (dist / m_Radius);
                            collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
                        }
                    }
                }

                
            }
        }


#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif
    }
}


