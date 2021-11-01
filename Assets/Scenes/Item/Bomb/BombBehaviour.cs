using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BombBehaviour : ItemBehaviour, ItemReceiveMessage
{
    private PhotonView photonView;
    private bool timerFlag;
    private float timer;
    public GameObject explosionEffect;
    protected int hp = 5;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        timerFlag = false;
        timer = 0;
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

        if( timerFlag)
        {
            timer += Time.deltaTime;
        }
        base.Update();
    }

    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {
        if (explosionEffect != null)
        {
            Vector3 offset = gameObject.transform.position;
            if (timer >= 7f)
            {
                GameObject ptl = PhotonNetwork.Instantiate(explosionEffect.name, offset, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    // （必須）アイテムの機能を使う(爆弾を飛ばす)
    public void Action(GameObject targetPoint)
    {
        timerFlag = true;

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
