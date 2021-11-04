using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObjectDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        
        GameObject item = collider.gameObject;
        if ( collider.gameObject.CompareTag("OBJECT"))
        {
            
            if ( item.GetComponent<ItemBehaviour>().IsAbandoned() == true)
            {
                Debug.Log("êGÇÍÇΩ");
                PhotonNetwork.Destroy(item);
            }
        }
    }
}
