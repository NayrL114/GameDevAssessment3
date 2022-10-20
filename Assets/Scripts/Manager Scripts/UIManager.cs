using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // main timer that stacks Time.deltaTime
    public float gameTimer;

    // 00:00:00 timer
    public int gameTimerMilSec;
    public int gameTimerSec;
    public int gameTimerMin;
    public float fraction;
    public string MilSecTxt;
    public string SecTxt;
    public string MinTxt;

    // ghost scare timer
    public float ghostTimer;
    public int ghostTimerTxt = 10;

    // Text variables
    public Text gameTimerText;
    public Text ghostScareTimerText;
    public Text scoreText;

    // Lives indicator, assigned prefab within Unity inspector
    public GameObject LivesIndicator;

    // Reference to PacManager
    public PacManager pac;

    // Start is called before the first frame update
    void Start()
    {
        pac = gameObject.GetComponent<PacManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (gameTimerText != null && ghostScareTimerText != null)
        {
            // Timer Stuffs
            gameTimer += Time.deltaTime;
            ghostTimer += Time.deltaTime;

            //ghost scare timer stuff
            if (ghostTimer >= 1 /* && GhostState == Scared*/)
            {
                Debug.Log(ghostTimerTxt);
                ghostScareTimerText.text = "" + ghostTimerTxt;
                ghostTimerTxt--;
                ghostTimer = 0;
            }

            if (ghostTimerTxt < 0)
            {
                ghostTimerTxt = 10;                
            }

            // 00:00:00 timer stuff
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
            //
        }

    }// end of Update()

    public void DrawLives()
    {
        Vector3 drawCor = new Vector3(-31.5f, -15f, 0f);
        for (int i = 0; i < pac.Lives; i++)
        {
            Instantiate(LivesIndicator, drawCor, Quaternion.identity);
            drawCor.x += 1.5f;
        }
    }

    public void LoadLevelOne()
    {
        GameManager.currentGameState = GameManager.GameState.LevelOne;
        SceneManager.LoadSceneAsync(1);
        SceneManager.sceneLoaded += OnSceneLoad;
        //SceneManager.sceneLoaded += OnSceneLoad;
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
        SceneManager.sceneLoaded += OnSceneLoad;
        //SceneManager.sceneLoaded += OnSceneLoad;
    }
    
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            Debug.Log("Start scene is called. ");
        }
        else if (scene.buildIndex == 1)
        {
            gameTimerText = GameObject.FindWithTag("GameTimer").GetComponent<Text>();
            Button exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            ghostScareTimerText = GameObject.FindWithTag("ScareTimer").GetComponent<Text>();
            ghostScareTimerText.text = "" + ghostTimerTxt;
            //pac = gameObject.GetComponent<PacManager>();

            exitButton.onClick.AddListener(ExitGame);

            DrawLives();
        }
    }    

    // Timer helper function
    public string ChangeTimeToTxt(int input)
    {
        if (input < 10)
        {
            return "0" + input;
        }
        return "" + input;
    }

    // Timer helper function
    public string ConvertMilSecTxt(int input)
    {
        if (input < 10)
        {
            return "00" + input;
        }
        else if (input < 100)
        {
            return "0" + input;
        }
        return "" + input;
    }

}// end of UIManager.cs
