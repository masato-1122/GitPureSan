using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonNetworkView : MonoBehaviourPunCallbacks, IPunObservable
{
    public PlayerBehaviour player;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {   // �X�g���[���ւ̏�������

            try
            {
                stream.SendNext(player.UserName);
                stream.SendNext(player.UserID);
            }
            catch (System.Exception e)
            {
                Debug.LogError("�v���C���[�̏����������߂܂���...\n\n" + e);
            }
        }
        else
        {   // �X�g���[���̓ǂݍ���

            try
            {
                string nickName = (string)stream.ReceiveNext();
                string userId = (string)stream.ReceiveNext();

                player.UserName = nickName;
                player.UserID = userId;

            }
            catch (System.Exception e)
            {
                Debug.LogError("�v���C���[�̏���ǂݍ��߂܂���...\n\n" + e);
            }
        }
    }
}
