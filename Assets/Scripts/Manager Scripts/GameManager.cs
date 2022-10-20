using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Similar enum implementation to week 9 lab. 
    public enum GameState
    {
        Start, LevelOne, LevelTwo
    };
    public static GameState currentGameState = GameState.Start;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
