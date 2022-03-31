using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class WeatherBehaviour : MonoBehaviour
{
    private Vector3 ang;

    public const int STATE_MORNING = 1;
    public const int STATE_NOON = 2;
    public const int STATE_EVENING = 3;
    public const int STATE_NIGHT = 4;
    public const int STATE_CURRENT = 5;
    public const int STATE_ACCE = 6;

    private PhotonView photonView;
    public GameObject Light;

    public bool rotationFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }


    [PunRPC]
    public void SetWeather(int number)
    {
        switch( number)
        {
            case STATE_MORNING:
                rotationFlag = false;
                ang = Light.transform.localEulerAngles;
                ang.x = 5;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_NOON:
                rotationFlag = false;
                ang = Light.transform.localEulerAngles;
                ang.x = 90;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_EVENING:
                rotationFlag = false;
                ang = Light.transform.localEulerAngles;
                ang.x = 180;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_NIGHT:
                rotationFlag = false;
                ang = Light.transform.localEulerAngles;
                ang.x = 270;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_CURRENT:
                rotationFlag = false;
                int hour = System.DateTime.Now.Hour;
                //’©
                if( hour < 8)
                {
                    ang = Light.transform.localEulerAngles;
                    ang.x = 5;
                    Light.transform.localEulerAngles = ang;
                }
                //’‹
                else if( hour < 16)
                {
                    ang = Light.transform.localEulerAngles;
                    ang.x = 90;
                    Light.transform.localEulerAngles = ang;
                }
                //—[•û
                else if( hour < 18)
                {
                    ang = Light.transform.localEulerAngles;
                    ang.x = 180;
                    Light.transform.localEulerAngles = ang;
                }
                //–é
                else if( hour < 22)
                {
                    ang = Light.transform.localEulerAngles;
                    ang.x = 225;
                    Light.transform.localEulerAngles = ang;
                }
                break;

            case STATE_ACCE:
                rotationFlag = true;
                break;
        }
    }

    void Update()
    {
        if( rotationFlag)
        {
            Light.transform.Rotate(new Vector3(0.05f, 0, 0));
        }
    }
}
