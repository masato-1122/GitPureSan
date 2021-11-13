using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class ShootBombBehaviour : MonoBehaviour
{
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ENEMY")
        {
            GameObject zombie = collision.gameObject;
            ExecuteEvents.Execute<ReceiveMessage>(
                target: zombie,
                eventData: null,
                functor: (receiver, eventData) => receiver.setDead());
        }


        if (particle != null)
        {
            Vector3 offset = gameObject.transform.position;
            GameObject ptl = PhotonNetwork.Instantiate(particle.name, offset, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}