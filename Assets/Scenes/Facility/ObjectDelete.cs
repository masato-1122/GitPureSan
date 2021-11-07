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
        if ( item.CompareTag("OBJECT"))
        {
            /*
            item.GetComponent<ItemBehaviour>().SetAbandoned();  
            PhotonNetwork.Destroy(item);
            */
            
        }
        if( item.CompareTag("Player"))
        {
            itemDelete(item);
        }
    }

    void itemDelete(GameObject player)
    {
        foreach (Transform childTransform in player.transform)
        {
            GameObject Hands = childTransform.gameObject;
            if (Hands.name == "RightHand")
            {
                GameObject rightItem = Hands.GetComponent<HandBehaviour>().DropItem();
                if (rightItem != null)
                {
                    rightItem.transform.parent = null;
                    rightItem.GetComponent<ItemBehaviour>().SetAbandoned();
                    PhotonNetwork.Destroy(rightItem);
                }
            }
            if (Hands.name == "LeftHand")
            {
                GameObject leftItem = Hands.GetComponent<HandBehaviour>().DropItem();
                if (leftItem != null)
                {
                    leftItem.transform.parent = null;
                    leftItem.GetComponent<ItemBehaviour>().SetAbandoned();
                    PhotonNetwork.Destroy(leftItem);
                }
            }
        }
    }
}
