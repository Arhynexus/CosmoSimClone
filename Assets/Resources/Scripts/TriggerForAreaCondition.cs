using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{



    public class TriggerForAreaCondition : MonoBehaviour
    {

        [SerializeField] private LevelConditionArea m_AreaCondition;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BonusesGrabber grabber = collision.GetComponent<BonusesGrabber>();
            if (grabber != null) return;
            Debug.Log("Triggered");
            m_AreaCondition.SetCondition();
        }
    }
}
