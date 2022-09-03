using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class BulletBehaviour : MonoBehaviour
{
    private float timer;
    private GameObject targetObject = null;
    private Rigidbody rb;
    void Start()
    {
        timer = 0;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0)
        {
            //PhotonNetwork.Destroy(gameObject);
            Destroy(gameObject);
        }
    }

    
    void OnCollisionEnter( Collision col )
    {
        
        GameObject obj = col.gameObject;

        if( obj.tag == "Player")
        {
            obj.GetComponent<PlayerBehaviour>().Damaged(gameObject);
        }
        
        ExecuteEvents.Execute<ItemReceiveMessage>(
                target: col.gameObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.Damaged(gameObject)
                );

        
        if(obj.tag=="WALL")
        {
            //PhotonNetwork.Destroy(gameObject);
            Destroy(gameObject);

            Debug.Log("Bullet Destroy");
            return;
        }
    }
    
}
