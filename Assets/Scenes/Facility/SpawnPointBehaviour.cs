using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn( GameObject obj )
    {
        obj.transform.position = transform.Find("Point").transform.position;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
