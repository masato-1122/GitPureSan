using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class AxeBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public const int STATE_ATTACK = 1;
    public const int STATE_BREAK = 2;

    protected float now;
    protected GameObject targetObject;

    public GameObject particle = null;

    private PhotonView photonView;
    private Rigidbody rigidbody;
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }

        rigidbody = this.GetComponent<Rigidbody>();

        now = 0.0f;
        targetObject = null;

        // 属性の設定
        SetAttribute(ATTRIB_OWNABLE);
        SetAttribute(ATTRIB_ABANDONABLE);
        SetAbandoned();

        heldAngle = new Vector3(90.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        base.Update();

        switch(state)
        {
            case STATE_ATTACK:
                stateAttack();
                break;
            case STATE_BREAK:
                stateBreak();
                break;
        }
    }

    
    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        else
        {
            this.rigidbody.position = Vector3.MoveTowards(rigidbody.position, networkPosition, Time.fixedDeltaTime);
            this.rigidbody.rotation = Quaternion.RotateTowards(rigidbody.rotation, networkRotation, Time.fixedDeltaTime * 100.0f);
        }
    }
    

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.rigidbody.position);
            stream.SendNext(this.rigidbody.rotation);
            Debug.Log("Streaming");
        }
        else
        {
            this.rigidbody.position = (Vector3)stream.ReceiveNext();
            this.rigidbody.rotation = (Quaternion)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            networkPosition += (this.rigidbody.velocity * lag);
        }
    }

    void stateAttack()
    {
        now += Time.deltaTime;
        if( now >= 0.1f )
        {
            state = STATE_NORMAL;
            // 斧を戻す
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            now = 0.0f;
        }
    }

    void stateBreak()
    {
        now += Time.deltaTime;
        if(now>=0.1f)
        {
            // ダメージ処理 攻撃した対象にメッセージされる
            ExecuteEvents.Execute<ItemReceiveMessage>(
                target: targetObject,
                eventData: null,
                functor: (receiver, eventData) => receiver.Damaged(gameObject)
                ) ;
            // 斧を戻す
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

            state = STATE_NORMAL;
            now = 0.0f;
        }
    }

    void OnTriggerEnter( Collider other )
    {

        //ゾンビ相手だからいらない　　
        if( state == STATE_ATTACK && other.CompareTag("ENEMY") )
        {
            GameObject zombie = other.gameObject;
            //ExecuteEvents オブジェクトに伝える
            ExecuteEvents.Execute<ReceiveMessage>(
                target: zombie,
                eventData: null,
                functor: (receiver, eventData) => receiver.setDead());
        }
    }

    public void Action( GameObject targetPoint )
    {
        // 斧を振り下ろす
        if (state != STATE_ATTACK)
        {
            state = STATE_ATTACK;
            transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
        }
    }

    public void ActionForTargetedObject( GameObject target )
    {
        targetObject = target;
        if(state == STATE_NORMAL)
        {
            state = STATE_BREAK;
            transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
        }
    }

    //
    public void Damaged(GameObject attacker)
    {
        // Nothing
    }
}
