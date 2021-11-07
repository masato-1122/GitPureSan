using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

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

    void OnCollisionEnter(Collision collision)
    {
        GameObject item = collision.gameObject;
        if (item.CompareTag("OBJECT"))
        {
            item.GetComponent<ItemBehaviour>().SetAbandoned();
            PhotonNetwork.Destroy(item);
        }
        if (item.CompareTag("Player"))
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
                if (grandChildTransform.gameObject.name == "LeftHand")
                {
                    GameObject leftHand = grandChildTransform.gameObject;
                    GameObject dropItem = leftHand.GetComponent<HandBehaviour>().DropItem();
                    if (dropItem != null)
                    {
                        dropItem.transform.parent = null;
                        dropItem.transform.position = player.transform.position;
                        dropItem.transform.rotation = Quaternion.identity;
                        dropItem.GetComponent<ItemBehaviour>().SetAbandoned();
                        PhotonNetwork.Destroy(dropItem);
                    }
                }

                if (grandChildTransform.gameObject.name == "RightHand")
                {
                    GameObject rightHand = grandChildTransform.gameObject;
                    GameObject dropItem = rightHand.GetComponent<HandBehaviour>().DropItem();
                    if (dropItem != null)
                    {
                        dropItem.transform.parent = null;
                        dropItem.transform.position = player.transform.position;
                        dropItem.transform.rotation = Quaternion.identity;
                        dropItem.GetComponent<ItemBehaviour>().SetAbandoned();
                        PhotonNetwork.Destroy(dropItem);
                    }
                }
            }
        }
    }
}
