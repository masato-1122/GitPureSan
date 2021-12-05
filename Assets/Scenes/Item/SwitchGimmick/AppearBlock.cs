using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearBlock : MonoBehaviour
{
    public GameObject item;
    public int limit = 10;
    private bool push = false;
    
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player") || push == false || limit > 0)
        {
            float posX = Random.Range(40, 50);
            //item = Instantiate( item, new Vector3( posX, 3f, -85f), Quaternion.identity);
            item = PhotonNetwork.Instantiate("MaterialBlock", new Vector3( posX, 2f, -85f), Quaternion.identity);
            item.GetComponent<ItemBehaviour>().SetAbandoned();
            limit--;
            push = true;
        }
    }

    void OnTriggerExit(Collider c)
    {
        push = false;
    }
}
