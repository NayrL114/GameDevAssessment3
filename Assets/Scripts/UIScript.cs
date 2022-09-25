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
       
}
