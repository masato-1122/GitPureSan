using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BombBehaviour : ItemBehaviour, ItemReceiveMessage
{
    private PhotonView photonView;
    private int timer;
    protected float now;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        now = 0;
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();
        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        now += Time.deltaTime;
        if (now >= 0.1f)
        {
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            now = 0.0f;
        }
        base.Update();
    }

    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {
        
    }

    // （必須）アイテムの機能を使う
    public void Action(GameObject targetPoint)
    {

    }

    public void Damaged(GameObject attacker)
    {
        // Nothing
    }
}
