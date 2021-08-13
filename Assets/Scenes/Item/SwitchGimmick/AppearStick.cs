using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearStick : MonoBehaviour
{
    public GameObject item;


    void Start()
    {

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            float posX = Random.Range(40, 50);
            //item = Instantiate( item, new Vector3( posX, 3f, -85f), Quaternion.identity);
            item = PhotonNetwork.Instantiate("TelaportStick", new Vector3(posX, 1f, -90f), Quaternion.identity);
        }
    }
}