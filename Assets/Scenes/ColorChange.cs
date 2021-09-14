using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ColorChange : MonoBehaviour
{
    private GameObject redslider, greenslider, blueslider, alphaslider;
    private GameObject colPanel;
    private Color objColor;

    // Start is called before the first frame update
    void Start()
    {
        this.redslider = GameObject.Find("RedSlider");
        this.greenslider = GameObject.Find("GreenSlider");
        this.blueslider = GameObject.Find("BlueSlider");
        this.colPanel = GameObject.Find("colorPanel");
    }

    // Update is called once per frame
    void Update()
    {
        float red = this.redslider.GetComponent<Slider>().value;
        float green = this.greenslider.GetComponent<Slider>().value;
        float blue = this.blueslider.GetComponent<Slider>().value;
        this.colPanel.GetComponent<Image>().color = new Color(red, green, blue, 1);
        objColor = new Color(red, green, blue, 1);
    }

    public Color getColor()
    {
        return objColor;
    }

    [PunRPC]
    public void setColor( GameObject player)
    {
        foreach (Transform childTransform in player.transform)
        {
            
            //Debug.Log("�q�I�u�W�F�N�g:" + childTransform.gameObject.name); // �q�I�u�W�F�N�g�����o��
            foreach (Transform grandChildTransform in childTransform)
            {
               // Debug.Log("���I�u�W�F�N�g:" + grandChildTransform.gameObject.name); // ���I�u�W�F�N�g�����o��
                if (grandChildTransform.gameObject.name == "Body")
                {
                    grandChildTransform.gameObject.GetComponent<Renderer>().material.color = getColor();
                }

                if(grandChildTransform.gameObject.name == "RightHand")
                {
                    foreach (Transform grandChild2Transform in grandChildTransform)
                    {
                        foreach (Transform grandChild3Transform in grandChild2Transform)
                        {
                            foreach (Transform grandChild4Transform in grandChild3Transform)
                            {
                                foreach (Transform grandChild5Transform in grandChild4Transform)
                                {
                                    if (grandChild5Transform.gameObject.name == "ID20")
                                    {
                                        grandChild5Transform.gameObject.GetComponent<Renderer>().material.color = getColor();
                                    }
                                }
                            }
                        }
                    }
                }

                if (grandChildTransform.gameObject.name == "LeftHand")
                {
                    foreach (Transform grandChild2Transform in grandChildTransform)
                    {
                        foreach (Transform grandChild3Transform in grandChild2Transform)
                        {
                            foreach (Transform grandChild4Transform in grandChild3Transform)
                            {
                                foreach (Transform grandChild5Transform in grandChild4Transform)
                                {
                                    if (grandChild5Transform.gameObject.name == "ID20")
                                    {
                                        grandChild5Transform.gameObject.GetComponent<Renderer>().material.color = getColor();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            

        }
    }
}
