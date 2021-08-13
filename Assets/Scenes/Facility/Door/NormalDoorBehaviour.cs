using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDoorBehaviour : ItemBehaviour, ItemReceiveMessage
{
    protected bool opened;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Action( GameObject targetPoint )
    {

    }

    public void ActionForTargetedObject(GameObject target)  // アイテムの機能を対象物に使う
    {
        if (opened)
        {
            transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            opened = false;
        }
        else
        {
            transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            opened = true;
        }
    }

    public void Damaged(GameObject attacker)
    {

    }
}
