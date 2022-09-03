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

    private int launch;
    private int maxLaunch = 6;

    public float maxTimer = 0.5f;
    private float time;

    private PhotonView photonView;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        photonView = gameObject.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();
        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
        launch = maxLaunch;
        time = maxTimer;
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
        time +=Time.deltaTime;
    }

    //
    public void Action( GameObject targetPoint )
    {
        Debug.Log("launch[" + launch + "] > 0");

        if (launch > 0 && time > maxTimer)
        {
           //owner.GetComponent<PhotonView>().RPC(nameof(CreateBullet), RpcTarget.AllBuffered);
            //this.photonView.RPC(nameof(CreateBullet), RpcTarget.AllBuffered);
            CreateBullet();
            time = 0.0f;
            //launch--;
        }
        else
        {
            return;
        }
    }

    [PunRPC]
    private void CreateBullet()
    {
        //GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
        GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, muzzle.transform.position, muzzle.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * initialVelocity;
    }

    public void addLaunch()
    {
        launch = maxLaunch;
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
