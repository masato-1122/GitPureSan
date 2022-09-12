using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Photon.Realtime;

public class ReadingMaze : MonoBehaviour
{
    TextAsset csvFile;
    private int mazeSize;
    List<string[]> csvDatas = new List<string[]>();

    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;

    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    const int WALL = 1;
    const int PATH = 0;

    private int[,] field;
    private string[] textData;

    private string mazeData1 = "SaveData1";
    private string mazeData2 = "SaveData2";
    private string mazeData3 = "SaveData3";
    private string saveData;

    //
    // Start is called before the first frame update
    void Start()
    {
        //時間帯によって生成する迷路を変更
        int hour = System.DateTime.Now.Hour;

        switch(hour % 3)
        {
            case 0:
                saveData = mazeData1;
                break;
            case 1:
                saveData = mazeData2;
                break;
            case 2:
                saveData = mazeData3;
                break;
        }

        
        csvFile = Resources.Load(saveData) as TextAsset;
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

        wall1.transform.position = new Vector3((start.transform.position.x + goal.transform.position.x)/2, start.transform.position.y, goal.transform.position.z + 3);
        wall2.transform.position = new Vector3((start.transform.position.x + goal.transform.position.x)/2, start.transform.position.y, start.transform.position.z - 3);
        wall3.transform.position = new Vector3(goal.transform.position.x + 3, start.transform.position.y, (start.transform.position.z + goal.transform.position.z)/2);
        wall4.transform.position = new Vector3(start.transform.position.x - 3, start.transform.position.y, (start.transform.position.z + goal.transform.position.z)/2);
    }
}
