using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameText : MonoBehaviour
{
    public Text nameText;

    public Transform target;
    private Transform targetTransform;
    private Vector3 targetPosition;
    private Vector3 screenPoint;
    private Vector3 ScreenOffset = new Vector3( 0f, 30, 0f);

    void LateUpdate()
    {
        
        this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + ScreenOffset;
    }

    public void setTarget(GameObject g, string s)
    {
        target = g.transform;
        this.gameObject.GetComponent<Text>().text = s;
    }

    void awake()
    {
        this.GetComponent<Transform>().SetParent(GameObject.Find("Canvas").GetComponent<Transform>());
    }

    void Update()
    {
        if( target == null)
        {
            Destroy( this.gameObject);
            return;
        }


    }
}
