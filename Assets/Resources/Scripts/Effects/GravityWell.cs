using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Radius;
        [SerializeField] private GameObject m_GravityAlert;

        private float timer;
        private float alertTimer;
        private float timerIfDestroyed;

        private float m_StartForce;

        private void Start()
        {
            m_StartForce = m_Force;
            m_GravityAlert.gameObject.SetActive(false);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            PowerUp powerUp = collision.transform.root.GetComponent<PowerUp>();
            BonusesGrabber grabber = collision.transform.root.GetComponent<BonusesGrabber>();
            if (grabber != null) return;
            if (powerUp != null) return;
            if (collision.attachedRigidbody == null) return;
            Destructible destructible = collision.transform.root.GetComponent<Destructible>();
            if (destructible.TeamId == 2 || destructible.TeamId == 1 && destructible.TeamId == 2)
            {
                Vector2 dir = transform.position - collision.transform.position;

                float dist = dir.magnitude;

                if (dist < m_Radius)
                {
                    Vector2 force = dir.normalized * m_Force * (dist / m_Radius);
                    collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
                    alertTimer += Time.deltaTime;



                    GravityAcceleration();
                }
                else
                {
                    m_Force = m_StartForce;
                }
            }
            if (destructible.TeamId == 1) return;
        }

        private void GravityAcceleration()
        {
            if (timer >= 1)
            {
                m_Force += 1;
                timer = 0;
                
                if(alertTimer < 1)
                {
                    m_GravityAlert.gameObject.SetActive(true);
                }
                if (alertTimer >= 1)
                {
                    m_GravityAlert.gameObject.SetActive(false);
                }
                if (alertTimer > 2)
                {
                    m_GravityAlert.gameObject.SetActive(true);
                    alertTimer = 0;
                }
            }
        }

        private void Update()
        {

            timer += Time.deltaTime;
            timerIfDestroyed += Time.deltaTime;
            
            if (timerIfDestroyed >= 2)
            {
                m_GravityAlert.gameObject.SetActive(false);
                timerIfDestroyed = 0;
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


