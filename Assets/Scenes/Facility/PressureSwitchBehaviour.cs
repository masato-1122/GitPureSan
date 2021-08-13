using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PressureSwitchBehaviour : MonoBehaviour
{
    public GameObject receiver;  // オンオフを受け取るオブジェクト

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per fram
    void Update()
    {
        
    }

    //
    void OnTriggerEnter( Collider c )
    {
        if( c.gameObject.CompareTag("Player") )
        {
            ExecuteEvents.Execute<ItemReceiveMessage>(
                target: receiver,
                eventData: null,
                functor: (receiver, eventData) => receiver.ActionForTargetedObject(c.gameObject)
                );
        }
    }
}
