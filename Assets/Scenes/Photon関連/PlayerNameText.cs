using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    public Transform target;
    private Vector3 screenPoint;

    void LateUpdate()
    {
        //screenPoint = Camera.main.WorldToScreenPoint(target.position);
        transform.position = new Vector3(screenPoint.x, screenPoint.y, 0);
    }

    public void setTarget(GameObject g, string s)
    {
        target = g.transform;
        this.gameObject.GetComponent<Text>().text = s;
    }
}
