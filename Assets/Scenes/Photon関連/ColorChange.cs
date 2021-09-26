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
}
