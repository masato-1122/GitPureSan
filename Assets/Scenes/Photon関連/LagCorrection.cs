using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//rigidbodyを使用しないオブジェクトの同期スクリプト(ラグ補正)
public class LagCorrection : MonoBehaviour
{
    private PhotonView photonView;
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    private Vector3 movement;
    private float movementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 oldPosition = transform.position;
        movement = transform.position - oldPosition;

        if (!photonView.IsMine)
        {
            transform.position = Vector3.MoveTowards(transform.position, networkPosition, Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, networkRotation, Time.deltaTime * 100);
            return;
        }
    }
}
