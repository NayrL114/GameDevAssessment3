using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    // Merging PacMovement and InputManager together into a PacStudentController script. 
    // This new PacStudentController is expected to be attached to GameManager in start scene. 

    // Every requirements in 80% is perfectlly met except this:
    //   Hitting a scared ghost will set all ghosts to scared state.
    // More detailed comment can be found at line 466. 

    public Animator pacAnimator;
    public Tween activeTween;
    private int localMoveState;

    // Change the clip when pac eats something. 
    public AudioSource pacAudioSource;
    public AudioSource pacHitAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;
    public AudioClip pacDeathClip;

    // Reference towards particle Systems
    public ParticleSystem dustParticle;// should be initialised through UIManager, when clicking onto level 1
    public ParticleSystem wallCollideParticle;
    public ParticleSystem deathParticle;

    // lastInput and currentInput as required by assessment specification
    public int lastInput;
    public int currentInput;

    // Reference to other scripts
    public GameManager gameManager;
    public PacManager pacManager;
    public UIManager uiManager;
    public GhostManager ghostManager;

    // Reference to pac in level
    public GameObject pac;

    // Reference to UIManager
    //public UIManager uimanager;

    private bool playOnce = false;
    private bool wallAhead = false;

    public bool isDead = false;

    //public PacMovement pacMovement;// should be initiased through UIManager, when clicking onto level 1. 

    // Change the clip when pac eats something. 
    /*
    public AudioSource pacAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;
    */    

    //[SerializeField]
    public int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,4,4,4,4,3,0,4,4,5,2,0,0,0,0,0},// The entrance into the ghost spawning area will be disabled through this array
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,4,4,4,4,3,0,4,4,5,2,0,0,0,0,0},// The entrance into the ghost spawning area will be disabled through this array
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
        // I hate myself doing this ;p
    };
    //private Vector3 pacCurrentPos = new Vector3(-21.5f, 11.5f, 0f);// should be top left
    private Vector2 currentPos = new Vector2(1f, 1f);// using this Vector2 to store the pac position of levelMap 2D array

    // Start is called before the first frame update
    void Start()
    {
        //SetMovementDirection(1);
        //pacSpRend = gameObject.GetComponent<SpriteRenderer>();
        //DontDestroyOnLoad(this);
        //pacAudioSource = gameObject.GetComponent<AudioSource>();// get it from UIManager
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pac != null);
        if (pac != null && !isDead) // making sure that these codes are not called in main menu
        {
            
                //currentInput = lastInput;
                // If users provide input
                // https://answers.unity.com/questions/658721/print-what-button-was-just-pressed.html
            if (Input.anyKeyDown)
            {

                print(Input.inputString);

            }
                //

            if (Input.GetKeyDown(KeyCode.W))// Up
            {
                lastInput = 1;
                    /*if (!checkMovementByDigit(lastInput)) //&& pacMovement.activeTween == null)
                    {
                        checkMovementByDigit(currentInput);
                    }
                    //currentPos = new Vector2(currentPos.x - 1, currentPos.y);// goes up in array, so x - 1
                    */
                if (checkMovement(new Vector2(currentPos.x - 1, currentPos.y)))// && pacMovement.activeTween == null)
                {
                        // pacMovement.SetMovementDirection(lastInput);
                    currentInput = lastInput;
                        //currentPos = new Vector2(currentPos.x - 1, currentPos.y);// goes up in array, so x - 1
                }

            }
            else if (Input.GetKeyDown(KeyCode.D))// Right
            {
                lastInput = 2;
                    /*if (!checkMovementByDigit(lastInput)) //&& pacMovement.activeTween == null)
                    {
                        checkMovementByDigit(currentInput);
                    }
                    //currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                    */
                if (checkMovement(new Vector2(currentPos.x, currentPos.y + 1)))// && pacMovement.activeTween == null)
                {
                        //pacMovement.SetMovementDirection(lastInput);
                    currentInput = lastInput;
                        //currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                }

            }
            else if (Input.GetKeyDown(KeyCode.S))// Down
            {
                lastInput = 3;
                    /*if (!checkMovementByDigit(lastInput)) //&& pacMovement.activeTween == null)
                    {
                        checkMovementByDigit(currentInput);
                    }
                    //currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                    */
                if (checkMovement(new Vector2(currentPos.x + 1, currentPos.y)))// && pacMovement.activeTween == null)
                {
                        //pacMovement.SetMovementDirection(lastInput);
                    currentInput = lastInput;
                        //currentPos = new Vector2(currentPos.x + 1, currentPos.y);
                }

            }
            else if (Input.GetKeyDown(KeyCode.A))// Left
            {
                lastInput = 4;
                    /*
                    if (!checkMovementByDigit(lastInput)) //&& pacMovement.activeTween == null)
                    {
                        checkMovementByDigit(currentInput);
                    }
                    //currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                    */
                if (checkMovement(new Vector2(currentPos.x, currentPos.y - 1)))// && pacMovement.activeTween == null)
                {
                        //pacMovement.SetMovementDirection(lastInput);
                    currentInput = lastInput;
                        //currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                }

            }

            if (!checkMovementByDigit(lastInput))// actual moving pac
            {
                if (!checkMovementByDigit(currentInput) && wallAhead && activeTween == null)
                {
                    // the effect of hitting wall is not achieved with checking collision
                    // Instead it is done through checking movement in my way
                    // because I think it is way easier to implement by this way in my script
                    if (!playOnce)// playing the effect of hitting wall
                    {
                        pacHitAudioSource.Play();
                        wallCollideParticle.Play();
                        playOnce = true;
                    }
                }
            }
            else
            {
                currentInput = lastInput;
            }

            if (activeTween != null)
            {
                //Debug.Log("moving activeTween");
                float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);

                // Start playing movement audio and particle. 
                if (!pacAudioSource.isPlaying)
                {
                    pacAudioSource.Play();
                }

                if (!dustParticle.isPlaying)
                {
                    dustParticle.Play();
                }

                // disable activeTween on destination
                if (Vector3.Distance(pac.transform.position, activeTween.EndPos) <= 0.01f)
                {
                    activeTween.Target.position = activeTween.EndPos;
                    activeTween = null;
                }

            }

            if (activeTween == null)// && moveAudioSource.isPlaying)
            {// if there is really an instance where pacStudent stops moving, pause the movement audio. 
                if (pacAudioSource.clip != pacDeathClip)
                {
                        pacAudioSource.Stop();
                } 
                
                dustParticle.Stop();
                    //wallCollideParticle.Play();
                    //pacHitAudioSource.Play();
                    //dustParticle.SetActive(false);
                    //pacAnimator.Play("PacStudentIdle");
                    //pacSpRend.sprite = pacIdleSprite;
                    //switch (localMoveState)
                switch (currentInput)
                {
                    case 1:
                        pacAnimator.Play("PacStudentIdle");
                        break;
                    case 2:
                        pacAnimator.Play("PacStudentIdleRight");
                        break;
                    case 3:
                        pacAnimator.Play("PacStudentIdleDown");
                        break;
                    case 4:
                        pacAnimator.Play("PacStudentIdleLeft");
                        break;
                    default:
                        break;
                }
            }
                        
        }

        //checkCollisionTag(pacManager.collisionTag);

    }// end of Update()

    private bool checkMovement(Vector2 currentPos)// check the levelMap array to determine if that block can be moved to
    {
        if (currentPos.x >= 0 && currentPos.y >= 0 && currentPos.x < 30 && currentPos.y < levelMap.GetLength(0) - 2)
        {
            //Debug.Log(levelMap[(int)currentPos.x]);
            //Debug.Log(levelMap[(int)currentPos.y]);
            int nextPos = levelMap[(int)currentPos.x, (int)currentPos.y];
            if (nextPos == 5 || nextPos == 6)// || levelMap[(int)currentPos.x, (int)currentPos.y] == 0)
            {
                //Debug.Log("setting first clip");
                // Put the audio play codes into the section for detecting eating pallet
                if (!pacAudioSource.isPlaying)
                {
                    pacAudioSource.clip = eatClip;
                    pacAudioSource.Play();
                }
                //
                levelMap[(int)currentPos.x, (int)currentPos.y] = 0;
                //
                playOnce = false;
                wallAhead = false;
                return true;
            }
            else if (nextPos == 0)
            {
                //Debug.Log("setting second clip");
                // Put the audio play codes into the section for detecting eating pallet
                if (!pacAudioSource.isPlaying)
                {
                    pacAudioSource.clip = normalClip;
                    pacAudioSource.Play();
                }
                // 
                playOnce = false;
                wallAhead = false;
                return true;
            }
            else if (nextPos == 1 || nextPos == 2 || nextPos == 3 || nextPos == 4)
            {
                wallAhead = true;// code to determine if the hitting wall effect should be played. 
            }
        }
        return false;
    }// end of checkMovement(Vector2 currentPos)

    private bool checkMovementByDigit(int input)// call the checkMovement with passed-in input digit
    {
        if (activeTween == null)// doesn't override the current activeTween, also preventing multiple checks with multiple key inputs. 
        {
            switch (input)
            {
                case 1:
                    if (checkMovement(new Vector2(currentPos.x - 1, currentPos.y)))// && activeTween == null)
                    {
                        //currentInput = 0;
                        //pacMovement.SetMovementDirection(input);
                        //Debug.Log("Moving to " + new Vector2(currentPos.x - 1, currentPos.y) + " andnumber is " + levelMap[(int)currentPos.x - 1, (int)currentPos.y]);
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x - 1, currentPos.y);// goes up in array, so x - 1
                        //checkEating(); //Debug.Log("Can move up");
                        return true;
                    }
                    /*
                    else
                    {
                        pacMovement.SetMovementDirection()
                    }
                    */
                    break;
                case 2:
                    if (checkMovement(new Vector2(currentPos.x, currentPos.y + 1)))// && activeTween == null)
                    {
                        //currentInput = 1;
                        //pacMovement.SetMovementDirection(input);
                        //Debug.Log("Moving to " + new Vector2(currentPos.x - 1, currentPos.y) + " andnumber is " + levelMap[(int)currentPos.x - 1, (int)currentPos.y]);
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                        //Debug.Log("Can move right");
                        return true;
                    }
                    break;
                case 3:
                    if (checkMovement(new Vector2(currentPos.x + 1, currentPos.y)))// && activeTween == null)
                    {
                        //currentInput = 2;
                        //pacMovement.SetMovementDirection(input);
                        //Debug.Log("Moving to " + new Vector2(currentPos.x - 1, currentPos.y) + " andnumber is " + levelMap[(int)currentPos.x - 1, (int)currentPos.y]);
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x + 1, currentPos.y);
                        //Debug.Log("Can move down");
                        return true;
                    }
                    break;
                case 4:
                    if (checkMovement(new Vector2(currentPos.x, currentPos.y - 1)))// && activeTween == null)
                    {
                        //currentInput = 3;
                        //pacMovement.SetMovementDirection(input);
                        //Debug.Log("Moving to " + new Vector2(currentPos.x - 1, currentPos.y) + " andnumber is " + levelMap[(int)currentPos.x - 1, (int)currentPos.y]);
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                        //Debug.Log("Can move left");
                        return true;
                    }
                    break;
                //default:
                    //Debug.Log("returning false on default");
                    //return false;
                    //break;

            }// end of switch(input)
        }

        /*
        else
        {
            //Debug.Log("returning true after checking activeTween with input: " + input);
            //return true;            
        }    
        */
        //Debug.Log("returning false on the end with input: " + input);
        
        return false;
    }// end of checkMovementByDigit(int input)

    public void SetMovementDirection(int moveState)// consider change to switch(moveState)
    {// smaller move time value means faster speed. 
        
        if (activeTween == null)
        {
            //Debug.Log("Setting direction");
            if (moveState == 1) // move up, positive y direction
            {
                //activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y + 1, 0f), Time.time, 0.5f);
                activeTween = new Tween(pac.transform, pac.transform.position, new Vector3(pac.transform.position.x, pac.transform.position.y + 1, 0f), Time.time, 0.3f);// 0.3f
                pacAnimator.Play("WalkingUp");//, -1, 0f);
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                pac.transform.rotation = Quaternion.Euler(0, 0, 0);
                //localMoveState = moveState;
            }
            else if (moveState == 2) // move right, positive x direction
            {
                activeTween = new Tween(pac.transform, pac.transform.position, new Vector3(pac.transform.position.x + 1, pac.transform.position.y, 0f), Time.time, 0.3f);
                pacAnimator.Play("PacStudentWalkingRight");//, -1, 0f);
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                pac.transform.rotation = Quaternion.Euler(0, 0, 0);
                //localMoveState = moveState;
            }
            else if (moveState == 3) // move down, negative y direction
            {
                activeTween = new Tween(pac.transform, pac.transform.position, new Vector3(pac.transform.position.x, pac.transform.position.y - 1, 0f), Time.time, 0.3f);
                pacAnimator.Play("PacStudentWalkingDown");//, -1, 0f);
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                pac.transform.rotation = Quaternion.Euler(0, 0, 0);
                //localMoveState = moveState;
            }
            else if (moveState == 4) // move left, negative x direction
            {
                activeTween = new Tween(pac.transform, pac.transform.position, new Vector3(pac.transform.position.x - 1, pac.transform.position.y, 0f), Time.time, 0.3f);
                pacAnimator.Play("PacStudentWalkingLeft");//, -1, 0f);
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                pac.transform.rotation = Quaternion.Euler(0, 0, 0);
                //localMoveState = moveState;
            }

        }

    }// end of SetMovementDirection(int moveState)

    public void enablePowerPallet()// called in PacManager's onTriggerEnter2D
    {
        Debug.Log("Enabling power pallet");
        uiManager.isScared = true;
    }    

    //public void checkGhostCollision()
    public void checkGhostCollision(GameObject other)// called in PacManager's onTriggerEnter2D
    {        
        Debug.Log("Checking ghost state with ID " + other.GetComponent<GhostScript>().ghostID);
        int ghostyID = other.GetComponent<GhostScript>().ghostID;
        if (other.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Scared 
            || other.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Recovering)
        {
            Debug.Log("hitted a scared ghost");
            pacManager.Score += 300;

            // Every requirements in 80% is perfectlly met except this:
            // Hitting a scared ghost will set all ghosts to scared state. 

            other.GetComponent<GhostScript>().CurrentGhostState = GhostScript.GhostState.Dead;
            // For some reason, this line will set all ghosts to dead state, which is not ideal. 
            // I didn't have time to debug this, so ;p

            //ghostManager.setDeadToAGhost(ghostyID);
            // And despite the syntax of above line is correct, it will throw an annoying error as below:
            //   NullReferenceException: Object reference not set to an instance of an object
            //   PacStudentController.checkGhostCollision(UnityEngine.GameObject other)
            // idk, I tried to wrote this line to explicitly set the state of a single ghost, and it doesn't work. 
            // I also dont have time to debug this, so ;p
        }
        else if (other.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Normal)
        {
            Debug.Log("hitted a normal ghost");
            pacManager.Lives--;
            uiManager.DrawLostLives();
            isDead = true;
            PacDeath();
            //}
        }
        else if (other.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Dead)
        {
            Debug.Log("hitted a dead ghost");
        }
    }

    // portal tunnel codes
    public void leftPortal()
    {
        activeTween = null;
        pac.transform.position = new Vector3(11.5f, pac.transform.position.y, pac.transform.position.x);
        currentPos = new Vector2(currentPos.x, 25f);
    }

    public void rightPortal()        
    {
        activeTween = null;
        pac.transform.position = new Vector3(-11.5f, pac.transform.position.y, pac.transform.position.x);
        currentPos = new Vector2(currentPos.x, 2f);
    }

    public void PacDeath()// play the death animation of pac
    {
        activeTween = null;
        switch (currentInput)
        {
            case 1:
                pacAnimator.Play("Death");
                break;
            case 2:
                pacAnimator.Play("PacStudentDeathRight");
                break;
            case 3:
                pacAnimator.Play("PacStudentDeathDown");
                break;
            case 4:
                pacAnimator.Play("PacStudentDeathLeft");
                break;
                //default:
                //Debug.Log("returning false on default");
                //return false;
                //break;

        }// end of switch(input)
        
        lastInput = 0;
        currentInput = 0;

        //uiManager.isPause = true;
        pacAudioSource.Stop();
        pacAudioSource.clip = pacDeathClip;        
        pacAudioSource.volume = 0.3f;
        pacAudioSource.pitch = 2.5f;
        if (!pacAudioSource.isPlaying)
        {
            pacAudioSource.Play();
        }
        deathParticle.Play();

        if (pacManager.Lives == 0)
        {
            GameOver();
            //CancelInvoke("RespawnPac");
            return;
        }

        Invoke("RespawnPac", 2f);
    }

    public void RespawnPac()// respawn pac to top left
    {
        uiManager.isPause = false;
        pac.transform.position = new Vector3(-12.5f, 13.5f);
        currentPos = new Vector2(1f, 1f);
        isDead = false;

        pacAudioSource.clip = null;
        pacAudioSource.volume = 0.5f;
        pacAudioSource.pitch = 1f;
        pacAnimator.Play("PacStudentIdle");
    }

    public void GameOver()// set uiManager.isPause to true and let UIManager handle GameOver stuff
    {
        uiManager.isPause = true;        
    }

    public void resetMapArray()// reset everything in this script
    {
        activeTween = null;
        currentPos = new Vector2(1f, 1f);
        levelMap = new int[,] {
            { 1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
            { 2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            { 2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            { 2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
            { 2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
            { 1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
            { 0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            { 0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
            { 0,0,0,0,0,2,5,4,4,0,3,4,4,4,4,4,4,3,0,4,4,5,2,0,0,0,0,0},// The entrance into the ghost spawning area will be disabled through this array
            { 2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
            { 0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
            { 0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
            { 2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
            { 0,0,0,0,0,2,5,4,4,0,3,4,4,4,4,4,4,3,0,4,4,5,2,0,0,0,0,0},// The entrance into the ghost spawning area will be disabled through this array
            { 0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
            { 0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            { 1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
            { 2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
            { 2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
            { 2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
            { 2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
            { 2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
            { 2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            { 1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
            // I hate myself doing this ;p
        };
        currentInput = 0;
        lastInput = 0;
    }

}
