using CosmoSimClone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{



    public class LevelConditionArea : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private Transform[] m_MoveStepsToTargetArea;

        public Transform[] MoveStepsToTargetArea => m_MoveStepsToTargetArea;

        public  float TotalDistanceToTargetArea { get; private set; }

        private void Start()
        {
            CalculateTotalDistance();
        }
        public void CalculateTotalDistance()
        {
            if (m_MoveStepsToTargetArea != null)
            {
                for (int i = 0; i < m_MoveStepsToTargetArea.Length - 1; i++)
                {
                    float stepdistance = Vector2.Distance(m_MoveStepsToTargetArea[i].transform.position, m_MoveStepsToTargetArea[i + 1].transform.position);
                    TotalDistanceToTargetArea += stepdistance;
                }
                Debug.Log("Total distance: " + TotalDistanceToTargetArea);
            }
        }
        
        private bool m_Reached;


        bool ILevelCondition.IsCompleted
        {
            get
            {
                return m_Reached;
            }
        }

        public void SetCondition()
        {
            m_Reached = true;
        }

    }
}
