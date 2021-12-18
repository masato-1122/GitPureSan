using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonOwnershipView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ŒÄ‚Ño‚µ‘¤‚ªŠ—LŒ ‚ğ‚Â
    public void RequestView(GameObject target)
    {
        if( target.GetComponent<PhotonView>())
        {
            PhotonView view = target.GetComponent<PhotonView>();
            view.RequestOwnership();
        }
    }
}
