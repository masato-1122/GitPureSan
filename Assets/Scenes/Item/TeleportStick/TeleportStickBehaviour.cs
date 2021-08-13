using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class TeleportStickBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject teleportPoint = null;     // �e���|�[�g��I�u�W�F�N�g
    public GameObject teleportEffect = null;    // �G�t�F�N�g
    public float effectLifeTime = 0;            // �G�t�F�N�g�̎�������
    protected float now;
    private PhotonView photon = null;
    

    protected void Start()
    {
        base.Start();

        photon = this.GetComponent<PhotonView>();
        now = 0;

        // �A�g���r���[�g�@��[���L�\][�����\]�ȃA�C�e��
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();

        

        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
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
    }


    // �i�K�{�j�A�C�e���̋@�\���g��
    public void Action(GameObject targetPoint)
    {
        transform.localEulerAngles = new Vector3(90.0f, 0.0f, 0.0f);
    }


    // �i�K�{�j�A�C�e���̋@�\��Ώە��Ɏg��
    public void ActionForTargetedObject(GameObject target)
    {

        if (true)
        {
            // �G�t�F�N�g
            if(teleportEffect != null)
            {
               // GameObject effect = Instantiate(teleportEffect, target.transform.position, Quaternion.Euler(-90, 0, 0));

                GameObject effect = PhotonNetwork.Instantiate("TeleportEffect", target.transform.position, Quaternion.Euler(-90, 0, 0));

                Destroy(effect, effectLifeTime);
            }

            // �Ώۂ��e���|�[�g
            target.transform.position = new Vector3(15f, 11f, -50f);

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
