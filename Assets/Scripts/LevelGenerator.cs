using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    // Automatic Level Generation is not finished. 

    // For A4, this script is modified to automaticly layout the pallets, while the walls in map design are kept as manual tilemap layout. 

    public GameObject ManualLayoutMap;
    public GameObject outsideCorner;// ID 1 Sprite, outside corner
    public GameObject outsideWall;// ID 2 Sprite, outside wall
    public GameObject insideCorner;// ID 3 Sprite, inside corner
    public GameObject insideWall;// ID 4 Sprite, inside wall
    public GameObject standardPallet;// ID 5 Sprite, normal pallet
    public GameObject powerPallet;// ID 6 Sprite, power pallet
    public GameObject outsideTJuuction;// ID 7 Sprite, outside T junction

    private Vector3 wallScale = new Vector3(0.5f, 0.5f, 0f);
    private Vector3 palletScale = new Vector3(0.4f, 0.4f, 0f);

    private Vector3 printCoordinate = new Vector3(-22.5f, 12.5f, 0f);

    private Vector3 topLeftInitial = new Vector3(-22.5f, 12.5f, 0f);
    private Vector3 topRightInitial = new Vector3(-8.5f, 12.5f, 0f);
    private Vector3 bottomLeftInitial = new Vector3(-22.5f, -2.5f, 0f);// (top right corner of bottom left quadrant is (-9.5, -2.5)
    private Vector3 bottomRightInitial = new Vector3(-8.5f, -2.5f, 0f);

    public int[,] levelMap = {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0}, // This row was disabled back in A3 for meeting the requirement of automatic level generation
    };

    void Awake()
    {
        // Disabling manual layout at the start of play
        //ManualLayoutMap.SetActive(false);

        Debug.Log("Printing top-left quadrant");
        printCoordinate = topLeftInitial;
        TopLeftQuadrant();
        Debug.Log("Printing top-right quadrant");
        printCoordinate = topRightInitial;
        TopRightQuadrant();
        Debug.Log("Printing bottom-right quadrant");
        printCoordinate = bottomRightInitial;
        BottomRightQuadrant();
        Debug.Log("Printing bottom-left quadrant");
        printCoordinate = bottomLeftInitial;
        BottomLeftQuadrant();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TopLeftQuadrant()
    {// POSITIVE x direction, POSITIVE y direction
        for (int x = 0; x < levelMap.GetLength(0); x++)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++)
            {
                //RotateSprite(levelMap[x, y], x, y)
                DrawSprite(levelMap[x, y], RotateSprite(levelMap[x, y], x, y));
                //Debug.Log("Position in multidimentional array at [" + x + ", " + y + "] is " + levelMap[x, y]);
                printCoordinate.x += 1;
            }
            printCoordinate.x = topLeftInitial.x;
            printCoordinate.y -= 1;
        }
    }

    private void TopRightQuadrant()
    {// POSITIVE x direction, NEGATIVE y direction
        for (int x = 0; x < levelMap.GetLength(0); x++)
        {
            for (int y = levelMap.GetLength(1) - 1; y >= 0; y--)
            {
                //DrawSprite(levelMap[x, y], 0f);
                DrawSprite(levelMap[x, y], RotateSprite(levelMap[x, y], x, y));
                //Debug.Log("Position in multidimentional array at [" + x + ", " + y + "] is " + levelMap[x, y]);




                printCoordinate.x += 1;
            }
            printCoordinate.x = topRightInitial.x;
            printCoordinate.y -= 1;
        }
    }

    private void BottomRightQuadrant()
    {// NEGATIVE x direction, NEGATIVE y direction
        for (int x = levelMap.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = levelMap.GetLength(1) - 1; y >= 0; y--)
            {
                //DrawSprite(levelMap[x, y], 0f);
                DrawSprite(levelMap[x, y], RotateSprite(levelMap[x, y], x, y));
                //Debug.Log("Position in multidimentional array at [" + x + ", " + y + "] is " + levelMap[x, y]);
                printCoordinate.x += 1;
            }
            printCoordinate.x = bottomRightInitial.x;
            printCoordinate.y -= 1;
        }
    }

    private void BottomLeftQuadrant()
    {// NEGATIVE x direction, POSITIVE y direction
        for (int x = levelMap.GetLength(0) - 1; x >= 0; x--)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++)
            {
                //DrawSprite(levelMap[x, y], 0f);
                DrawSprite(levelMap[x, y], RotateSprite(levelMap[x, y], x, y));
                //Debug.Log("Position in multidimentional array at [" + x + ", " + y + "] is " + levelMap[x, y]);
                //Debug.Log(printCoordinate);
                printCoordinate.x += 1;
            }
            printCoordinate.x = bottomLeftInitial.x;
            printCoordinate.y -= 1;
        }
    }

    private float RotateSprite(int spriteID, int x, int y)
    {
        bool hasRight = false;
        bool hasLeft = false;
        bool hasAbove = false;
        bool hasBelow = false;

        if (spriteID == 1)
        {
            Debug.Log("checking spriteID 1");
            if (x - 1 >= 0)// above
            {
                Debug.Log("checking above");
                if (levelMap[x - 1, y] == 1 || levelMap[x - 1, y] == 2 || levelMap[x - 1, y] == 7)
                {
                    hasAbove = true;
                }
            }

            if (x + 1 >= 0 && x + 1 < levelMap.GetLength(0))// below
            {
                Debug.Log("checking below");
                if (levelMap[x + 1, y] == 1 || levelMap[x + 1, y] == 2 || levelMap[x - 1, y] == 7)
                {
                    hasBelow = true;
                }
            }

            if (y + 1 >= 0 && y + 1 < levelMap.GetLength(1))// right
            {
                Debug.Log("checking right");
                if (levelMap[x, y + 1] == 1 || levelMap[x, y + 1] == 2 || levelMap[x - 1, y] == 7)
                {
                    hasRight = true;
                }
            }

            if (y - 1 >= 0)// left
            {
                Debug.Log("checking left");
                if (levelMap[x, y - 1] == 1 || levelMap[x, y - 1] == 2 || levelMap[x - 1, y] == 7)
                {
                    hasLeft = true;
                }
            }

            Debug.Log(hasAbove + " " + hasBelow + " " + hasLeft + " " + hasRight);

            if (hasAbove && hasRight)
            {
                return 180f;
            }
            else if (hasAbove && hasLeft)
            {
                return 270f;
            }
            else if (hasBelow && hasRight)
            {
                return 90f;
            }
            else if (hasBelow && hasLeft)
            {
                return 0f;
            }

        }// end of spriteID 1
        else if (spriteID == 2)
        {            
            if (x - 1 >= 0)// above
            {
                //Debug.Log("checking above");
                if (levelMap[x - 1, y] == 1 || levelMap[x - 1, y] == 2)
                {
                    return 0f;
                }
            }
            
            if (x + 1 >= 0 && x + 1 < levelMap.GetLength(0))// below
            {
                //Debug.Log("checking below");
                if (levelMap[x + 1, y] == 1 || levelMap[x + 1, y] == 2)
                {
                    return 0f;
                }
            }
            
            if (y + 1 >= 0 && y + 1 < levelMap.GetLength(1))// right
            {
                //Debug.Log("checking right");
                if (levelMap[x, y + 1] == 1 || levelMap[x, y + 1] == 2)
                {
                    return 90f;
                }
            }
             
            if (y - 1 >= 0)// left
            {
                //Debug.Log("checking left");
                if (levelMap[x, y - 1] == 1 || levelMap[x, y - 1] == 2)
                {
                    return 90f;
                }
            }

        }// end of spriteID 2
        if (spriteID == 3)
        {
            Debug.Log("checking spriteID 3");
            if (x - 1 >= 0)// above
            {
                Debug.Log("checking above");
                if (levelMap[x - 1, y] == 3 || levelMap[x - 1, y] == 4)
                {
                    hasAbove = true;
                }
            }

            if (x + 1 >= 0 && x + 1 < levelMap.GetLength(0))// below
            {
                Debug.Log("checking below");
                if (levelMap[x + 1, y] == 3 || levelMap[x + 1, y] == 4)
                {
                    hasBelow = true;
                }
            }

            if (y + 1 >= 0 && y + 1 < levelMap.GetLength(1))// right
            {
                Debug.Log("checking right");
                if (levelMap[x, y + 1] == 3 || levelMap[x, y + 1] == 4)
                {
                    hasRight = true;
                }
            }

            if (y - 1 >= 0)// left
            {
                Debug.Log("checking left");
                if (levelMap[x, y - 1] == 3 || levelMap[x, y - 1] == 4)
                {
                    hasLeft = true;
                }
            }

            Debug.Log(hasAbove + " " + hasBelow + " " + hasLeft + " " + hasRight);

            if (hasAbove && hasRight)
            {
                return 180f;
            }
            else if (hasAbove && hasLeft)
            {
                return 270f;
            }
            else if (hasBelow && hasRight)
            {
                return 90f;
            }
            else if (hasBelow && hasLeft)
            {
                return 0f;
            }

        }// end of spriteID 3
        if (spriteID == 4)
        {
            Debug.Log("checking spriteID 4");
            if (x - 1 >= 0)// above
            {
                Debug.Log("checking above");
                if (levelMap[x - 1, y] == 3 || levelMap[x - 1, y] == 4)
                {
                    hasAbove = true;
                }
            }

            if (x + 1 >= 0 && x + 1 < levelMap.GetLength(0))// below
            {
                Debug.Log("checking below");
                if (levelMap[x + 1, y] == 3 || levelMap[x + 1, y] == 4)
                {
                    hasBelow = true;
                }
            }

            if (y + 1 >= 0 && y + 1 < levelMap.GetLength(1))// right
            {
                Debug.Log("checking right");
                if (levelMap[x, y + 1] == 3 || levelMap[x, y + 1] == 4)
                {
                    hasRight = true;
                }
            }

            if (y - 1 >= 0)// left
            {
                Debug.Log("checking left");
                if (levelMap[x, y - 1] == 3 || levelMap[x, y - 1] == 4)
                {
                    hasLeft = true;
                }
            }

            Debug.Log(hasAbove + " " + hasBelow + " " + hasLeft + " " + hasRight);

            if (hasLeft && hasRight && !hasAbove)
            {
                return 270f;
            }
            else if (hasLeft && hasRight && !hasBelow)
            {
                return 90f;
            }
            else if (hasBelow && hasAbove && !hasLeft)
            {
                return 0f;
            }
            else if (hasBelow && hasAbove && !hasRight)
            {
                return 180f;
            }

        }// end of spriteID 4
        if (spriteID == 7)
        {
            Debug.Log("checking spriteID 3");
            if (x - 1 >= 0)// above
            {
                Debug.Log("checking above");
                if (levelMap[x - 1, y] == 2 || levelMap[x - 1, y] == 4)
                {
                    hasAbove = true;
                }
            }

            if (x + 1 >= 0 && x + 1 < levelMap.GetLength(0))// below
            {
                Debug.Log("checking below");
                if (levelMap[x + 1, y] == 2 || levelMap[x + 1, y] == 4)
                {
                    hasBelow = true;
                }
            }

            if (y + 1 >= 0 && y + 1 < levelMap.GetLength(1))// right
            {
                Debug.Log("checking right");
                if (levelMap[x, y + 1] == 2 || levelMap[x, y + 1] == 4)
                {
                    hasRight = true;
                }
            }

            if (y - 1 >= 0)// left
            {
                Debug.Log("checking left");
                if (levelMap[x, y - 1] == 2 || levelMap[x, y - 1] == 4)
                {
                    hasLeft = true;
                }
            }

            Debug.Log(hasAbove + " " + hasBelow + " " + hasLeft + " " + hasRight);

            if (hasAbove && hasRight)
            {
                return 180f;
            }
            else if (hasAbove && hasLeft)
            {
                return 270f;
            }
            else if (hasBelow && hasRight)
            {
                return 90f;
            }
            else if (hasBelow && hasLeft)
            {
                return 0f;
            }

        }// end of spriteID 3

        return 45f;
    }// end of RotateSprite(int spriteID, int x, int y)

    private void DrawSprite(int spriteID, float rotation)// -22.5, 12.5
    {
        /*
        if (spriteID == 1)// outside corner
        {
            //Debug.Log("printing first sprite");
            //Sprite testCorner =
            //GameObject outCorner = Instantiate(outsideCorner, printCoordinate, Quaternion.Euler(0f, 0f, 90f));
            GameObject outCorner = Instantiate(outsideCorner, printCoordinate, Quaternion.Euler(0f, 0f, rotation));
            outCorner.transform.localScale = wallScale;
        }
        else if (spriteID == 2)// outside wall
        {
            //Debug.Log("printing second sprite");
            GameObject outWall = Instantiate(outsideWall, printCoordinate, Quaternion.Euler(0f, 0f, rotation));
            outWall.transform.localScale = wallScale;
        }
        else if (spriteID == 3)// inside corner
        {
            //Debug.Log("printing third sprite");
            GameObject outWall = Instantiate(insideCorner, printCoordinate, Quaternion.Euler(0f, 0f, rotation));
            outWall.transform.localScale = wallScale;
        }
        else if (spriteID == 4)// inside wall
        {
            //Debug.Log("printing third sprite");
            GameObject outWall = Instantiate(insideWall, printCoordinate, Quaternion.Euler(0f, 0f, rotation));
            outWall.transform.localScale = wallScale;
        }
        */

        /*else*/ if (spriteID == 5)// normal pallet
        {
            //Debug.Log("printing third sprite");
            GameObject output = Instantiate(standardPallet, printCoordinate, Quaternion.identity);
            output.transform.localScale = palletScale;
        }
        else if (spriteID == 6)// power pallet
        {
            //Debug.Log("printing third sprite");
            GameObject output = Instantiate(powerPallet, printCoordinate, Quaternion.identity);
            output.transform.localScale = palletScale;
        }

        /*
        else if (spriteID == 7)// t junction
        {
            //Debug.Log("printing third sprite");
            GameObject outWall = Instantiate(outsideTJuuction, printCoordinate, Quaternion.Euler(0f, 0f, rotation));
            outWall.transform.localScale = wallScale;
        }
        */

    }

}
