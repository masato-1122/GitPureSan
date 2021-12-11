using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendReset : MonoBehaviour
{
    GameObject[] vender;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        for(int i = 0; i < vender.Length; i++)
        {
            vender[i].GetComponent<VendingBehaviour>().Reset();
        }
    }
}
