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

    private PhotonView photonView;
    public GameObject Light;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void SetWeather(int number)
    {
        switch( number)
        {
            case STATE_MORNING:
                ang = Light.transform.localEulerAngles;
                ang.x = 5;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_NOON:
                ang = Light.transform.localEulerAngles;
                ang.x = 90;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_EVENING:
                ang = Light.transform.localEulerAngles;
                ang.x = 180;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_NIGHT:
                ang = Light.transform.localEulerAngles;
                ang.x = 225;
                Light.transform.localEulerAngles = ang;
                break;

            case STATE_CURRENT:
                break;
        }
    }
}
