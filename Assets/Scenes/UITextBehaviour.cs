using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextBehaviour : MonoBehaviour
{
    public GameObject uiobject = null;
    public Text msg = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Text uitext = uiobject.GetComponent<Text>();
        uitext.text = String.Format("Zombies: {0}\nHP: {1}\nAmmo: {2}\nKill: {3}", ZombieBehaviour.numberOfZombies,
                                                                                   PlayerBehaviour.hp,
                                                                                   PlayerBehaviour.ammo,
                                                                                   PlayerBehaviour.kills);

        if( PlayerBehaviour.hp < 0 )
        {
            msg.text = "Game Over";
        }
    }
}