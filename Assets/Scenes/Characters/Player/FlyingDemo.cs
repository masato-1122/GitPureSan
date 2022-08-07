using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingDemo : MonoBehaviour
{
    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject body;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
        transform.Translate(new Vector3(0f, Mathf.Sin(10 * Time.time) * 0.5f, 0f) * Time.deltaTime, Space.World);
    }

    public void setColor(Color c)
    {
        rightArm.GetComponent<Renderer>().material.color = c;
        leftArm.GetComponent<Renderer>().material.color = c;
        body.GetComponent<Renderer>().material.color = c;
    }
}
