using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPointBehaviour : MonoBehaviour
{
    public GameObject spawnPoint = null;  // 出現ポイントオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter( Collider c )
    {
        if (c.gameObject.CompareTag("Player"))
        {
            spawnPoint.GetComponent<SpawnPointBehaviour>().Spawn(c.gameObject);
        }
        else if( c.gameObject.CompareTag("OBJECT"))
        {
            if(c.gameObject.GetComponent<ItemBehaviour>().IsAbandoned())
            {
                spawnPoint.GetComponent<SpawnPointBehaviour>().Spawn(c.gameObject);
            }
        }
    }
}
