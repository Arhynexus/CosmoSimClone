using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class MovementController : MonoBehaviour
    {
        public enum ControllerType
        {
            Keyboard,
            Virtual_Joystick
        }

        [SerializeField] private SpaceShip m_TargetSpaceShip;

        public void SetTargetShip(SpaceShip ship) => m_TargetSpaceShip = ship;

        public SpaceShip TargetSpaceShip => m_TargetSpaceShip;

        [SerializeField] private Virtual_Joystick m_Virtual_Joystick;

        [SerializeField] private ControllerType m_ControlMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Update()
        {
            if (m_TargetSpaceShip == null) return;
            
            if (m_ControlMode == ControllerType.Keyboard) ControlKeyboard();

            if (m_ControlMode == ControllerType.Virtual_Joystick) ControlVirtual_Joystick();
        }

        private void Start()
        {
            if (m_ControlMode == ControllerType.Keyboard)
            {
                m_Virtual_Joystick.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }

            else
            {
                m_Virtual_Joystick.gameObject.SetActive(true);
                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void ControlVirtual_Joystick()
        {
            Vector3 dir = m_Virtual_Joystick.value;

            var dot = Vector2.Dot(dir, m_TargetSpaceShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetSpaceShip.transform.right);

            if (m_MobileFirePrimary.Hold == true)
            {
                m_TargetSpaceShip.Fire(TurretMode.Main);
            }

            if (m_MobileFireSecondary.Hold == true)
            {
                m_TargetSpaceShip.Fire(TurretMode.Secondary);
            }

            m_TargetSpaceShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetSpaceShip.TorqueControl = -dot2;
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;
            
            if (Input.GetKey(KeyCode.W)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.S)) thrust = -1.0f;

            if (Input.GetKey(KeyCode.A)) torque = 1.0f;
            if (Input.GetKey(KeyCode.D)) torque = -1.0f;

            if(Input.GetKey(KeyCode.Mouse0))
            {
                m_TargetSpaceShip.Fire(TurretMode.Main);
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                m_TargetSpaceShip.Fire(TurretMode.Secondary);
            }

            m_TargetSpaceShip.ThrustControl = thrust;
            m_TargetSpaceShip.TorqueControl = torque;
        }
    }
}


