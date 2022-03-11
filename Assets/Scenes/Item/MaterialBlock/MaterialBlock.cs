using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MaterialBlock : ItemBehaviour, ItemReceiveMessage
{
    public GameObject particle;

    protected int hp;
    public const int STATE_INSTALLED = 1;

    public GameObject newBlock;
    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }

        // 属性の設定
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAttribute(ATTRIB_INSTALLABLE);
        SetAttribute(ATTRIB_BREAKABLE);

        heldSize = new Vector3(0.4f, 0.4f, 0.4f);
        hp = 5;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        switch( state )
        {
            case STATE_INSTALLED:
                stateInstalled();
                break;
        }
    }

    protected void stateInstalled()
    {
    }

    public void Install()
    {
        state = STATE_INSTALLED;
    }

    public void Action( GameObject targetPoint )
    {
        Vector3 pos = targetPoint.transform.position;

        newBlock = PhotonNetwork.Instantiate( nameof(MaterialBlock), new Vector3( pos.x, pos.y, pos.z), Quaternion.identity);

        newBlock.GetComponent<MaterialBlock>().Install();
        newBlock.GetComponent<MaterialBlock>().Lock();
        newBlock.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void ActionForTargetedObject(GameObject target)
    {

    }

    public void Damaged(GameObject attacker)
    {
        hp = hp - 1;
        Vector3 offset = transform.position;
        // パーティクル表示
        //GameObject ptl = Instantiate(particle, transform.position, transform.rotation);
        GameObject ptl = PhotonNetwork.Instantiate(particle.name, offset, Quaternion.identity);
        if (hp <= 0)
        {
            ItemDelete();
        }
    }

    public void Lock()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
