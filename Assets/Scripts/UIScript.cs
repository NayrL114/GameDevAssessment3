using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    public Text GhostStateText;
    public Text PacStateText;
    public float timer;

    private string[] ghostStateStrings = { "Walking Left", "Walking Right", "Walking Up", "Walking Down", "Scared", "Recovering", "Death" };
    private string[] pacStatesStrings = {"Walking Up",
        "Death Facing Up", "Walking Right", "Death Facing Right", "Walking Down", "Death Facing Down", "Walking Left", "Death Facing Left"};
    private int ghostArrayIter;
    private int pacArrayIter;
        
    // Start is called before the first frame update
    void Start()
    {
        ghostArrayIter = 0;
        pacArrayIter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer % 3 == 0)
        {

        }
    }
}
