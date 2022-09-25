using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioSource introBGMSource;
    public AudioClip normalBGMClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Switch the clip in the introBGM AudioSource
        // The new clip is the BGM when ghost is in normal state
        if (!introBGMSource.isPlaying)
        {
            introBGMSource.clip = normalBGMClip;
            introBGMSource.loop = true;
            introBGMSource.playOnAwake = false;
            introBGMSource.Play();
        }
    }
}
