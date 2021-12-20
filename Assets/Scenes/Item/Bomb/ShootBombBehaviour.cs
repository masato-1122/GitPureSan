using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class ShootBombBehaviour : MonoBehaviour
{
    public GameObject particle;
    private float timer;
    private Rigidbody rb;
    private GameObject follower = null;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rb = gameObject.GetComponent<Rigidbody>();
        //StartCoroutine("Damage", follower);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0)
        {
            Damage(follower);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        rb.isKinematic = true;
        if( c.gameObject.tag == "OBJECT")
        {
            follower = c.gameObject;
        }
    }

    void Damage( GameObject targetObject)
    {
        
        ExecuteEvents.Execute<ItemReceiveMessage>(
                target: targetObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.Damaged(gameObject)
                );
        

        if (particle != null)
        {
            Vector3 offset = gameObject.transform.position;
            GameObject ptl = PhotonNetwork.Instantiate(particle.name, offset, Quaternion.identity);
        }
    }
}
