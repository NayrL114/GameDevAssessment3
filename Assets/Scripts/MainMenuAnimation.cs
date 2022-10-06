using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{

    public Animator mainPacStudentAnimator;

    // Start is called before the first frame update
    void Start()
    {
        //mainPacStudentAnimator.Play("PacStudentWalkingLeft");
    }

    // Update is called once per frame
    void Update()
    {
        mainPacStudentAnimator.Play("PacStudentWalkingLeft");
    }
}
