using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;
using Photon.Realtime;

public class AllPlayerList : MonoBehaviour
{
    public Text listText;

    private string listMessage;
    // Start is called before the first frame update
    void Start()
    {
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        
    }

    public void UpdateList(GameObject[] ps)
    {
        Player[] player = PhotonNetwork.PlayerList;
        listText = gameObject.GetComponent<Text>();
        listText.text = "";
        for (int i = 0; i < ps.Length; i++)
        {
            Color c = ps[i].GetComponent<PlayerBehaviour>().GetClothColor();
            string n = player[i].NickName; 
            listText.text += RGBConvertHex(c, n);
        }
    }

    private string RGBConvertHex(Color c, string t)
    {
        int r = (int)c.r;
        string sr = r.ToString("x2");

        int g = (int)c.g;
        string sg = g.ToString("x2");

        int b = (int)c.b;
        string sb = b.ToString("x2");
        return "<color=" + sr + sg + sb + ">" + t + "</color>" + ("\n");
    }
}
