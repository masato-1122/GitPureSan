using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TimeAcceBehaviour : ItemBehaviour, ItemReceiveMessage
{
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
        photonView.RPC("SetWeather", RpcTarget.All, 6);
    }

    public void Action(GameObject target)
    {

    }

    public void Damaged(GameObject attacker)
    {

    }
}
