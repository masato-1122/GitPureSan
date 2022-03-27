using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeBehaviour : MonoBehaviour
{
    DateTime today;
    private float rotateSpeed = 1f;
    private Vector3 rot = new Vector3(270f, 330f, 0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        today = DateTime.Now;
        Debug.Log(today.Hour.ToString());
        */
        transform.Rotate(new Vector3(0.05f, 0, 0));
    }
}
