using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelUI : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject openButton;
    private float speed = 0.05f;
    

    // Start is called before the first frame update
    void Start()
    {
        OpenPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        menuPanel.SetActive(true);
        openButton.SetActive(false);
    }

    public void ClosePanel()
    {
        menuPanel.SetActive(false);
        openButton.SetActive(true);
    }
}
