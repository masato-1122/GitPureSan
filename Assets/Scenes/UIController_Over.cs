using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_Over : MonoBehaviour
{
    [SerializeField]
    private Transform targetTF;
    private RectTransform myRectTF;
    private Vector3 offset = new Vector3(0, 1.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        myRectTF = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myRectTF.position = RectTransformUtility.WorldToScreenPoint(Camera.main, targetTF.position + offset );
    }

    public void lateUpdate()
    {

    }
}
