using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BombBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject bombPrefab;
    protected int hp = 5;
    private PhotonView photonView;

    private Rigidbody rb;

    private Vector3 force;
    public float initialVelocity = 25.0f;

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
    }

    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {

    }

    // （必須）アイテムの機能を使う(爆弾を飛ばす)
    public void Action(GameObject targetPoint)
    {
        GameObject bullet = Instantiate(bombPrefab, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
        //GameObject bullet = PhotonNetwork.Instantiate("Bullet", muzzle.transform.position, muzzle.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * initialVelocity;
        //Destroy(gameObject);
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
}
