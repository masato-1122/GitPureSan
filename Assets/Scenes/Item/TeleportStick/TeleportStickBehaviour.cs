using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TeleportStickBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject teleportPoint = null;     // �e���|�[�g��I�u�W�F�N�g
    public GameObject teleportEffect = null;    // �G�t�F�N�g
    public float effectLifeTime = 2;            // �G�t�F�N�g�̎�������
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

        // �A�g���r���[�g�@��[���L�\][�����\]�ȃA�C�e��
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


    // �i�K�{�j�A�C�e���̋@�\���g��
    public void Action(GameObject targetPoint)
    {
        transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }


    // �i�K�{�j�A�C�e���̋@�\��Ώە��Ɏg��
    public void ActionForTargetedObject(GameObject target)
    {

        if (teleportPoint != null)
        {
            // �G�t�F�N�g
            if(teleportEffect != null)
            {
               // GameObject effect = Instantiate(teleportEffect, target.transform.position, Quaternion.Euler(-90, 0, 0));

                GameObject effect = PhotonNetwork.Instantiate(teleportEffect.name, target.transform.position, Quaternion.Euler(-90, 0, 0));

                Destroy(effect, effectLifeTime);
            }

            // �Ώۂ��e���|�[�g
            target.transform.position = offset;

            transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
        }
        else
        {
            Debug.Log("�e���|�[�g��̃I�u�W�F�N�g��ݒ肵�Ă��������B");
        }
    }


    // �i�K�{�j�_���[�W���󂯂�
    public void Damaged(GameObject attacker)
    {
        // �_���[�W�͎󂯂Ȃ�
    }

}
