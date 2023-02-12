using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class Healer : Pickable
    {
        public enum TypeOfRestore
        {
            shield,
            armor,
            health
        }
        
        [SerializeField] private GameObject m_ImpactEffectHealth;

        [Header("Спрайты для бонусов восстановления")]
        [SerializeField] private Sprite m_HealthSPrite;
        [SerializeField] private Sprite m_ArmorSprite;
        [SerializeField] private Sprite m_ShieldSprite;
        [SerializeField] private Sprite m_DefaultSprite;

        [SerializeField] private EventManager m_EventManager;

        [Header("Восстанавливаемый ресурс корабля")]
        /// <summary>
        /// Тип восстанавливаемого ресурса
        /// </summary>
        [SerializeField] public TypeOfRestore m_Type;

        [Header("Объем восстанавливаемого ресурса")]
        /// <summary>
        /// Количество восстанавливаемого ресурса
        /// </summary>
        [SerializeField] private int m_AddHeal;
        public int AddHeal => m_AddHeal;

        private SpriteRenderer m_SriteRenderer;




        private void Start()
        {
           m_EventManager = FindObjectOfType<EventManager>();
            m_SriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (m_SriteRenderer.sprite == null) m_SriteRenderer.sprite = m_DefaultSprite;
            if (m_Type == TypeOfRestore.shield) m_SriteRenderer.sprite = m_ShieldSprite;
            if (m_Type == TypeOfRestore.armor) m_SriteRenderer.sprite = m_ArmorSprite;
            if (m_Type == TypeOfRestore.health) m_SriteRenderer.sprite = m_HealthSPrite;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            Debug.Log("Столкновение с объектом", collision);

            Destructible destructible = collision.transform.root.GetComponent<Destructible>();

            if (destructible == null) return;

            if (m_Type == TypeOfRestore.shield)
            {
                destructible.RestoreShield(m_AddHeal);
                Destroy(gameObject);
                //Instantiate(m_ImpactEffectHealth);
            }
            if (m_Type == TypeOfRestore.health)
            {
                destructible.RestoreHealth(m_AddHeal);
                Destroy(gameObject);
                //Instantiate(m_ImpactEffectHealth);
            }
            if (m_Type == TypeOfRestore.armor)
            {
                destructible.RestoreArmor(m_AddHeal);
                Destroy(gameObject);
                //Instantiate(m_ImpactEffectHealth);
            }
            destructible.ChangeHitPoints.Invoke();
        }




    }
}


