using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    /*
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
    */

    public Animator ghostAnimator;
    public AudioSource bgmAudioSource;
    public AudioClip normalClip;
    public AudioClip scaredClip;
    public AudioClip deadClip;    
    public float ghostTimer = 0;

    public UIManager uiManager;

    public bool hasScared = false;
    public bool hasDead = false;
    public bool hasNormal = false;
    public bool setScared = false;
    public bool setRecovered = false;
    public bool setNormal = false;

    [SerializeField] public GameObject[] ghosts = new GameObject[4];
    public GhostScript[] ghostScripts = new GhostScript[4];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       

        if (uiManager.isScared)
        {
            ghostTimer += Time.deltaTime;

            if (!setScared)
            {
                setAllScaredState();
                setScared = true;
            }

            if (ghostTimer >= 7 && !setRecovered)
            {
                setRecoveringState();
                setRecovered = true;
            }

            if (ghostTimer >= 10 && !setNormal)
            {
                setNormalState();
                setNormal = true;
            }
        }
        else
        {
            ghostTimer = 0;
            setScared = false;
            setRecovered = false;
            setNormal = false;
        }

        //if (bgmAudioSource != null && ghostAnimator != null)
        if (bgmAudioSource != null && ghosts.Length != 0) // checking all ghost state and set BGM
        {
            foreach (GameObject ghost in ghosts)
            {
                if (ghost.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Scared)
                {
                    //bgmAudioSource.clip = scaredClip;
                    hasScared = true;
                }
                else if (ghost.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Dead)
                {
                    if (uiManager.isScared)
                    {
                        //bgmAudioSource.clip = deadClip;
                        hasDead = true;
                    }
                }
                else if (ghost.GetComponent<GhostScript>().CurrentGhostState == GhostScript.GhostState.Normal)
                {
                    //bgmAudioSource.clip = normalClip;
                    hasNormal = true;
                }
            }

            if (hasDead)
            {
                bgmAudioSource.clip = deadClip;
            }
            else if (hasScared)
            {
                bgmAudioSource.clip = scaredClip;
            }
            else if (hasNormal)
            {
                bgmAudioSource.clip = normalClip;
            }

            if (!bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Play();
            }

            hasDead = false;
            hasScared = false;
            hasNormal = false;

        }
                     

    }// end of Update()

    public void startScriptArray()
    {
        //ghostScripts = GameObject.FindGameObjectsWithTag("Ghost");
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghostScripts[i] = ghosts[i].GetComponent<GhostScript>();
        }
    }

    public void setAllScaredState()
    {
        foreach (GameObject ghost in ghosts)
        {            
            ghost.GetComponent<GhostScript>().CurrentGhostState = GhostScript.GhostState.Scared;
        }
    }

    public void setRecoveringState()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript gScript = ghost.GetComponent<GhostScript>();
            if (!(gScript.CurrentGhostState == GhostScript.GhostState.Dead))
            {
                gScript.CurrentGhostState = GhostScript.GhostState.Recovering;
            }
        }
    }

    public void setNormalState()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript gScript = ghost.GetComponent<GhostScript>();
            if (!(gScript.CurrentGhostState == GhostScript.GhostState.Dead))
            {
                gScript.CurrentGhostState = GhostScript.GhostState.Normal;
            }
        }
    }

    public void setAllNormalState()
    {
        foreach (GameObject ghost in ghosts)
        {
            GhostScript gScript = ghost.GetComponent<GhostScript>();
            gScript.CurrentGhostState = GhostScript.GhostState.Normal;
        }
    }

    //public void setDeadToAGhost(GameObject ghosty)
    public void setDeadToAGhost(int ID)
    {
        foreach (GameObject ghost in ghosts)
        {
            //if (ghost.GetComponent<GhostScript>().ghostID == ghosty.GetComponent<GhostScript>().ghostID)
            if (ghost.GetComponent<GhostScript>().ghostID == ID)
            {
                ghost.GetComponent<GhostScript>().CurrentGhostState = GhostScript.GhostState.Dead;
                return;
            }
        }
    }

}// end of everything
