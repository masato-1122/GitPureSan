using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonRendererView : MonoBehaviourPunCallbacks, IPunObservable
{
    public Renderer[] Renderers;

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {   // ストリームへの書き込み

            try
            {
                // Material 
                foreach (Renderer renderer in Renderers)
                {
                    stream.SendNext(renderer.material.color.r);
                    stream.SendNext(renderer.material.color.g);
                    stream.SendNext(renderer.material.color.b);
                    stream.SendNext(renderer.material.color.a);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("色情報を書き込めませんでした...\n\n" + e);
            }
        }
        else
        {   // ストリームの読み込み

            try
            {
                // Material
                foreach (Renderer renderer in Renderers)
                {
                    float r = (float)stream.ReceiveNext();
                    float g = (float)stream.ReceiveNext();
                    float b = (float)stream.ReceiveNext();
                    float a = (float)stream.ReceiveNext();

                    renderer.material.color = new Color(r, g, b, a);
                }
            }
            catch(System.Exception e)
            {
                Debug.LogError("色情報を読み込めませんでした...\n\n" + e);
            }
        }
    }
}
