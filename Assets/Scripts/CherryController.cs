using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{

    public GameObject cherry;
    public GameObject cherryObject;    

    public int score = 10;
    private bool isSpawning;
    public float spawnTimer = 0f;    
    public Tween activeTween;

    //public Vector3 centre = new Vector3(-9f, -2f, 0f);// 45.5 // ADD 25? Add Y if going up, add X if going right, and magnitude is 17.5
    public Vector3 centre = new Vector3(0f, 0f, 0f);// 45.5 // ADD 25? Add Y if going up, add X if going right, and magnitude is 17.5

    /*public Vector3[,] spawnPoints = {
        {new Vector3(-9f, 23f, 0f), new Vector3(-9f, -27f, 0f)}, // N to S
        {new Vector3(-9f, -27f, 0f), new Vector3(-9f, 23f, 0f)}, // S to N
        {new Vector3(-38f, -2f, 0f), new Vector3(20f, -2f, 0f)}, // W to E, slighly increased X so cherry will spawn outside camera, was 4 less
        {new Vector3(20f, -2f, 0f), new Vector3(-38f, -2f, 0f)}, // E to W, slighly increased X so cherry will spawn outside camera, was 4 less
        {new Vector3(8.5f, 15.5f, 0f), new Vector3(-26.5f, -19.5f, 0f)}, // NE to SW
        {new Vector3(-23.5f, 15.5f, 0f), new Vector3(4.5f, -19.5f, 0f)}, // NW to SE, slighly decreased X so cherry will go through centre, was 4 more
        {new Vector3(-26.5f, -19.5f, 0f), new Vector3(8.5f, 15.5f, 0f)}, // SW to NE
        {new Vector3(4.5f, -19.5f, 0f), new Vector3(-23.5f, 15.5f, 0f)}, // SE to NW, slighly decreased X so cherry will go through centre, was 4 more
        // All coordinates are calculated based on new Vector3(-9f, -2f, 0f), which roughly forms a circle.
    };*/

    // Start is called before the first frame update
    void Start()
    {
        //centre 
        //isSpawning = false;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(activeTween == null);
        //Debug.Log("deltaTime is" + Time.deltaTime);

        // spawning cherry every 10 seconds
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= 3f)// 10f
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
                spawnCor = new Vector3(Random.Range(20f, 21f), Random.Range(-20f - 9f, 15f - 2f), 0f);// roughly the righter area
                //spawnCor = spawnPoints[0, 0];// N to S
                break;
            case 2:
                spawnCor = new Vector3(Random.Range(-38f, 21f), Random.Range(15f - 9f, 16f - 2f), 0f);// roughly the upper area
                //spawnCor = spawnPoints[1, 0];// N to S
                break;
            case 3:
                spawnCor = new Vector3(Random.Range(-39f, -38f), Random.Range(-20f - 9f, 15f - 2f), 0f);// roughly the lefter area
                //spawnCor = spawnPoints[2, 0];// N to S
                break;
            case 4:
                spawnCor = new Vector3(Random.Range(-38f, 21f), Random.Range(-21f - 9f, -20f - 2f), 0f);// roughly the down area
                //spawnCor = spawnPoints[3, 0];// N to S
                break;
            default:
                spawnCor = new Vector3(-9f, -2f, 0f);// centre of level
                break;
        }

        //Cherry bin = new Cherry(cherry.transform, spawnCor, new Vector3(-spawnCor.x, -spawnCor.y, 0f));

        //cherryObject = (GameObject)Instantiate(cherry, spawnPoints[areaNum, 0], Quaternion.identity);
        //moveCherry(cherryObject.transform, /*spawnCor*/spawnPoints[areaNum, 0], spawnPoints[areaNum, 1]);

        //cherryObject = (GameObject)Instantiate(cherry, spawnCor, Quaternion.identity);
        //moveCherry(cherryObject.transform, /*spawnCor*/spawnCor, spawnPoints[areaNum, 1]);

        //Invoke("destroyCherry", 9f);

        Debug.Log(spawnCor);
        Debug.DrawLine(spawnCor, centre, Color.white, 2f);// 9f
        Debug.DrawLine(centre, new Vector3(-spawnCor.x, -spawnCor.y, 0f), Color.white, 2f);

        //bin.moveCherry();
        //isSpawning = false;

        //Vector3 spawnCor = new Vector3(Random.Range(), Random.Range(), 0f)
    }
        
    public void destroyCherry()
    {
        Destroy(cherryObject);
        activeTween = null;
        
        //Debug.Log("cherry should be destroyed now");
    }    

    public void clearStuff()
    {
        spawnTimer = 0f;
        //activeTween = null;
        CancelInvoke("destroyCherry");
        destroyCherry();
    }    

    public void moveCherry(Transform transform, Vector3 startPoint, Vector3 endPoint)
    {
        //activeTween = new Tween(gameObject, startPoint, endPoint, Time.time, 3f);
        activeTween = new Tween(transform, startPoint, endPoint, Time.time, 8f);
    }

    
}
