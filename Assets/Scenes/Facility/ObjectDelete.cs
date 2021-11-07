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
            
            item.GetComponent<ItemBehaviour>().SetAbandoned();  
            PhotonNetwork.Destroy(item);
            
        }
        if( item.CompareTag("Player"))
        {
            itemDelete(item);
        }
    }


    void itemDelete(GameObject player)
    {
        foreach (Transform child in player.transform)
        {
            foreach (Transform grandChildTransform in child)
            {
                if (grandChildTransform.gameObject.name == "RightHand")
                {
                    GameObject rightHand = grandChildTransform.gameObject;
                    GameObject rightItem = rightHand.GetComponent<HandBehaviour>().DropItem();
                    if (rightItem != null)
                    {
                        rightItem.transform.parent = null;
                        rightItem.transform.position = player.transform.position;
                        rightItem.transform.rotation = Quaternion.identity;
                        rightItem.GetComponent<ItemBehaviour>().SetAbandoned();
                        PhotonNetwork.Destroy(rightItem);
                    }
                }

                if (grandChildTransform.gameObject.name == "LeftHand")
                {
                    GameObject leftHand = grandChildTransform.gameObject;
                    GameObject leftItem = leftHand.GetComponent<HandBehaviour>().DropItem();
                    if (leftItem != null)
                    {
                        leftItem.transform.parent = null;
                        leftItem.transform.position = player.transform.position;
                        leftItem.transform.rotation = Quaternion.identity;
                        leftItem.GetComponent<ItemBehaviour>().SetAbandoned();
                        PhotonNetwork.Destroy(leftItem);
                    }
                }
            }
        }
    }
}
