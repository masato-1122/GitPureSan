using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class NonPhotonVending : ItemBehaviour, ItemReceiveMessage
{
    public GameObject spawn;
    private Vector3 offset = new Vector3(0f, 0f, -5f);
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

    public void ActionForTargetedObject(GameObject target)  // �A�C�e���̋@�\��Ώە��Ɏg��
    {
        if (limit > 0)
        {
            offset.x += 1f;
            GameObject item = Instantiate(spawn, offset, Quaternion.identity) as GameObject;
            item.GetComponent<ItemBehaviour>().SetAbandoned();
            limit--;
        }
    }

    public void Damaged(GameObject attacker)
    {

    }

    public void Reset()
    {
        limit = 6;
        offset = gameObject.transform.position;
        offset.z += -5f;
    }
}
