using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearBlock : MonoBehaviour
{
    private int limit = 6;
    private bool push = false;
    private float posX = 40;

    void OnTriggerEnter(Collider c)
    {
        if ((c.gameObject.CompareTag("Player") && push == false )&& limit >= 0)
        {
            posX++;
            //item = Instantiate( item, new Vector3( posX, 3f, -85f), Quaternion.identity);
            GameObject item = PhotonNetwork.Instantiate("MaterialBlock", new Vector3( posX, 2f, -85f), Quaternion.identity);
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
