using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{
    public class PlayerHUD_UI : MonoBehaviour
    {
        [SerializeField] private Player m_Player;

        [SerializeField] private Text m_ShieldText;
        [SerializeField] private Text m_ArmorText;
        [SerializeField] private Text m_HealthText;
        [SerializeField] private Text m_EnergyText;
        [SerializeField] private Text m_AmmoText;
        [SerializeField] private Image m_ShieldBar;
        [SerializeField] private Image m_ArmorBar;
        [SerializeField] private Image m_HealthBar;
        [SerializeField] private Image m_EnergyBar;

        //private float m_Timer = 1f;

        private SpaceShip m_SpaceShip;

        private void Start()
        {
            m_SpaceShip = m_Player.ActiveShip;
            m_SpaceShip.ChangeHitPoints.AddListener(UpdateHUD);
            m_SpaceShip.ChangeEnergy.AddListener(UpdateEnergyHUD);
            m_SpaceShip.ChangeAmmo.AddListener(UpdateAmmoHUD);
        }

        
        public void SetActiveShip()
        {
            m_SpaceShip = m_Player.ActiveShip;
            m_SpaceShip.ChangeHitPoints.AddListener(UpdateHUD);
            m_SpaceShip.ChangeEnergy.AddListener(UpdateEnergyHUD);
            m_SpaceShip.ChangeAmmo.AddListener(UpdateAmmoHUD);
            m_SpaceShip.ChangeHitPoints.Invoke();
            m_SpaceShip.ChangeEnergy.Invoke();
            m_SpaceShip.ChangeAmmo.Invoke();
        }
        private void UpdateAmmoHUD()
        {
            int ammo = (int)m_SpaceShip.CurrentSecondaryAmmo;
            m_AmmoText.text = "X " + ammo.ToString();
        }

        private void OnDestroy()
        {
            m_SpaceShip.ChangeHitPoints.RemoveListener(UpdateHUD);
            m_SpaceShip.ChangeEnergy.RemoveListener(UpdateEnergyHUD);
            m_SpaceShip.ChangeAmmo.RemoveListener(UpdateAmmoHUD);
        }

        private void Update()
        {

        }


        public void UpdateEnergyHUD()
        {
            int maxEnergy = m_SpaceShip.MaxEnergy;
            int energy = (int)m_SpaceShip.CurrentPrimaryEnergy;
            m_EnergyBar.fillAmount = (float)energy / (float)maxEnergy;
            m_EnergyText.text = energy.ToString();
            if (m_EnergyBar.fillAmount == 1) return;
        }
        public void UpdateHUD()
        {
            int shield = m_SpaceShip.Shield;
            int armor = m_SpaceShip.Armor;
            int health = m_SpaceShip.CurrentHealthPoints;
            int maxShield = m_SpaceShip.MaxShield;
            int maxArmor = m_SpaceShip.MaxArmor;
            int maxHealth = m_SpaceShip.Hitpoints;
            
            m_ShieldBar.fillAmount =  (float) shield / (float)maxShield;
            m_ArmorBar.fillAmount  =  (float) armor / (float)maxArmor;
            m_HealthBar.fillAmount =  (float) health / (float)maxHealth;
            
            m_ShieldText.text = shield.ToString();
            m_ArmorText.text = armor.ToString();
            m_HealthText.text = health.ToString();
            
        }

    }
}


