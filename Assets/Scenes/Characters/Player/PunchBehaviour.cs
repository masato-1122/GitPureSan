using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject hand;
    //パンチもアイテム扱い
    //掴むといった手の操作を追加する時、新たに装備品として作る必要がある

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        hand = null;
    }

    // Update is called once per frame
    protected void Update()
    {
        
    }

    //
    public void Action( GameObject targetObject )
    {
    }

    public void ActionForTargetedObject( GameObject target )
    {
        Action();
    }

    public void Damaged(GameObject attacker)
    {
        // Nothing
    }
}
