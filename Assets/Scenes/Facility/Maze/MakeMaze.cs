using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeMaze : MonoBehaviour
{
    public int max;
    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;

    //ポジション
    private int[] startPos;
    private int[] goalPos;
    //1:通路、0:壁
    private int[,] walls;

    GameObject startObj;
    GameObject goalObj;
    GameObject clone;


    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }


    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        walls = new int[max, max];
        startPos = GetStartPosition();
        //startPos = new int[] { start.trasnform.position.x, start.transform.position.z};
        goalPos = MakeMap(startPos);
        int[] tempPos = goalPos;
        for (int i = 0; i < max * 5; i++)
        {
            MakeMap(tempPos);
            tempPos = GetStartPosition();
        }

        BuildMaze();


        startObj = Instantiate(start, new Vector3(startPos[0], 1, startPos[1]), Quaternion.identity) as GameObject;
        goalObj = Instantiate(goal, new Vector3(goalPos[0], 1, goalPos[1]), Quaternion.identity) as GameObject;
        startObj.transform.parent = transform;
        goalObj.transform.parent = transform;
    }

    
    int[] GetStartPosition()
    {
        int randx = Random.Range(0, max);
        int randy = Random.Range(0, max);

        while (randx % 2 != 0 || randy % 2 != 0)
        {
            randx = Random.Range(0, max);
            randy = Random.Range(0, max);
        }
        return new int[] { randx, randy };
    }
    

    int[] MakeMap(int[] sPos)
    {
        //スタート位置配列を複製
        int[] tempStartPos = new int[2];
        sPos.CopyTo(tempStartPos, 0);

        //移動可能なリストを取得
        Dictionary<int, int[]> movePos = GetPosition(tempStartPos);

        while (movePos != null)
        {
            int[] tempPos = movePos[Random.Range(0, movePos.Count)];
            walls[tempPos[0], tempPos[1]] = 1;

            int xPos = tempPos[0] + (tempStartPos[0] - tempPos[0]) /2;
            int yPos = tempPos[1] + (tempStartPos[1] - tempPos[1]) /2;
            walls[xPos, yPos] = 1;

            tempStartPos = tempPos;
            movePos = GetPosition(tempPos);
        }
        return tempStartPos;
    }

    Dictionary<int, int[]> GetPosition(int[] sPos)
    {
        int x = sPos[0];
        int y = sPos[1];

        List<int[]> position = new List<int[]>
        {
            new int[]{ x, y+2},
            new int[]{ x, y-2},
            new int[]{ x+2, y},
            new int[]{ x-2, y}
        };

        Dictionary<int, int[]> positions = position.Where(p => !isOutOfRange(p[0], p[1]) && walls[p[0], p[1]] == 0)
                                                   .Select((p, i) => new { p, i })
                                                   .ToDictionary(p => p.i, p => p.p);

        return positions.Count() != 0 ? positions : null;
    }

    bool isOutOfRange(int x, int y)
    {
        return (x < 0 || y < 0 || x >= max || y >= max);
    }

    void BuildMaze()
    {
        for (int i = -1; i <= max; i++)
        {
            for (int j = -1; j <= max; j++)
            {
                if (isOutOfRange(i, j) || walls[i, j] == 0)
                {
                    GameObject wallobj = Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
                    wallobj.transform.parent = transform;
                }

                GameObject floorObj = Instantiate(floor, new Vector3(i, -1, j), Quaternion.identity) as GameObject;
                floorObj.transform.parent = transform;
            }
        }
    }
}
