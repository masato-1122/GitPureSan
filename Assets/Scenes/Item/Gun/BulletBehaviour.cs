using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

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
    }
}
