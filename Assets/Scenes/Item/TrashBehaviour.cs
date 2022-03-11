using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TrashBehaviour : MonoBehaviour
{
    private GameObject rightHand;
    private GameObject leftHand;
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
        if (item.CompareTag("Player"))
        {
            rightHand = item.GetComponent<PlayerBehaviour>().GetRightHandItem();
            leftHand = item.GetComponent<PlayerBehaviour>().GetLeftHandItem();
            itemDelete(item);
        }
    }


    void itemDelete(GameObject player)
    {
        GameObject dropItem1 = leftHand.GetComponent<HandBehaviour>().DropItem();
        if (dropItem1 != null)
        {
            dropItem1.transform.parent = null;
            dropItem1.transform.position = player.transform.position;
            dropItem1.transform.rotation = Quaternion.identity;
            dropItem1.GetComponent<ItemBehaviour>().SetAbandoned();
            //dropItem1.GetComponent<PhotonView>().RequestOwnership();
            dropItem1.GetComponent<ItemBehaviour>().ItemDelete();
        }

        GameObject dropItem2 = rightHand.GetComponent<HandBehaviour>().DropItem();
        if (dropItem2 != null)
        {
            dropItem2.transform.parent = null;
            dropItem2.transform.position = player.transform.position;
            dropItem2.transform.rotation = Quaternion.identity;
            dropItem2.GetComponent<ItemBehaviour>().SetAbandoned();
            //dropItem2.GetComponent<PhotonView>().RequestOwnership();
            dropItem2.GetComponent<ItemBehaviour>().ItemDelete();
        }

        /*
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
        */
    }
}
