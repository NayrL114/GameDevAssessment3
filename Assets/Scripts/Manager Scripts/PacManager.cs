using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManager : MonoBehaviour
{

    public int Lives = 3;
    public int Score = 0;
    public int palletNum = 224;
    //public CherryController cherryCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(palletNum);
        if (palletNum == 0 || Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
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
            //Debug.Log("normal");
            Score += 10;
            palletNum--;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "PowerPallet")
        {
            //Debug.Log("power");
            Score += 10;
            palletNum--;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "CherryPallet")
        {
            //Debug.Log("cherry");
            Score += 100;
            //cherryCtrl.clearStuff();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Ghost")
        {
            Debug.Log("die");
        }
        else if (other.gameObject.tag == "LeftTP")
        {
            Debug.Log("LeftTP");
        }
        else if (other.gameObject.tag == "RightTP")
        {
            Debug.Log("RightTP");
        }

    }

}
