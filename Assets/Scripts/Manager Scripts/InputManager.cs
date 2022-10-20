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
    public int testVariable;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        //gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {


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





}
