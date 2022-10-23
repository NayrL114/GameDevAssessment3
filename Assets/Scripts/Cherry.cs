using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry// : MonoBehaviour
{

    public int score = 10;
    //public Tween activeTween;
    public Transform cherryTransform;
    public Vector3 startCor;
    public Vector3 endCor;

    public Cherry(Transform transform, Vector3 startcor, Vector3 endcor)
    {
        cherryTransform = transform;
        startCor = startcor;
        endCor = endcor;
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("destroyCherry", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("destroyCherry", 3f);// can be changed to 15f

        if (activeTween != null)
        {
            float fractionTime = (Time.time - activeTween.StartTime) / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, fractionTime);
        }
        
    }

    public void moveCherry(/*Vector3 startPoint, Vector3 endPoint)
    {
        //activeTween = new Tween(gameObject, startPoint, endPoint, Time.time, 3f);
        activeTween = new Tween(transform, startCor, endCor, Time.time, 3f);
    }

    private void destroyCherry()
    {
        Destroy(gameObject);
    }
    */
}
