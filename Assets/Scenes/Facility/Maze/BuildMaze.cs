using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class BuildMaze : ItemBehaviour, ItemReceiveMessage
{
    public PhotonView photonView;
    private bool flag = true;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void ActionForTargetedObject(GameObject target)
    {
        if(flag)
        {
            photonView.RPC("Build", RpcTarget.All);
            flag = false;
            //photonView.RPC("ObjectOff", RpcTarget.All, gameObject);
        }
    }

    public void Action(GameObject target)
    {

    }

    public void Damaged(GameObject attacker)
    {

    }
}
