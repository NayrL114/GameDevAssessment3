using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{

    public enum GhostState
    {
        Normal,
        Scared,
        Recovering,
        Dead,
    }
    private static GhostState currentGhostState = GhostState.Normal;
    public static GhostState CurrentGhostState
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

    public Animator ghostAnimator;
    public AudioSource bgmAudioSource;
    public AudioClip normalClip;
    public AudioClip scaredClip;
    public AudioClip deadClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bgmAudioSource != null && ghostAnimator != null)
        {
            if (currentGhostState == GhostState.Scared)
            {
                bgmAudioSource.clip = scaredClip;
                ghostAnimator.Play("GhostOneScared");
                //bgmAudioSource.Play();
            }
            else if (currentGhostState == GhostState.Dead)
            {
                bgmAudioSource.clip = deadClip;
                ghostAnimator.Play("GhostOneDeath");
                //bgmAudioSource.Play();
            }
            else if (currentGhostState == GhostState.Recovering)
            {
                //bgmAudioSource.clip = deadClip;
                ghostAnimator.Play("GhostOneRecovering");
                //bgmAudioSource.Play();
            }
            else if (currentGhostState == GhostState.Normal)
            {
                bgmAudioSource.clip = normalClip;
                ghostAnimator.Play("GhostOne");
                //bgmAudioSource.Play();
            }

            if (!bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Play();
            }
        }

        
        

    }

    
}
