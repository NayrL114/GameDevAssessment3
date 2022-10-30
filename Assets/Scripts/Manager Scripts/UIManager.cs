using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager instance = null;

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
    public float totalTime;// tracking total time spend on playing this level
    public float overTimer;
    public Text gameTimerText;

    // ghost scare timer
    public float ghostTimer;
    public int ghostTimerTxt = 9;
    public bool isScared = false;
    public Text ghostScareTimerText;
    public GameObject ghostScareText;

    // countdown timer
    public float countDown;
    public string countDownTxt;
    public int actualCountdown = 4;
    public Text countDownText;
    public bool isPause = false;

    public bool isOver = false;

    // Text variables        
    public Text scoreText;

    // Lives indicator, assigned prefab within Unity inspector
    public GameObject LivesIndicator;

    // HUD Transform
    public Transform hudTransform;
    public Vector3 drawCor;

    // Reference to other scripts
    public PacStudentController pacCtrl;
    public CherryController cherryController;
    public PacManager pacManager;
    public GhostManager ghostManager;

    // number of pallets in level 1
    public int palletNum = 224;

    // loadingScreen
    public RectTransform loadingCanvasT;
    public Image loadingPanel;
    public RectTransform loadingPanelT;

    // Lives object array
    public GameObject[] batteries = new GameObject[3];

    // buttons
    [SerializeField] public Button levelOneButton;
    [SerializeField] public Button exitButton;

    // bool for onSceneLoad
    private bool hasbeenloaded = false;

    // Main menu score and time
    public Text mainMenuScore;
    public Text mainMenuTime;

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

    // Start is called before the first frame update
    void Start()
    {
        //pac = gameObject.GetComponent<PacManager>();        
        //inputManager = gameObject.GetComponent<InputManager>();
        pacCtrl = gameObject.GetComponent<PacStudentController>();
        //pacCtrl.uimanager = this;
        cherryController = gameObject.GetComponent<CherryController>();
        ghostManager = gameObject.GetComponent<GhostManager>();
        ghostManager.uiManager = this;

        levelOneButton = GameObject.FindWithTag("LevelOneButton").GetComponent<Button>();
        levelOneButton.onClick.AddListener(LoadLevelOne);

        countDownText = GameObject.FindWithTag("CountDown").GetComponent<Text>();

        mainMenuScore = GameObject.FindWithTag("MainMenuScore").GetComponent<Text>();
        mainMenuTime = GameObject.FindWithTag("MainMenuTime").GetComponent<Text>();

        drawCor = new Vector3(28f, 28f, 0f);

        //Debug.Log(Screen.width + " " + Screen.height);
        loadingPanelT.sizeDelta = new Vector2(loadingCanvasT.rect.width, loadingCanvasT.rect.height);

        loadingPanel.enabled = false;

        getRecord();
        
        //ghostScareText.enabled = false;

        //SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Update is called once per frame
    void Update()
    {      

        if (!isPause)
        {
            if (gameTimerText != null && ghostScareTimerText != null && scoreText != null)
            {
                // Timer Stuffs
                gameTimer += Time.deltaTime;  
                //
                totalTime += Time.deltaTime;
                //
                if (isScared)
                //if (GhostManager.CurrentGhostState == GhostManager.GhostState.Scared 
                    //|| GhostManager.CurrentGhostState == GhostManager.GhostState.Recovering) 
                    //|| GhostManager.CurrentGhostState == GhostManager.GhostState.Dead)
                {
                    //ghostScareTimerText.enabled = true;
                    ghostScareText.SetActive(true);
                    ghostTimer += Time.deltaTime;                    
                    if (ghostTimer >= 1 /* && GhostState == Scared*/)
                    {
                        //Debug.Log(GhostManager.CurrentGhostState);
                        ghostScareTimerText.text = "" + ghostTimerTxt;
                        ghostTimerTxt--;
                        ghostTimer = 0;
                    }

                    if (ghostTimerTxt < 4)
                    {
                        if (!(GhostManager.CurrentGhostState == GhostManager.GhostState.Dead))
                        {
                            GhostManager.CurrentGhostState = GhostManager.GhostState.Recovering;
                        }                        
                    }

                    if (ghostTimerTxt < 0)
                    {
                        ghostTimerTxt = 9;
                        //ghostScareTimerText.enabled = false;
                        /*
                        if (!GhostManager.CurrentGhostState == GhostManager.GhostState.Dead)
                        {
                            GhostManager.CurrentGhostState = GhostManager.GhostState.Recovering;
                        }
                        */
                        if (!(GhostManager.CurrentGhostState == GhostManager.GhostState.Dead))
                        {
                            GhostManager.CurrentGhostState = GhostManager.GhostState.Normal;
                        }
                        isScared = false;
                        ghostScareText.SetActive(false);
                        ghostScareTimerText.text = "10";
                        ghostManager.bgmAudioSource.clip = ghostManager.normalClip;
                    }
                }

                //ghost scare timer stuff                

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
                scoreText.text = "" + pacManager.Score;
            }
        }
        else // if isPause is true
        {
            //Debug.Log("check 1");

            pacCtrl.enabled = false;

            if (countDownText != null && !isOver)
            {
                //Debug.Log("check 2");
                countDown += Time.deltaTime; 
                if (countDown >= 1)
                {
                    //Debug.Log("countDown " + actualCountdown);
                    
                    actualCountdown--;
                    countDownText.text = "" + actualCountdown;
                    countDown = 0;
                }

                if (actualCountdown == 0)
                {
                    countDownText.text = "Go!";
                    //Debug.Log("Go");
                }

                if (actualCountdown < 0)
                {
                    countDownText.text = "";

                    isPause = false;
                    loadingPanel.enabled = false;

                    cherryController.enabled = true;
                    pacCtrl.enabled = true;
                }
            }

            if (pacManager.Lives == 0)
            {                
                //pacCtrl.enabled = false;
                GameOver();
            }
        }
        
        if (pacManager != null && pacManager.palletNum == 0)
        {
            isPause = true;
            if (!isOver)
            {
                GameOver();
                //isPause = true;
                isOver = true;
            }
            
        }

    }// end of Update()

    public void GameOver()
    {
        isPause = false;
        pacCtrl.enabled = false;
        cherryController.enabled = false;

        loadingPanel.enabled = true;
        countDownText.enabled = true;
        countDownText.text = "Game Over";
        updateRecord();
        //Debug.Log("check 2");
        Invoke("ExitGame", 3f);
                
    }

    public void getRecord()
    {
        if (PlayerPrefs.HasKey("Score"))
        {
            mainMenuScore.text = PlayerPrefs.GetInt("Score") + "";
            mainMenuTime.text = ChangeTimeToTxt(PlayerPrefs.GetInt("Min")) 
                + ":" + ChangeTimeToTxt(PlayerPrefs.GetInt("Sec")) 
                + ":" + ConvertMilSecTxt(PlayerPrefs.GetInt("MilSec"));
        }
    }

    public void updateRecord()
    {
        if (!PlayerPrefs.HasKey("Score") && !PlayerPrefs.HasKey("MilSec") && !PlayerPrefs.HasKey("Sec") && !PlayerPrefs.HasKey("Min"))
        {// if there is no score and time record
            PlayerPrefs.SetInt("Score", pacManager.Score);
            PlayerPrefs.SetInt("MilSec", gameTimerMilSec);
            PlayerPrefs.SetInt("Sec", gameTimerSec);
            PlayerPrefs.SetInt("Min", gameTimerMin);
        }
        else
        {
            if (pacManager.Score > PlayerPrefs.GetInt("Score"))
            {// if there is a better score
                PlayerPrefs.SetInt("Score", pacManager.Score);
                PlayerPrefs.SetInt("MilSec", gameTimerMilSec);
                PlayerPrefs.SetInt("Sec", gameTimerSec);
                PlayerPrefs.SetInt("Min", gameTimerMin);
            }
            else if (pacManager.Score == PlayerPrefs.GetInt("Score"))
            {// if score is the same but better time
                if (gameTimerMilSec < PlayerPrefs.GetInt("MilSec"))
                {
                    PlayerPrefs.SetInt("MilSec", gameTimerMilSec);
                }
                
                if (gameTimerSec < PlayerPrefs.GetInt("Sec"))
                {
                    PlayerPrefs.SetInt("Sec", gameTimerSec);
                }
                
                if (gameTimerMin < PlayerPrefs.GetInt("Min"))
                {
                    PlayerPrefs.SetInt("Min", gameTimerMin);
                }             
                
            }
        }
        PlayerPrefs.Save();
    }

    public void DrawLives()
    {
        //Debug.Log(Screen.currentResolution);
        for (int i = 0; i < pacManager.Lives; i++)
        {
            batteries[i] = (GameObject)Instantiate(LivesIndicator, drawCor, Quaternion.identity, hudTransform);
            //Debug.Log(Screen.width);
            //drawCor.x += 80f; //40f
            drawCor.x += (float)(Screen.width * 0.04);
        }
        drawCor = new Vector3(28f, 28f, 0f);
    }

    public void DrawLostLives()
    {
        for (int i = 0; i < batteries.Length; i++)
        {
            if (batteries[i] != null)
            {
                batteries[i].SetActive(false);
                Destroy(batteries[i]);
            }
            
            //batteries[i] = null;
        }
        DrawLives();
    }

    public void LoadLevelOne()
    {
        Debug.Log("opening level 1");
        //levelOneButton.onClick.GetPersistentEventCount();
        levelOneButton.onClick.RemoveListener(LoadLevelOne);
        //levelOneButton.onClick.GetPersistentEventCount();        

        GameManager.currentGameState = GameManager.GameState.LevelOne;
        SceneManager.LoadScene(1);
        //pauseGame();
        //Invoke("resumeGame", 4f);
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        //SceneManager.sceneLoaded += OnSceneLoad;
        //SceneManager.sceneLoaded += OnSceneLoad;
        if (!hasbeenloaded)
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            hasbeenloaded = true;
        }
        isOver = false;
        //startCountDown();
    }

    public void LoadLevelTwo()
    {
        //GameManager.currentGameState = GameManager.currentGameState.LevelTwo;
        //SceneManager.LoadScene(2);// Reserved for level 2
    }

    public void ExitGame()
    {
        Debug.Log("returning to main menu");
        //exitButton.onClick.GetPersistentEventCount();
        if (exitButton != null){
            exitButton.onClick.RemoveListener(ExitGame);
        }
        
        //exitButton.onClick.GetPersistentEventCount();
        pacCtrl.resetMapArray();
        GameManager.currentGameState = GameManager.GameState.Start;        
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //Debug.Log("check 3");

        SceneManager.LoadScene(0);
        
        //getRecord();
        //SceneManager.sceneLoaded += OnSceneLoad;
        //SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void resetTimers()
    {
        gameTimer = 0;
        ghostTimer = 0;
        countDown = 0;
        actualCountdown = 4;
    }
    
    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            resetTimers();
            
            Debug.Log("Start scene is called. ");
            //Destroy(gameObject);
            //cherryController.enabled = !cherryController.enabled;
                        
            //if (cherryController.cherryObject != null)
            if (cherryController.enabled)
            {// destroy the stuffs in cherryController so there wont be errors
                //cherryController.destroyCherry();
                cherryController.clearStuff();
                //cherryController.clearTween();
            }
            cherryController.enabled = false;
            pacCtrl.enabled = false;
            //Button levelOneButton = GameObject.FindWithTag("LevelOneButton").GetComponent<Button>();
            levelOneButton = GameObject.FindWithTag("LevelOneButton").GetComponent<Button>();
            levelOneButton.onClick.AddListener(LoadLevelOne);
            Debug.Log("check 5");
            loadingPanel.enabled = false;
            countDownText.text = "";
            mainMenuScore = GameObject.FindWithTag("MainMenuScore").GetComponent<Text>();
            mainMenuTime = GameObject.FindWithTag("MainMenuTime").GetComponent<Text>();
            getRecord();
        }
        else if (scene.buildIndex == 1)
        {
            GhostManager.CurrentGhostState = GhostManager.GhostState.Normal;
            // UI related stuff that is useful in current script
            gameTimerText = GameObject.FindWithTag("GameTimer").GetComponent<Text>();
            exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
            ghostScareTimerText = GameObject.FindWithTag("ScareTimer").GetComponent<Text>();
            ghostScareText = GameObject.FindWithTag("ScareTimer");
            hudTransform = GameObject.FindWithTag("HUD").GetComponent<Transform>();
            //ghostScareTimerText.text = "" + ghostTimerTxt;
            scoreText = GameObject.FindWithTag("Score").GetComponent<Text>();
            //pac = gameObject.GetComponent<PacManager>();

            // Help the InputManager to get stuffs in level 1
            //inputManager.pacMovement = GameObject.FindWithTag("Player").GetComponent<PacMovement>();
            //inputManager.pacAudioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();

            // Help PacStudentController to get stuffs in level 1
            pacCtrl.pac = GameObject.FindWithTag("Player");            
            pacCtrl.uiManager = this;
            pacCtrl.pacAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>();
            pacCtrl.pacAudioSource = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
            pacCtrl.dustParticle = GameObject.FindWithTag("DustEffect").GetComponent<ParticleSystem>();
            pacCtrl.wallCollideParticle = GameObject.FindWithTag("WallEffect").GetComponent<ParticleSystem>();
            pacCtrl.deathParticle = GameObject.FindWithTag("DeathEffect").GetComponent<ParticleSystem>();            
            pacCtrl.pacHitAudioSource = GameObject.FindWithTag("HitWallAudio").GetComponent<AudioSource>();

            // Enable the cherryController attached to Game Manager
            //CherryController cherryController = gameObject.GetComponent<CherryController>();
            //cherryController.enabled = !cherryController.enabled;

            //cherryController.enabled = true;
            cherryController.cherrySpeed = 10f;

            // get the pacManager, and assign a cherryControlled into pacManager
            pacManager = GameObject.FindWithTag("Player").GetComponent<PacManager>();
            pacCtrl.pacManager = pacManager;
            pacManager.pacCtrl = pacCtrl;
            //pacManager.cherryCtrl = gameObject.GetComponent<CherryController>();

            ghostManager.ghostAnimator = GameObject.FindWithTag("Ghost").GetComponent<Animator>();
            ghostManager.bgmAudioSource = GameObject.FindWithTag("BGM").GetComponent<AudioSource>();

            // Enabling the exitButton in level 1
            exitButton.onClick.AddListener(ExitGame);

            // Drawing the live counters. 
            DrawLives();
                        
            loadingPanel.enabled = true;            
            isPause = true;
            ghostScareText.SetActive(false);
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
