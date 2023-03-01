using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{



    public class PlayerShipSelectionController : MonoBehaviour
    {
        [SerializeField] private SpaceShip m_SpaceShipPrefab;

        [SerializeField] private Text m_SpaceShipName;
        [SerializeField] private Text m_HealthPoints;
        [SerializeField] private Text m_Armor;
        [SerializeField] private Text m_ArmorResistance;
        [SerializeField] private Text m_Shield;
        [SerializeField] private Text m_Speed; // m_MaxlinearVelocity
        [SerializeField] private Text m_Agility; // m_MaxAngularVelocity

        [SerializeField] private Image m_ShipPreview;
        void Start()
        {
            if (m_SpaceShipName != null) m_SpaceShipName.text = m_SpaceShipPrefab.name;
            if (m_HealthPoints != null) m_HealthPoints.text = " ������: " + m_SpaceShipPrefab.Hitpoints.ToString();
            if (m_Armor != null) m_Armor.text = " ����� " + m_SpaceShipPrefab.Armor.ToString();
            if (m_ArmorResistance != null) m_ArmorResistance.text = " ����. ��.: " + m_SpaceShipPrefab.ResistanceOfArmor.ToString();
            if (m_Shield != null) m_Shield.text = " ����: " + m_SpaceShipPrefab.Shield.ToString();
            if (m_Speed != null) m_Speed.text = " ��������: " + m_SpaceShipPrefab.MaxLinearVelocity.ToString();
            if (m_Agility != null) m_Agility.text = " ������.: " + m_SpaceShipPrefab.MaxAngularVelocity.ToString();
            if (m_ShipPreview != null) m_ShipPreview.sprite = m_SpaceShipPrefab.SpaceShipImagePreview;
        }

        public void OnSelectShip()
        {
            LevelSequenceController.PlayerShip = m_SpaceShipPrefab;
            MainMenuController.Instance.gameObject.SetActive(true);
        }

        
    }
}
