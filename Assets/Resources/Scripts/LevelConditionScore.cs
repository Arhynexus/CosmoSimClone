using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace CosmoSimClone
{



    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_ScoreCondition;
        public int ScoreCondition => m_ScoreCondition;

        [SerializeField] private ScoreController m_ScoreController;

        private int m_CurrentScore;

        private bool m_Reached;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (m_CurrentScore >= m_ScoreCondition)
                    {
                        m_Reached = true;
                    }
                }
                return m_Reached;
            }
        }

        void Update()
        {
            m_CurrentScore = m_ScoreController.CurrentScore;
        }
    }
}
