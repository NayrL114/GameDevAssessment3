using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManager : MonoBehaviour
{

    public int Lives = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage()
    {
        Lives--;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "NormalPallet") //other.gameObject.CompareTag("PowerPallet")
        {
            Debug.Log("normal");
        }
        else if (other.gameObject.tag == "PowerPallet")
        {
            Debug.Log("power");
        }
        else if (other.gameObject.tag == "CherryPallet")
        {
            Debug.Log("cherry");
        }
        else if (other.gameObject.tag == "Ghost")
        {
            Debug.Log("die");
        }

    }

}
