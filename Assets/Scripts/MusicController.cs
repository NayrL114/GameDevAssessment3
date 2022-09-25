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
        if (!introBGMSource.isPlaying)
        {
            introBGMSource.clip = normalBGMClip;
            introBGMSource.loop = true;
            introBGMSource.playOnAwake = false;
            introBGMSource.Play();
        }
    }
}
