using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BombBehaviour : ItemBehaviour, ItemReceiveMessage
{
    protected int hp = 5;
    private PhotonView photonView;

    private Rigidbody rb;

    private Vector3 force;
    private float forceMagnitude = 10.0f;
    private Vector3 forceDirection;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        rb = gameObject.GetComponent<Rigidbody>();
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();
        heldAngle = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Update();
        forceDirection = gameObject.transform.position;
    }

    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {

    }

    // （必須）アイテムの機能を使う(爆弾を飛ばす)
    public void Action(GameObject targetPoint)
    {

        //PhotonNetwork.Destroy(gameObject);
    }

    public void Damaged(GameObject attacker)
    {
        hp -= 1;
        if (hp <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
}
