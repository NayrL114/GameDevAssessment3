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

    // HUD Transform
    public Transform hudTransform;
    public Vector3 drawCor;

    // Reference to PacManager
    public PacManager pac;

    // Reference to other scripts
    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        pac = gameObject.GetComponent<PacManager>();        
        inputManager = gameObject.GetComponent<InputManager>();

        drawCor = new Vector3(28f, 28f, 0f);
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
        Debug.Log(Screen.currentResolution);
        for (int i = 0; i < pac.Lives; i++)
        {
            Instantiate(LivesIndicator, drawCor, Quaternion.identity, hudTransform);
            drawCor.x += 40f;
        }
        drawCor = new Vector3(28f, 28f, 0f);
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
            // UI related stuff that is useful in current script
            gameTimerText = GameObject.FindWithTag("GameTimer").GetComponent<Text>();
            Button exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            ghostScareTimerText = GameObject.FindWithTag("ScareTimer").GetComponent<Text>();
            hudTransform = GameObject.FindWithTag("HUD").GetComponent<Transform>();
            ghostScareTimerText.text = "" + ghostTimerTxt;
            //pac = gameObject.GetComponent<PacManager>();

            // Help the InputManager to get stuffs in level 1
            inputManager.pacMovement = GameObject.FindWithTag("Player").GetComponent<PacMovement>();
            inputManager.pacAudioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();

            // Enabling the exitButton in level 1
            exitButton.onClick.AddListener(ExitGame);

            // Drawing the live counters. 
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