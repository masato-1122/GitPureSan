using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearAxe : MonoBehaviour
{
    private bool push = false;
    private int limit = 6;
    private float posX = 40;

    void OnTriggerEnter( Collider c )
    {
        if( (c.gameObject.CompareTag("Player") && push == false) && limit >= 0)
        {
            posX++;
            GameObject item = PhotonNetwork.Instantiate("Axe", new Vector3(posX, 0.5f, -87f), Quaternion.identity);
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
