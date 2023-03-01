using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{



    public class LevelConditionProgressPanel : MonoBehaviour
    {

        [SerializeField] private LevelController m_LevelController;
        [SerializeField] private ScoreController m_ScoreController;

        [SerializeField] private Text m_ScoreText;
        [SerializeField] private Text m_AreaText;
        [SerializeField] private Text m_TimeText;

        private Timer ConditionTimerForShowConditions;

        readonly float conditionTimeForShowConditions = 0.1f;




        private void Start()
        {
            ConditionTimerForShowConditions = new Timer(conditionTimeForShowConditions);
            ConditionTimerForShowConditions.Start(conditionTimeForShowConditions);
            GetAndShowConditions();
        }

        private LevelConditionScore scoreCondition;
        private LevelConditionArea areaCondition;
        private LevelConditionTime timeCOndition;

       
        private void GetAndShowConditions()
        {
            scoreCondition = FindObjectOfType<LevelConditionScore>();
            areaCondition = FindObjectOfType<LevelConditionArea>();
            timeCOndition = FindObjectOfType<LevelConditionTime>();

            if(scoreCondition != null)
            {
                m_ScoreText.text = "Набрать очки: " + scoreCondition.ScoreCondition.ToString();
            }
            
            if (areaCondition != null)
            {
                m_AreaText.text = "Добраться до точки эвакуации";
            }

            if (timeCOndition != null)
            {
            m_levelConditionTime = timeCOndition.ConditionTime;
            timeConditionForUpdate = timeCOndition.ConditionTime;
            m_LevelConditionTimer = new Timer(m_levelConditionTime);
            m_TimeText.text = "Продержаться минут: " + (m_levelConditionTime / 60).ToString();
            m_LevelConditionTimer.Start(m_levelConditionTime);
            StartCoroutine(StartTimerForTimeCondition());
            }
            return;
        }

        private void UpdateScoreCondition()
        {
            if (scoreCondition != null)
            {
                int scoreConditionForUpdate = scoreCondition.ScoreCondition;
                int currentScore = m_ScoreController.CurrentScore;
                int showRemainigScore = scoreConditionForUpdate - currentScore;
                m_ScoreText.text = "Набрать очки: " + showRemainigScore.ToString();
                if (showRemainigScore <= 0)
                {
                    m_ScoreText.text = "Набрать очки: Достигнуто";
                }
            }
        }

        private float timeConditionForUpdate;
        private Timer m_LevelConditionTimer;
        private float m_levelConditionTime;
        private IEnumerator StartTimerForTimeCondition()
        {
            if (timeCOndition != null)
            {
                while (timeConditionForUpdate > 0)
                {
                    m_LevelConditionTimer.RemoveTime(Time.deltaTime);
                    timeConditionForUpdate = m_LevelConditionTimer.CurrentTime;
                    float minutes = Mathf.FloorToInt(timeConditionForUpdate / 60);
                    float seconds = Mathf.FloorToInt(timeConditionForUpdate % 60);
                    if(minutes < 0 || seconds < 0 )
                    {
                        minutes = 0;
                        seconds = 0;
                    }
                    m_TimeText.text = string.Format("Продержаться минут: {0:00} : {1:00}", minutes, seconds);
                    yield return null;
                }
            }
        }
        private void UpdateTimeCondition()
        {
            if (timeCOndition != null)
            {
                
            }
        }

        private void Update()
        {
            
            ConditionTimerForShowConditions.RemoveTime(Time.deltaTime);

            if (ConditionTimerForShowConditions.IsFinished)
            {
                
                UpdateScoreCondition();
                if (timeCOndition != null)
                {
                    
                }
            }
        }
    }
}