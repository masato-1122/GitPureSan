using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MazeInformation : MonoBehaviour
{
    //���o����csv�f�[�^
    static TextAsset csvFile;
    string str;
    string strget;
    //�}�b�v�f�[�^
    int size;
    int[,] map;
    int[] iDat = new int[15];

    int a = 0;    //���p�@���l�^�ϐ�
    int b = 0;    //���p�@���l�^�ϐ�
    int c = 0;    //���p�@���l�^�ϐ�


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
                    iDat[0] = str.IndexOf(",", iDat[0]);   //","������
                }
                catch { break; }

                try
                {
                    iDat[1] = str.IndexOf(",", iDat[0] + 1);   //����","������
                }
                catch { break; }

                iDat[2] = iDat[1] - iDat[0] - 1;                //���������o��������

                try
                {
                    strget = str.Substring(iDat[0] + 1, iDat[2]);   //iDat[2]�����Ԃ񂾂����o��
                }
                catch { break; }

                try
                {
                    iDat[3] = int.Parse(strget);        //���o����������𐔒l�^�ɕϊ�
                }
                catch { break; }

                map[a, b] = iDat[3];                    //�}�b�v�p�ϐ��ɕۑ��B�P�Ƃ��U�Ƃ������������
                b++;                            //��E�̃}�b�v�p�ϐ���
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
