using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
        transform.Translate(new Vector3(0f, Mathf.Sin(10 * Time.time) * 0.5f, 0f) * Time.deltaTime, Space.World);
    }
}
