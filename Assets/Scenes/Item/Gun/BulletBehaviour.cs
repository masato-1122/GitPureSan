﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBehaviour : MonoBehaviour
{
    private float timer;
    private GameObject targetObject = null;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0)
        {
            Destroy(gameObject);
        }
    }

    
    void OnCollisionEnter( Collision col )
    {
        GameObject obj = col.gameObject;
        if( obj.tag == "Player")
        {
            obj.GetComponent<PlayerBehaviour>().Damage(10);
        }

        ExecuteEvents.Execute<ItemReceiveMessage>(
                target: targetObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.Damaged(gameObject)
                );
        /*
        ExecuteEvents.Execute<PlayerBehaviour>(
                target: targetObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.Damage(10)
                );
        */

        /*
        if(obj.tag=="WALL")
        {
            Destroy(gameObject);
            return;
        }
        if(obj.tag=="ENEMY")
        {
            GameObject zombie = collisionInfo.gameObject;
            ExecuteEvents.Execute<ReceiveMessage>(
                target: zombie,
                eventData: null,
                functor: (receiver, eventData) => receiver.setDead());
        }
        if(obj.tag == "OBJECT")
        {
            Destroy(gameObject);
            return;
        }
        */
    }
    
}
