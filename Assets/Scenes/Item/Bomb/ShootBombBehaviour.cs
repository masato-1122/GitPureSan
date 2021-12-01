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
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0)
        {
            Damage(follower);
            Destroy(follower);
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

    void Damage( GameObject g)
    {
        /*
        if( g.tag =="Player")
        {
            g.GetComponent<PlayerBehaviour>().Damage(20);
        }
        */

        if (particle != null)
        {
            Vector3 offset = gameObject.transform.position;
            GameObject ptl = PhotonNetwork.Instantiate(particle.name, offset, Quaternion.identity);
        }
    }
}
