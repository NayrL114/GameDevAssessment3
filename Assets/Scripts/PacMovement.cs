using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{

    public Animator pacAnimator;
    public Tween activeTween;
    //public bool isMoving;

    /*
    private int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, 
    };
    //private Vector3 pacCurrentPos = new Vector3(-21.5f, 11.5f, 0f);// should be top left
    private Vector2 pacCurrentPos = new Vector2(0f, 0f);// using this Vector2 to store the position of levelMap 2D array
    */

    // Start is called before the first frame update
    void Start()
    {
        //SetMovementDirection(1);
    }

    // Update is called once per frame
    void Update()
    {
        //SetMovementDirection(1);
        
        if (activeTween != null)
        {
            float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);


            // Start playing movement audio. 
            //if (!moveAudioSource.isPlaying)
            //{
            //moveAudioSource.loop = true;
            //    moveAudioSource.Play();
            //}

            if (Vector3.Distance(transform.position, activeTween.EndPos) <= 0.01f)
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
            //moveAudioSource.Pause();

        }
        
    }// end of Update()
        
    public void SetMovementDirection(int moveState)// consider change to switch(moveState)
    {// Set the move coordinates. 
        if (activeTween == null){
            if (moveState == 0) // move up, positive y direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y + 1, 0f), Time.time, 0.5f);
                pacAnimator.Play("WalkingUp");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveState == 1) // move right, positive x direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x + 1, transform.position.y, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingRight");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (moveState == 2) // move down, negative y direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y - 1, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingDown");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 180);
            }
            else if (moveState == 3) // move left, negative x direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x - 1, transform.position.y, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingLeft");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 270);
            }
        }
        
    }// end of SetMovementDirection(int moveState)

}// end of PacMovement.cs
