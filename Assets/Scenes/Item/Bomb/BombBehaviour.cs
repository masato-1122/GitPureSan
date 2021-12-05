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
    public GameObject muzzle;
    private int launch;
    private int maxLaunch = 10;

    private Rigidbody rb;

    private Vector3 force;
    private Vector3 offset;
    public float initialVelocity = 25.0f;

    public float maxTimer = 1.0f;
    private float time;

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
        launch = maxLaunch;
        heldAngle = new Vector3(0.0f, 0.0f, 0.0f);
        offset = new Vector3(0f, 5f, 0f);
        time = maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Update();
        time +=Time.deltaTime;
    }

    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {
        Action(target);
    }

    // （必須）アイテムの機能を使う(爆弾を飛ばす)
    public void Action(GameObject targetPoint)
    {
        if (launch > 0 && time > maxTimer)
        {
            GameObject bullet = PhotonNetwork.Instantiate(bombPrefab.name, muzzle.transform.position, muzzle.transform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = -transform.up* initialVelocity;
            time = 0.0f;
            //launch--;
        }

    }

    public void Damaged(GameObject attacker)
    {
        hp -= 5;
        if (hp <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    public void addLaunch()
    {
        launch = maxLaunch;
    }
}
