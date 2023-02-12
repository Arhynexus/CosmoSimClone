using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CosmoSimClone
{
    public class Virtual_Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image m_Joystick;
        [SerializeField] private Image m_Stick;

        public Vector3 value {get; private set;}
        
        
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 position = Vector2.zero;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_Joystick.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            position.x = (position.x / m_Joystick.rectTransform.sizeDelta.x);
            position.y = (position.y / m_Joystick.rectTransform.sizeDelta.y);

            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            value = new Vector3(position.x, position.y, 0);

            if (value.magnitude > 1) value = value.normalized;

            float offsetX = m_Joystick.rectTransform.sizeDelta.x / 2 - m_Stick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_Joystick.rectTransform.sizeDelta.y / 2 - m_Stick.rectTransform.sizeDelta.y / 2;

            m_Stick.rectTransform.anchoredPosition = new Vector2(value.x * offsetX, value.y * offsetY);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            value = Vector3.zero;
            m_Stick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}


