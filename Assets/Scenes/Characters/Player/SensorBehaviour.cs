using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensorBehaviour : MonoBehaviour
{
    public GameObject indicator = null;

    protected GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = null;
        clearIndicator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        target = other.gameObject;
        if (!target.CompareTag("WALL"))
        {
            Text uitext = indicator.GetComponent<Text>();
            uitext.text = String.Format(String.Format("{0}", target.name));
        }
    }

    void OnTriggerExit(Collider other)
    {
        target = null;
        clearIndicator();
    }

    private void clearIndicator()
    {
        Text uitext = indicator.GetComponent<Text>();
        uitext.text = "";
    }

    public GameObject getTarget()
    {
        return target;
    }
}
