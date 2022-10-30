using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManager : MonoBehaviour
{

    public int Lives = 3;
    public int Score = 0;
    public int palletNum = 2;// 224
    //public string collisionTag;
    public PacStudentController pacCtrl;

    //public AudioSource
    //public CherryController cherryCtrl;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(palletNum);
        //if (palletNum == 0 || Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Time.timeScale = 0f;
        //}
        //else if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Time.timeScale = 1f;
        //}
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
            //collisionTag = other.gameObject.tag;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "PowerPallet")
        {
            //Debug.Log("power");
            Score += 10;
            palletNum--;
            //collisionTag = other.gameObject.tag;
            pacCtrl.enablePowerPallet();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "CherryPallet")
        {
            //Debug.Log("cherry");
            Score += 100;
            //cherryCtrl.clearStuff();
            //collisionTag = other.gameObject.tag;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Ghost")
        {
            //Debug.Log("die");
            pacCtrl.checkGhostCollision();
            //collisionTag = other.gameObject.tag;
        }
        else if (other.gameObject.tag == "LeftTP")
        {
            Debug.Log("LeftTP");
            //collisionTag = other.gameObject.tag;
        }
        else if (other.gameObject.tag == "RightTP")
        {
            Debug.Log("RightTP");
            //collisionTag = other.gameObject.tag;
        }

    }

}
