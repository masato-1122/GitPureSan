using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GunBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject muzzle = null;
    public GameObject bulletPrefab = null;
    public float initialVelocity = 50.0f;
    private PhotonView photonView;
    private int launch;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();
        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
        launch = 10;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }

    //

    public void Action( GameObject targetPoint )
    {
        if (launch > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
            //GameObject bullet = PhotonNetwork.Instantiate("Bullet", muzzle.transform.position, muzzle.transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * initialVelocity;
            launch--;
        }
    }

    public void addLaunch(int n)
    {
        launch += n;
    }

    public void ActionForTargetedObject( GameObject target )
    {
        Action();
    }

    public void Damaged(GameObject attacker)
    {
        // Nothing
    }
}
