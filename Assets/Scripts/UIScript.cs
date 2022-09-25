using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public Text GhostStateText;
    public Text PacStateText;

    private float ghostTimer = 0;
    private float pacTimer = 0;
    private string[] ghostStateStrings = { "Walking Left", "Walking Right", "Walking Up", "Walking Down", "Scared", "Recovering", "Death" };
    private string[] pacStatesStrings = {"Walking & Death Facing Up",
        "Walking & Death Facing Right", "Walking & Death Facing Down", "Walking Left & Death Facing Left", };
    private int ghostArrayIter = 0;
    private int pacArrayIter = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        GhostStateText.text = ghostStateStrings[ghostArrayIter];
        PacStateText.text = pacStatesStrings[pacArrayIter];

    }

    // Update is called once per frame
    void Update()
    {
        ghostTimer += Time.deltaTime;
        pacTimer += Time.deltaTime;
        //Debug.Log(timer);

        if (ghostTimer >= 3)
        {
            ghostArrayIter++;
            GhostStateText.text = ghostStateStrings[ghostArrayIter];
            ghostTimer = 0;

        }

        if (pacTimer >= 5.08f)
        {
            pacArrayIter++;
            PacStateText.text = pacStatesStrings[pacArrayIter];
            pacTimer = 0;

        }

        if (ghostArrayIter >= 6) ghostArrayIter = -1;

        if (pacArrayIter >= 3) pacArrayIter = -1;

        /*
        if (timer % 5 == 0)
        {
            pacArrayIter++;
            PacStateText.text = pacStatesStrings[pacArrayIter];

        }
        */

    }

    /* 
     * I reckon this might be a nice place for some audio references :p
     *
     *  PacMove: 566303__crazybeatsinc__pop-sound-effect.wav - https://freesound.org/people/CrazyBeatsINC/sounds/566303/

        EatPallet: 610026__cc0-sound__04-mt-trx-xt.wav - https://freesound.org/people/CC0_Sound/sounds/610026/

        PacDeath: 207321__patricklieberkind__machine-shutdown.wav https://freesound.org/people/PatrickLieberkind/sounds/207321/

        WallHit: 13946__adcbicycle__18.wav - https://freesound.org/people/adcbicycle/sounds/13946/

        IntroMusic: 326553__shadydave__the-sonata-piano-loop.mp3 - https://freesound.org/people/ShadyDave/sounds/326553/

        NormalGhostMusic: 623616__davejf__melody-loop-107-bpm.mp3 - https://freesound.org/people/DaveJf/sounds/623616/

        ScaredMusic: 256218__zagi2__guitar-riff - https://freesound.org/people/zagi2/sounds/256218/

        DeadMusic: 501079__zagi2__ambush.wav - https://freesound.org/people/zagi2/sounds/501079/


    */

       
}
