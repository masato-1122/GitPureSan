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
        {   // ストリームへの書き込み

            try
            {
                stream.SendNext(player.UserName);
                stream.SendNext(player.UserID);
            }
            catch (System.Exception e)
            {
                Debug.LogError("プレイヤーの情報を書き込めません...\n\n" + e);
            }
        }
        else
        {   // ストリームの読み込み

            try
            {
                string nickName = (string)stream.ReceiveNext();
                string userId = (string)stream.ReceiveNext();

                player.UserName = nickName;
                player.UserID = userId;

            }
            catch (System.Exception e)
            {
                Debug.LogError("プレイヤーの情報を読み込めません...\n\n" + e);
            }
        }
    }
}
