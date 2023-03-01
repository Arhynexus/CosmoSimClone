using CosmoSimClone;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalPlayerStatistics : MonoBehaviour
{
    [SerializeField] Text m_TotalKillsText;
    [SerializeField] Text m_TotalScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateTotalStats();
    }

    private void UpdateTotalStats()
    {
        m_TotalKillsText.text = PlayerPrefs.GetInt("TotalKills").ToString();
        m_TotalScoreText.text = PlayerPrefs.GetInt("TotalScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
