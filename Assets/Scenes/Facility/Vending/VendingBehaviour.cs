using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class VendingBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject spawn;
    private Vector3 offset = new Vector3(0f, 0f, -2f);
    private float posX = 0f;
    private int limit = 6;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        offset += gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Action(GameObject targetPoint)
    {
        
    }

    public void ActionForTargetedObject(GameObject target)  // アイテムの機能を対象物に使う
    {
        if (limit > 0)
        {
            offset.x += posX;
            GameObject item = PhotonNetwork.Instantiate(spawn.name, offset, Quaternion.identity);
            item.GetComponent<ItemBehaviour>().SetAbandoned();
            limit--;
            posX++;
        }
        Debug.Log("アイテム出現");
    }

    public void Damaged(GameObject attacker)
    {

    }
}
