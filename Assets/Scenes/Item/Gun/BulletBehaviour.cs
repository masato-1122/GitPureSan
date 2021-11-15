using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBehaviour : MonoBehaviour
{
    private float timer;
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

    /*
    void OnCollisionEnter( Collision collisionInfo )
    {
        if(collisionInfo.gameObject.tag=="WALL")
        {
            Destroy(gameObject);
            return;
        }
        if(collisionInfo.gameObject.tag=="ENEMY")
        {
            GameObject zombie = collisionInfo.gameObject;
            ExecuteEvents.Execute<ReceiveMessage>(
                target: zombie,
                eventData: null,
                functor: (receiver, eventData) => receiver.setDead());
        }
        if(collisionInfo.gameObject.tag == "OBJECT")
        {
            Destroy(gameObject);
            return;
        }
    }
    */
}
