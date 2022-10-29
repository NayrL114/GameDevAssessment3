using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //public static GameManager instance = null;
    
    // Similar enum implementation to week 9 lab. 
    public enum GameState
    {
        Start, LevelOne, LevelTwo
    };
    public static GameState currentGameState = GameState.Start;

    /*
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    */

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
