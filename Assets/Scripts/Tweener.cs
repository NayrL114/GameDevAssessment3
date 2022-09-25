using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{

    // This Tweener script is taken from and modified based on the Tweener script in week 7 tutorial exercise

    //private Tween activeTween;
    //private List<Tween> activeTweens;
    //private float timer;
    public Transform pacStudent;
    public AudioSource moveAudioSource;
    public Animator pacStudentAnimator;
    private Tween activeTween;
    private int moveState;

    // Start is called before the first frame update
    void Start()
    {
        // Tween constructor: Tween(Transform target, Vector3 startPos, Vector3 endPos, float startTime, float duration)
        //activeTween = new Tween(pacStudent, new Vector3(-21.5f, 7.5f, 0f), new Vector3(-21.5f, 11.5f, 0f), Time.time, 2f);
        moveState = 0;
        SetMovementDirection(moveState);
        moveAudioSource.loop = true;
        moveAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // (-21.5, 7.5) -> (-21.5, 11.5) -> (-16.5, 11.5) -> (-16.5, 7.5)
        //Debug.Log(activeTweens.Count);
        //if (activeTween != null)
        if (activeTween != null)
        {            
            float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);
            /*
            if (!moveAudioSource.isPlaying)
            {
                moveAudioSource.loop = true;
                moveAudioSource.Play();
            }
            */
        }

        if (Vector3.Distance(pacStudent.position, activeTween.EndPos) <= 0.1f)
        {
            moveState++;
            if (moveState >= 4)
            {
                moveState = 0;
            }
            SetMovementDirection(moveState);
        }

    }

    /*
    private bool CheckCloseDistance(Transform targetObject, Tween activeTween)
    {
        return Vector3.Distance(targetObject.position, activeTween.EndPos) <= 0.1f;
    }
    */
    
    private void SetMovementDirection(int moveState)
    {
        if (moveState == 0) // move up, (-21.5, 7.5) -> (-21.5, 11.5)
        {
            activeTween = new Tween(pacStudent, new Vector3(-21.5f, 7.5f, 0f), new Vector3(-21.5f, 11.5f, 0f), Time.time, 2f);
        }
        else if (moveState == 1) // move right, (-21.5, 11.5) -> (-16.5, 11.5)
        {
            activeTween = new Tween(pacStudent, new Vector3(-21.5f, 11.5f, 0f), new Vector3(-16.5f, 11.5f, 0f), Time.time, 2f);
        }
        else if (moveState == 2) // move down, (-16.5, 11.5) -> (-16.5, 7.5)
        {
            activeTween = new Tween(pacStudent, new Vector3(-16.5f, 11.5f, 0f), new Vector3(-16.5f, 7.5f, 0f), Time.time, 2f);
        }
        else if (moveState == 3) // move left, (-16.5, 7.5) -> (-21.5, 7.5)
        {
            activeTween = new Tween(pacStudent, new Vector3(-16.5f, 7.5f, 0f), new Vector3(-21.5f, 7.5f, 0f), Time.time, 2f);
        }
    }
    

    

}
