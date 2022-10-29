using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{

    // Merging PacMovement and InputManager together into a PacStudentController script. 
    // This new PacStudentController is expected to be attached to GameManager in start scene. 

    public Animator pacAnimator;
    public Tween activeTween;
    private int localMoveState;

    // Change the clip when pac eats something. 
    public AudioSource pacAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;

    // Reference towards particle System for simulating dust
    public ParticleSystem dustParticle;// should be initialised through UIManager, when clicking onto level 1

    // lastInput and currentInput as required by assessment specification
    public int lastInput;
    public int currentInput;

    // Reference to other scripts
    public GameManager gameManager;

    // Reference to pac in level
    public GameObject pac;

    //public PacMovement pacMovement;// should be initiased through UIManager, when clicking onto level 1. 

    // Change the clip when pac eats something. 
    /*
    public AudioSource pacAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;
    */    

    private int[,] levelMap = {
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


    // Below are the codes from PacMovement

    /*
    public Animator pacAnimator;
    public Tween activeTween;
    private int localMoveState;

    // Change the clip when pac eats something. 
    public AudioSource pacAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;

    // Reference towards particle System for simulating dust
    public ParticleSystem dustParticle;

    //public bool isMoving;
    //public SpriteRenderer pacSpRend;
    //public Sprite pacIdleSprite;    
    */

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
        if (pac != null) // making sure that these codes are not called in main menu
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

            printInput(lastInput);

            //Debug.Log("lastInput is " + printInput(lastInput) + " and currentInput is " + printInput(currentInput));

            //currentInput = lastInput;
            //bool moveParameter = checkMovementByDigit(lastInput);
            //Debug.Log(moveParameter);
            //if (moveParameter == false)
            if (!checkMovementByDigit(lastInput)) //&& pacMovement.activeTween == null)
            {
                checkMovementByDigit(currentInput);
                /*
                if (!checkMovementByDigit(currentInput))
                {
                    Debug.Log("stopping");
                    //dustParticle.Stop();
                }
                else
                {
                    Debug.Log("moving based on currentInput");
                }
                */
            }
            else
            {
                Debug.Log("moving based on lastInput");
                currentInput = lastInput;
            }

            // Below are the code from PacMovement

            if (activeTween != null)
            {
                //Debug.Log("moving activeTween");
                float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);

                // Start playing movement audio. 
                if (!pacAudioSource.isPlaying)
                {
                    //moveAudioSource.loop = true;
                    pacAudioSource.Play();
                }

                /*
                if (!dustParticle.activeSelf)
                {
                    dustParticle.SetActive(true);
                }
                */
                if (!dustParticle.isPlaying)
                {
                    //Debug.Log("moving activeTween");
                    dustParticle.Play();
                }

                if (Vector3.Distance(pac.transform.position, activeTween.EndPos) <= 0.01f)
                {
                    
                    activeTween.Target.position = activeTween.EndPos;
                    activeTween = null;
                    /*
                    moveState++;
                    if (moveState >= 4)
                    {
                        moveState = 0;
                    }
                    SetMovementDirection(moveState);

                    */

                }

            }

            if (activeTween == null)// && moveAudioSource.isPlaying)
            {// if there is really an instance where pacStudent stops moving, pause the movement audio. 
                pacAudioSource.Stop();
                dustParticle.Stop();
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

    }// end of Update()

    private bool checkMovement(Vector2 currentPos)
    {
        //return (currentPos.x >= 0 && currentPos.y >= 0 && currentPos.x < levelMap.GetLength(0) && currentPos.y < levelMap.GetLength(0));

        if (currentPos.x >= 0 && currentPos.y >= 0 && currentPos.x < 30 && currentPos.y < levelMap.GetLength(0) - 2)
        {
            //Debug.Log(levelMap[(int)currentPos.x]);
            //Debug.Log(levelMap[(int)currentPos.y]);

            if (levelMap[(int)currentPos.x, (int)currentPos.y] == 5 || levelMap[(int)currentPos.x, (int)currentPos.y] == 6)// || levelMap[(int)currentPos.x, (int)currentPos.y] == 0)
            {
                //Debug.Log("setting first clip");
                // Put the audio play codes into the section for detecting eating pallet
                if (!pacAudioSource.isPlaying)
                {
                    pacAudioSource.clip = eatClip;
                    pacAudioSource.Play();
                }
                //
                return true;
            }
            else if (levelMap[(int)currentPos.x, (int)currentPos.y] == 0)
            {
                //Debug.Log("setting second clip");
                // Put the audio play codes into the section for detecting eating pallet
                if (!pacAudioSource.isPlaying)
                {
                    pacAudioSource.clip = normalClip;
                    pacAudioSource.Play();
                }
                // 
                return true;
            }
            /*
            else if (levelMap[(int)currentPos.x, (int)currentPos.y] == 6)
            {
                return true;
            }
            else if (levelMap[(int)currentPos.x, (int)currentPos.y] == 0)
            {
                return true;
            }
            */
        }
        return false;
    }// end of checkMovement(Vector2 currentPos)

    private bool checkMovementByDigit(int input)
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
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x - 1, currentPos.y);// goes up in array, so x - 1
                                                                                 //Debug.Log("Can move up");
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
                        SetMovementDirection(input);
                        currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                        //Debug.Log("Can move left");
                        return true;
                    }
                    break;
                default:
                    //Debug.Log("returning false on default");
                    return false;
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

    private string printInput(int input)
    {
        switch (input)
        {
            case 1:
                //Debug.Log("Pressed: W");
                return "W";
                //break;
            //break;
            case 2:
                //Debug.Log("Pressed: D");
                return "D";
                //break;
            //break;
            case 3:
                //Debug.Log("Pressed: S");
                return "S";
                //break;
            //break;
            case 4:
                //Debug.Log("Pressed: A");
                return "A"; 
                //break;
            //break;
            default:
                return "None";
                //break;
        }
    }

    public void SetMovementDirection(int moveState)// consider change to switch(moveState)
    {// smaller move time value means faster speed. 
        if (activeTween == null)
        {
            Debug.Log("Setting direction");
            if (moveState == 1) // move up, positive y direction
            {
                //activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y + 1, 0f), Time.time, 0.5f);
                activeTween = new Tween(pac.transform, pac.transform.position, new Vector3(pac.transform.position.x, pac.transform.position.y + 1, 0f), Time.time, 0.3f);
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


    // Above are the codes from PacMovement



}
