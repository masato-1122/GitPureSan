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
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);
        float forceMagnitude = 10.0f;
        Vector3 force = forceMagnitude * forceDirection;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(force, ForceMode.Impulse);
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
    }
}
