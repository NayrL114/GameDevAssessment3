using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    
    void Awake()
    {
        LoadScore();
        LoadTime();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore()
    {
        // Create a ScoreManager for recording score and time. 
        //PlayerPrefs.SetInt("Score", (int)ScoreManager.CurrentScore);
    }

    public void SaveTime()
    {
        // Create a ScoreManager for recording score and time. 
        //PlayerPrefs.SetInt("Time", (float)ScoreManager.CurrentScore);
        // Remember that time format is 00:00:00, 
        // So maybe the time should be stored as 3 integers seperate for minute, second, miliseconds. 
    }

    public void LoadScore()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            // Set the score variable in ScoreManager
            //ScoreManager.CurrentScore = (ScoreManager.Score)PlayerPrefs.GetInt("Score");
        }
    }

    public void LoadTime()
    {
        if (PlayerPrefs.HasKey("Time"))
        {
            // Set the time variable in ScoreManager
            //ScoreManager.CurrentScore = (ScoreManager.Score)PlayerPrefs.GetInt("Score");
        }
    }
}
