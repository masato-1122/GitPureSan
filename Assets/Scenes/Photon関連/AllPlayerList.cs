using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class AllPlayerList : MonoBehaviour
{
    public Text listText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void UpdateList()
    {
        GameObject[] pla = GameObject.FindGameObjectsWithTag("Player");

        Player[] player = PhotonNetwork.PlayerList;
        listText = gameObject.GetComponent<Text>();
        listText.text = "";
        for (int i = 0; i < pla.Length; i++)
        {
            Color c = pla[i].GetComponent<PlayerBehaviour>().GetClothColor();
            //string n = pla[i].GetComponent<PlayerBehaviour>().GetName();
            string n = player[i].NickName; 
            listText.text += RGBConvertHex(c, n);
            Debug.Log(n);
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
