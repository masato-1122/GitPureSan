using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearBomb : MonoBehaviour
{
    private bool push = false;
    private int limit = 6;
    private float posX = 40;

    // Update is called once per frame
    void OnTriggerEnter(Collider c)
    {
        if ((c.gameObject.CompareTag("Player") && push == false )&& limit >= 0)
        {
            posX++;
            GameObject item = PhotonNetwork.Instantiate("Bomb", new Vector3(posX, 2f, -91f), Quaternion.identity);
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
