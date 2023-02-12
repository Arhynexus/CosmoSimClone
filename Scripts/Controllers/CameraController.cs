using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] Camera m_Camera;

        [SerializeField] Transform m_Target;

        [SerializeField] private float m_InteroplationLInear;

        [SerializeField] private float m_InteroplationAngular;

        [SerializeField] private float m_CameraZOffset;

        [SerializeField] private float m_ForwardOffset;

        [SerializeField] private float m_CameraDistance;

        [SerializeField] private MovementController m_MovementController;

        private float m_DefaultForwardOffset = 0;
        private float m_CurrentForwardOffset;

        private float m_CurrentCameraSize;
        private float m_DefaultCameraSize = 10;


        void FixedUpdate()
        {
            if (m_Camera == null || m_Target == null) return;
            CameraViewController();
            if (Input.GetKey(KeyCode.R))
            {
                m_CurrentForwardOffset = m_ForwardOffset;
                m_CurrentCameraSize = m_CameraDistance;
            }

        }

        private void CameraViewController()
        {
            if (m_MovementController.TargetSpaceShip.ThrustControl == 0)//  Input.GetKey(KeyCode.DownArrow) == false && Input.GetKey(KeyCode.UpArrow) == false)
            {
                if (m_CurrentForwardOffset < 0) m_CurrentForwardOffset += 0.2f;
                if (m_CurrentForwardOffset > 0) m_CurrentForwardOffset -= 0.2f;
                if (m_CurrentForwardOffset == m_DefaultForwardOffset) m_CurrentForwardOffset = m_DefaultForwardOffset;
                m_CurrentCameraSize -= 0.1f;
                if (m_CurrentCameraSize < m_DefaultCameraSize) m_CurrentCameraSize = m_DefaultCameraSize;
            }


            if (m_MovementController.TargetSpaceShip.ThrustControl > 0) //Input.GetKey(KeyCode.UpArrow) == true)
            {
                m_CurrentForwardOffset += 0.1f;
                if (m_CurrentForwardOffset >= m_ForwardOffset) m_CurrentForwardOffset = m_ForwardOffset;
                m_CurrentCameraSize += 0.1f;
                if (m_CurrentCameraSize >= m_CameraDistance) m_CurrentCameraSize = m_CameraDistance;
            }

            if (m_MovementController.TargetSpaceShip.ThrustControl < 0) //Input.GetKey(KeyCode.DownArrow) == true)
            {
                m_CurrentForwardOffset -= 0.1f;
                if (m_CurrentForwardOffset <= -m_ForwardOffset) m_CurrentForwardOffset = -m_ForwardOffset;
                m_CurrentCameraSize += 0.1f;
                if (m_CurrentCameraSize >= m_CameraDistance) m_CurrentCameraSize = m_CameraDistance;
            }





            Vector2 camPos = m_Camera.transform.position;
            Vector2 targetPos = m_Target.transform.position + m_Target.transform.up * m_CurrentForwardOffset;

            Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InteroplationLInear * Time.deltaTime);

            m_Camera.transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset);
            m_Camera.orthographicSize = m_CurrentCameraSize;

            if (m_InteroplationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InteroplationAngular * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.R)) return;
        }

        public void SetTarget(Transform newTarget)
        {
            m_Target = newTarget;
        }
    }
}


