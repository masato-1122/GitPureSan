using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class ReadingMaze : MonoBehaviour
{
    TextAsset csvFile;
    private int mazeSize;
    List<string[]> csvDatas = new List<string[]>();

    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;

    const int WALL = 1;
    const int PATH = 0;

    private int[,] field;
    private string[] textData;


    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("SaveData1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
            mazeSize++;
        }
        field = new int[mazeSize, mazeSize];

        for ( int i = 0; i< mazeSize; i++)
        {
            for( int j = 0; j < mazeSize; j++)
            {
                field[i, j] = int.Parse(csvDatas[i][j]);
                Debug.Log(field[i, j]);
                
                if(int.Parse(csvDatas[i][j]) == 1)
                {
                    GameObject wallobj = Instantiate(wall, new Vector3(i*3+40, 0, j*3+400), Quaternion.identity) as GameObject;
                    wallobj.transform.parent = transform;
                }
                GameObject floorObj = Instantiate(floor, new Vector3(i*3+40, -1, j*3+400), Quaternion.identity) as GameObject;
                floorObj.transform.parent = transform;
                
            }
        }
        start.transform.position = new Vector3(1*3+40, 0, 1*3+400);
        goal.transform.position = new Vector3((mazeSize-2)*3+40, 1, (mazeSize-2)*3+400);


    }
}
