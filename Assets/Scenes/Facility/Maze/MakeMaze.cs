using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeMaze : MonoBehaviour
{
    const int WALL = 1;
    const int PATH = 0;
    //1:•Ç 0:’Ê˜H
    public int max = 29;

    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;


    // Start is called before the first frame update
    void Start()
    {
        int[,] field = new int[max, max];
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                field[i, j] = 0;
            }
        }

        for (int i = 0; i < max; i++)
        {
            field[i, 0] = 1;
            field[0, i] = 1;
            field[i, max-1] = 1;
            field[max-1, i] = 1;
        }

        for (int i = 2; i< max -1; i+=2)
        {
            for (int j = 2; j < max -1; j+=2)
            {
                field[i, j] = 1;

                while (true)
                {
                    int direction;

                    if (j == 2)
                    {
                        direction = Random.Range(0, 3);
                    }
                    else
                    {
                        direction = Random.Range(0, 2);
                    }

                    //direction = Random.Range(0, 3);
                    int wallx = i;
                    int wally = j;
                    switch (direction)
                    {
                        case 0:
                            wallx++;
                            break;
                        case 1:
                            wally++;
                            break;
                        case 2:
                            wallx--;
                            break;
                        case 3:
                            wally--;
                            break;
                    }
                    if (field[wallx, wally] != WALL)
                    {
                        field[wallx, wally] = WALL;
                        break;
                    }
                }
            }
        }
        for (int i = 0; i < max; i++)
        {
            for (int j = 0; j < max; j++)
            {
                if (field[i, j] == WALL)
                {
                    GameObject wallobj = Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                    wallobj.transform.parent = transform;
                }
                GameObject floorObj = Instantiate(floor, new Vector3(i, -1, j), Quaternion.identity) as GameObject;
                floorObj.transform.parent = transform;
            }
        }
        
        GameObject startObj = Instantiate(start, new Vector3(1, 1, 1), Quaternion.identity) as GameObject;
        startObj.transform.parent = transform;
        GameObject goalObj = Instantiate(goal, new Vector3(max-2, 1, max-2), Quaternion.identity) as GameObject;
        goalObj.transform.parent = transform;
        
    }
}
