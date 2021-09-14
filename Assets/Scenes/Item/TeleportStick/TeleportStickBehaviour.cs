using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TeleportStickBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject teleportPoint = null;     // テレポート先オブジェクト
    public GameObject teleportEffect = null;    // エフェクト
    public float effectLifeTime = 2;            // エフェクトの持続時間
    protected float now;
    private PhotonView photon = null;

    [SerializeField]private string objName;
    private Vector3 offset;

    protected void Start()
    {
        base.Start();

        photon = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        now = 0;

        // アトリビュート　※[所有可能][放棄可能]なアイテム
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();

        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
        teleportPoint = GameObject.Find(objName);
    }


    protected void Update()
    {
        now += Time.deltaTime;
        if (now >= 0.1f)
        {
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            now = 0.0f;
        }

        base.Update();
        if (teleportPoint != null)
        {
            offset = teleportPoint.transform.position + new Vector3(0f, 5f, 0f);
        }
        else
        {
            return;
        }
    }


    // （必須）アイテムの機能を使う
    public void Action(GameObject targetPoint)
    {
        transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }


    // （必須）アイテムの機能を対象物に使う
    public void ActionForTargetedObject(GameObject target)
    {

        if (teleportPoint != null)
        {
            // エフェクト
            if(teleportEffect != null)
            {
               // GameObject effect = Instantiate(teleportEffect, target.transform.position, Quaternion.Euler(-90, 0, 0));

                GameObject effect = PhotonNetwork.Instantiate(teleportEffect.name, target.transform.position, Quaternion.Euler(-90, 0, 0));

                Destroy(effect, effectLifeTime);
            }

            // 対象をテレポート
            target.transform.position = offset;

            transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        }
        else
        {
            Debug.Log("テレポート先のオブジェクトを設定してください。");
        }
    }


    // （必須）ダメージを受ける
    public void Damaged(GameObject attacker)
    {
        // ダメージは受けない
    }

}
