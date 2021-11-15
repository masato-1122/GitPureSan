using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWarpPointBehaviour : MonoBehaviour
{
    public GameObject[] point;
    private int turn;
    private int maxPoint;
    // Start is called before the first frame update
    void Start()
    {
        turn = 0;
        maxPoint = point.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            point[turn].GetComponent<SpawnPointBehaviour>().Spawn(c.gameObject);
        }
        else if (c.gameObject.CompareTag("OBJECT"))
        {
            if (c.gameObject.GetComponent<ItemBehaviour>().IsAbandoned())
            {
                point[turn].GetComponent<SpawnPointBehaviour>().Spawn(c.gameObject);
            }
        }

        turn++;
        if( turn >= maxPoint)
        {
            turn = 0;
        }
    }
}
