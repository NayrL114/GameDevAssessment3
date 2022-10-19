using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    // Consider put the following enum and general movement (including pac, ghost and cherry) into a MovementManager
    public enum DirectionState
    {
        Up, 
        Down, 
        Left, 
        Right,
    }

    public static DirectionState currentDirectionState = DirectionState.Up;
    public static DirectionState lastDirectionState = DirectionState.Up;

    public int lastInputDirection;
    public int currentInputDirection;    
    public GameManager gameManager;

    public float gameTimer;
    public int gameTimerMilSec;
    public int gameTimerSec;
    public int gameTimerMin;
    public float fraction;
    public string MilSecTxt;
    public string SecTxt;
    public string MinTxt;

    public float ghostTimer;
    public int lastTime;

    public Text gameTimerText;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Timer Stuffs
        gameTimer += Time.deltaTime;
        ghostTimer += Time.deltaTime;
        //Debug.Log(gameTimer);
        if (gameTimer >= lastTime)
        {
            Debug.Log(lastTime);
            lastTime++;
        }

        // Below timer implementation is from https://answers.unity.com/questions/514378/timer-in-milliseconds-to-mmssms.html
        gameTimerMin = (int)gameTimer / 60;
        gameTimerSec = (int)gameTimer % 60;
        //gameTimerMilSec = ((int)gameTimer * 1000) % 1000;
        fraction = gameTimer * 1000;
        //Debug.Log(fraction);
        gameTimerMilSec = (int)(fraction % 1000);
        // Above codes are from a timer implementation guide found on UnityForum, linked in above commment.         
               
        MilSecTxt = ConvertMilSecTxt(gameTimerMilSec);
        SecTxt = ChangeTimeToTxt(gameTimerSec);
        MinTxt = ChangeTimeToTxt(gameTimerMin);

        //if (gameTimerSec < 10)

        //gameTimerText.text = gameTimerMin + ":" + gameTimerSec + ":" + gameTimerMilSec; //Time.deltaTime; //gameTimer
        gameTimerText.text = MinTxt + ":" + SecTxt + ":" + MilSecTxt; //Time.deltaTime; //gameTimer;

        // Input Stuffs
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentInputDirection = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentInputDirection = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            currentInputDirection = 2;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            currentInputDirection = 3;
        }

    }// end of Update()

    public void LoadLevelOne()
    {
        GameManager.currentGameState = GameManager.GameState.LevelOne;
        SceneManager.LoadScene(1);
    }

    public void LoadLevelTwo()
    {
        //GameManager.currentGameState = GameManager.currentGameState.LevelTwo;
        //SceneManager.LoadScene(2);// Reserved for level 2
    }

    public void ExitGame()
    {
        GameManager.currentGameState = GameManager.GameState.Start;
        SceneManager.LoadScene(0);
    }

    public string ChangeTimeToTxt(int input)
    {
        if (input < 10)
        {
            return "0" + input;
        }
        return "" + input;
    }

    public string ConvertMilSecTxt(int input)
    {        
        if (input < 100)
        {
            return "0" + input;
        }
        return "" + input;
    }

}
