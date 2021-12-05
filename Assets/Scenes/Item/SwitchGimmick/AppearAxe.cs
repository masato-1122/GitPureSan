using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AppearAxe : MonoBehaviour
{
    private bool push = false;
    public int limit = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider c )
    {
        if( c.gameObject.CompareTag("Player") || push == false || limit > 0)
        {
            float posX = Random.Range(40, 50);
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
