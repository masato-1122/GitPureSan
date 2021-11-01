using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private Vector3 defaultPos;
    public float limit;
    public float speed;
    private int state;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(0f, speed, 0f);
        if (transform.position.y > defaultPos.y + limit)
        {
            speed *= -1;
        }
        else if (transform.position.y < defaultPos.y)
        {
            speed = Mathf.Abs(speed);
        }
    }
}
