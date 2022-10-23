using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherry;
    public GameObject cherryObject;
    private Vector3 centre;

    public int score = 10;
    private bool isSpawning;
    private float spawnTimer;
    //public Vector2[] spawnPoints;
    public Tween activeTween;
    
    // Start is called before the first frame update
    void Start()
    {
        centre = new Vector3(-9f, -2f, 0f);
        //isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        // spawning cherry every 10 seconds
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 10f)
        {
            spawnCherry();
            spawnTimer = 0;
        }

        if (activeTween != null)
        {
            float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);

            if (Vector3.Distance(cherryObject.transform.position, activeTween.EndPos) <= 0.01f)
            {
                activeTween.Target.position = activeTween.EndPos;
                activeTween = null;
                
            }
        }
        /*
        if (!isSpawning)
        {
            isSpawning = true;
            Invoke("spawnCherry", 2.0f);
        }        
        */
        //Invoke("destroyCherry", 2f);
    }

    private void spawnCherry()
    {
        int areaNum = Random.Range(1, 5);
        Vector3 spawnCor;
        switch (areaNum)
        {
            case 1:
                spawnCor = new Vector3(Random.Range(20f, 21f), Random.Range(-20f, 15f), 0f);// roughly the righter area
                break;
            case 2:
                spawnCor = new Vector3(Random.Range(-38f, 21f), Random.Range(15f, 16f), 0f);// roughly the upper area
                break;
            case 3:
                spawnCor = new Vector3(Random.Range(-39f, -38f), Random.Range(-20f, 15f), 0f);// roughly the lefter area
                break;
            case 4:
                spawnCor = new Vector3(Random.Range(-38f, 21f), Random.Range(-21f, -20f), 0f);// roughly the down area
                break;
            default:
                spawnCor = new Vector3(-9f, -2f, 0f);// centre of level
                break;
        }

        //Cherry bin = new Cherry(cherry.transform, spawnCor, new Vector3(-spawnCor.x, -spawnCor.y, 0f));
        cherryObject = (GameObject)Instantiate(cherry, spawnCor, Quaternion.identity);
        moveCherry(cherryObject.transform, spawnCor, new Vector3(-spawnCor.x, -spawnCor.y, 0f));
        Invoke("destroyCherry", 9f);
        //bin.moveCherry();
        //isSpawning = false;

        //Vector3 spawnCor = new Vector3(Random.Range(), Random.Range(), 0f)
    }

    
    private void destroyCherry()
    {
        Destroy(cherryObject);
        //Debug.Log("cherry should be destroyed now");
    }
    

    public void moveCherry(Transform transform, Vector3 startPoint, Vector3 endPoint)
    {
        //activeTween = new Tween(gameObject, startPoint, endPoint, Time.time, 3f);
        activeTween = new Tween(transform, startPoint, endPoint, Time.time, 3f);
    }

    
}
