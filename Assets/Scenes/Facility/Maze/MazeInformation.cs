using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MazeInformation : MonoBehaviour
{
    //取り出したcsvデータ
    static TextAsset csvFile;
    string str;
    string strget;
    //マップデータ
    int size;
    int[,] map;
    int[] iDat = new int[15];

    int a = 0;    //濫用　数値型変数
    int b = 0;    //濫用　数値型変数
    int c = 0;    //濫用　数値型変数


    // Start is called before the first frame update
    void Start()
    {
        map = new int[size, size];
        csvFile = Resources.Load("Mapping.csv") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            str = str +","+ line;
        }

        str = str + ",";

        for ( int i = 0; i < size; i++)
        {
            for( int j = 0; j < size; j++)
            {
                /*
                try
                {
                    iDat[0] = str.IndexOf(",", iDat[0]);   //","を検索
                }
                catch { break; }

                try
                {
                    iDat[1] = str.IndexOf(",", iDat[0] + 1);   //次の","を検索
                }
                catch { break; }

                iDat[2] = iDat[1] - iDat[0] - 1;                //何文字取り出すか決定

                try
                {
                    strget = str.Substring(iDat[0] + 1, iDat[2]);   //iDat[2]文字ぶんだけ取り出す
                }
                catch { break; }

                try
                {
                    iDat[3] = int.Parse(strget);        //取り出した文字列を数値型に変換
                }
                catch { break; }

                map[a, b] = iDat[3];                    //マップ用変数に保存。１とか６とか数字が入るよ
                b++;                            //一つ右のマップ用変数へ
                iDat[0]++;
                */
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
