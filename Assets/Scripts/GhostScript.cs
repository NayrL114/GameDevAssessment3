using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{
    public enum GhostState
    {
        Normal,
        Scared,
        Recovering,
        Dead,
    }
    private static GhostState currentGhostState = GhostState.Normal;
    public GhostState CurrentGhostState
    {
        get
        {
            return currentGhostState;
        }
        set
        {
            currentGhostState = value;
        }
    }

    public bool isDead = false;
    public float deadTimer;
    public Animator ghostAnimator;

    public int ghostID;

    // Start is called before the first frame update
    void Start()
    {
        ghostAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deadTimer += Time.deltaTime;
            //Debug.Log(deadTimer);
            if (deadTimer >= 5)
            {
                currentGhostState = GhostState.Normal;
                isDead = false;
                deadTimer = 0;
            }

        }
        playAnimation();
    }

    public void playAnimation()
    {
        if (currentGhostState == GhostState.Scared)
        {
            switch (ghostID)
            {
                case 1:
                    ghostAnimator.Play("GhostOneScared");
                    break;
                case 2:
                    ghostAnimator.Play("GhostTwoScared");
                    break;
                case 3:
                    ghostAnimator.Play("GhostThreeScared");
                    break;
                case 4:
                    ghostAnimator.Play("GhostFourScared");
                    break;
            }
            //bgmAudioSource.clip = scaredClip;
            //ghostAnimator.Play("GhostOneScared");
            //bgmAudioSource.Play();
        }
        else if (currentGhostState == GhostState.Dead)
        {
            /*
            if (uiManager.isScared)
            {
                bgmAudioSource.clip = deadClip;
            }
            */
            switch (ghostID)
            {
                case 1:
                    ghostAnimator.Play("GhostOneDeath");
                    break;
                case 2:
                    ghostAnimator.Play("GhostTwoDeath");
                    break;
                case 3:
                    ghostAnimator.Play("GhostThreeDeath");
                    break;
                case 4:
                    ghostAnimator.Play("GhostFourDeath");
                    break;
            }
            //ghostAnimator.Play("GhostOneDeath");
            isDead = true;
            //bgmAudioSource.Play();
        }
        else if (currentGhostState == GhostState.Recovering)
        {
            //bgmAudioSource.clip = deadClip;
            switch (ghostID)
            {
                case 1:
                    ghostAnimator.Play("GhostOneRecovering");
                    break;
                case 2:
                    ghostAnimator.Play("GhostTwoRecovering");
                    break;
                case 3:
                    ghostAnimator.Play("GhostThreeRecovering");
                    break;
                case 4:
                    ghostAnimator.Play("GhostFourRecovering");
                    break;
            }
            //ghostAnimator.Play("GhostOneRecovering");
            //bgmAudioSource.Play();
        }
        else if (currentGhostState == GhostState.Normal)
        {
            //bgmAudioSource.clip = normalClip;
            switch (ghostID)
            {
                case 1:
                    ghostAnimator.Play("GhostOne");
                    break;
                case 2:
                    ghostAnimator.Play("GhostTwoLeft");
                    break;
                case 3:
                    ghostAnimator.Play("GhostThreeLeft");
                    break;
                case 4:
                    ghostAnimator.Play("GhostFourLeft");
                    break;
            }
            //ghostAnimator.Play("GhostOne");
            //bgmAudioSource.Play();
        }
    }
}
