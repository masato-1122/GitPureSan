using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class PhotonTextView : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject Text;
    
    // Start is called before the first frame update
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if( stream.IsWriting)
        {
            stream.SendNext(Text.GetComponent<Text>().text);
        }
        else
        {
            string t = (string)stream.ReceiveNext();
            Text.GetComponent<Text>().text = t;
        }
    }
}
