using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TreeBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject particle = null;
    public GameObject particlePoint = null;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetAttribute(ATTRIB_BREAKABLE);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Action( GameObject targetPoint )
    {

    }

    public void ActionForTargetedbject( GameObject target )
    {

    }

    //
    public void Damaged(GameObject attacker)
    {
        // パーティクル表示
        GameObject ptl = PhotonNetwork.Instantiate(particle.name, particlePoint.transform.position, particlePoint.transform.rotation);
    }
}
