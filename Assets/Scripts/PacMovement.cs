using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{

    // This PacMovement script is modified based on the Tweener script from weekly lab activities. 

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
        //pacSpRend = gameObject.GetComponent<SpriteRenderer>();
        pacAudioSource = gameObject.GetComponent<AudioSource>();
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
                dustParticle.Play();
            }

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
            pacAudioSource.Stop();
            dustParticle.Stop();
            //dustParticle.SetActive(false);
            //pacAnimator.Play("PacStudentIdle");
            //pacSpRend.sprite = pacIdleSprite;
            switch (localMoveState)
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
        
    }// end of Update()
        
    public void SetMovementDirection(int moveState)// consider change to switch(moveState)
    {// smaller move time value means faster speed. 
        if (activeTween == null){
            if (moveState == 1) // move up, positive y direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y + 1, 0f), Time.time, 0.5f);
                pacAnimator.Play("WalkingUp");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 0);
                localMoveState = moveState;
            }
            else if (moveState == 2) // move right, positive x direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x + 1, transform.position.y, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingRight");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
                localMoveState = moveState;
            }
            else if (moveState == 3) // move down, negative y direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x, transform.position.y - 1, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingDown");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 180);
                localMoveState = moveState;
            }
            else if (moveState == 4) // move left, negative x direction
            {
                activeTween = new Tween(transform, transform.position, new Vector3(transform.position.x - 1, transform.position.y, 0f), Time.time, 0.5f);
                pacAnimator.Play("PacStudentWalkingLeft");//, -1, 0f);
                transform.rotation = Quaternion.Euler(0, 0, 270);
                localMoveState = moveState;
            }
            
        }
        
    }// end of SetMovementDirection(int moveState)

}// end of PacMovement.cs
