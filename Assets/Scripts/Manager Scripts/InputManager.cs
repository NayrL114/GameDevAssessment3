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

    // lastInput and currentInput as required by assessment specification
    public int lastInput;
    public int currentInput;

    // References to other scripts
    public GameManager gameManager;
    public PacMovement pacMovement;// should be initiased through UIManager, when clicking onto level 1. 

    // Change the clip when pac eats something. 
    public AudioSource pacAudioSource;
    public AudioClip normalClip;
    public AudioClip eatClip;

    //public AudioSource normalSource;
    //public AudioSource eatSource;

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
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,4,0,3,3,5,1,2,2,2,2,2},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
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
        DontDestroyOnLoad(this);
        //gameManager = gameObject.GetComponent<GameManager>();
        //Debug.Log("Length of array is " + levelMap.Length + " " + levelMap.GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        if (pacMovement != null) // making sure that these codes are not called in main menu
        {
            //currentInput = lastInput;
            // If users provide input
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

            //Debug.Log("lastInput is " + printInput(lastInput) + " and currentInput is " + printInput(currentInput));

            //currentInput = lastInput;
            //bool moveParameter = checkMovementByDigit(lastInput);
            //Debug.Log(moveParameter);
            //if (moveParameter == false)
            if (checkMovementByDigit(lastInput) == false) //&& pacMovement.activeTween == null)
            {
                checkMovementByDigit(currentInput);
            }
            else
            {
                currentInput = lastInput;
            }
            //Debug.Log(currentInputDirection);
            //checkMovement(currentPos);

            /*
            if (checkMovement(new Vector2(currentPos.x, currentPos.y - 1))) // && pacMovement.activeTween == null)
            {
                //pacMovement.SetMovementDirection(lastInput);
                currentInput = lastInput;
                //currentPos = new Vector2(currentPos.x, currentPos.y - 1);
            }
            */

            // If pac is not lerping
            /*
            if (pacMovement.activeTween == null)
            {
                if (!checkMovementByDigit(lastInput))
                {
                    checkMovementByDigit(currentInput);
                }
            */

            /*
            switch (lastInput)
            {
                case 0:
                    if (checkMovement(new Vector2(currentPos.x - 1, currentPos.y)) && pacMovement.activeTween == null)
                    {
                        currentInput = 0;
                        pacMovement.SetMovementDirection(currentInput);
                        currentPos = new Vector2(currentPos.x - 1, currentPos.y);// goes up in array, so x - 1
                    }
                    else if ()
                    {

                    }
                    break;
                case 1:
                    if (checkMovement(new Vector2(currentPos.x, currentPos.y + 1)) && pacMovement.activeTween == null)
                    {
                        currentInput = 1;
                        pacMovement.SetMovementDirection(currentInput);
                        currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                    }
                    break;
                case 2:
                    if (checkMovement(new Vector2(currentPos.x + 1, currentPos.y)) && pacMovement.activeTween == null)
                    {
                        currentInput = 2;
                        pacMovement.SetMovementDirection(currentInput);
                        currentPos = new Vector2(currentPos.x + 1, currentPos.y);
                    }
                    break;
                case 3:
                    if (checkMovement(new Vector2(currentPos.x, currentPos.y - 1)) && pacMovement.activeTween == null)
                    {
                        currentInput = 3;
                        pacMovement.SetMovementDirection(currentInput);
                        currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                    }
                    break;
            }
            */
            //}
        }// end of if (pacMovement != null)

        //Debug.Log(currentPos);
        //Debug.Log(lastInput);
        //printInput(lastInput);

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
        switch (input)
        {
            case 1:
                if (checkMovement(new Vector2(currentPos.x - 1, currentPos.y)) && pacMovement.activeTween == null)
                {
                    //currentInput = 0;
                    pacMovement.SetMovementDirection(input);
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
                if (checkMovement(new Vector2(currentPos.x, currentPos.y + 1)) && pacMovement.activeTween == null)
                {
                    //currentInput = 1;
                    pacMovement.SetMovementDirection(input);
                    currentPos = new Vector2(currentPos.x, currentPos.y + 1);
                    //Debug.Log("Can move right");
                    return true;
                }
                break;                
            case 3:
                if (checkMovement(new Vector2(currentPos.x + 1, currentPos.y)) && pacMovement.activeTween == null)
                {
                    //currentInput = 2;
                    pacMovement.SetMovementDirection(input);
                    currentPos = new Vector2(currentPos.x + 1, currentPos.y);
                    //Debug.Log("Can move down");
                    return true;
                }
                break;                
            case 4:
                if (checkMovement(new Vector2(currentPos.x, currentPos.y - 1)) && pacMovement.activeTween == null)
                {
                    //currentInput = 3;
                    pacMovement.SetMovementDirection(input);
                    currentPos = new Vector2(currentPos.x, currentPos.y - 1);
                    //Debug.Log("Can move left");
                    return true;
                }
                break;
                
        }// end of switch(input)
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
            case 2:
                //Debug.Log("Pressed: D");
                return "D";
                //break;
            case 3:
                //Debug.Log("Pressed: S");
                return "S";
                //break;
            case 4:
                //Debug.Log("Pressed: A");
                return "A";
                //break;
            default:
                return "None";
        }
    }

}
