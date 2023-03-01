using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CosmoSimClone
{
    public class DebrisSpawner : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs;

        [SerializeField] private CircleArea m_Area;

        [SerializeField] private int m_NumDebris;

        [SerializeField] private float m_RandomSpeed;

        //private int m_SpawnedDebris = 0;
        //
        //private float m_Timer = 10f;



        void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris() //Пофиксить множественный спавн объектов
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomINsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if(rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2) Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }

        void Update()
        {
            
        }
    }
}
