using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class BombBehaviour : ItemBehaviour, ItemReceiveMessage
{
    private PhotonView photonView;
    private float timer;
    public GameObject explosionEffect = null;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
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
        base.Update();
    }

    // �i�K�{�j�A�C�e���̋@�\��Ώە��Ɏg��
    public void ActionForTargetedObject(GameObject target)
    {
        if (explosionEffect != null)
        {
            GameObject effect = PhotonNetwork.InstantiateRoomObject(explosionEffect.name, target.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    // �i�K�{�j�A�C�e���̋@�\���g��(���e���΂�)
    public void Action(GameObject targetPoint)
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {

        }
    }

    public void Damaged(GameObject attacker)
    {
        // Nothing
    }
}
