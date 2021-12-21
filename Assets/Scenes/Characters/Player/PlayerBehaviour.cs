using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerBehaviour : MonoBehaviour
{
    protected int state;

    public const int STATE_NORMAL = 0;
    public const int STATE_OVER= 1;

    public Camera camera;
    public GameObject dbboard = null;  // デバッグ表示

    //現体力、最大体力
    public static int hp;
    public static int maxHp = 50;
    public static int ammo;
    public static int maxAmmo = 20;
    public static int kills = 0;

    // Hand
    protected GameObject rightHand;
    protected GameObject leftHand;

    // Sensor
    public GameObject indicator = null;
    protected GameObject target;
    public GameObject sensorPoint = null;

    public Camera mainCamera = null;
    public Camera tpCamera = null;

    private PhotonManager photonManager;
    public PhotonView photonView;

    private GameObject canvas;
    public Text nameText;

    private Rigidbody rigidbody;
    private Vector3 networkPosition;
    private Quaternion networkRotation;

    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        photonView = this.GetComponent<PhotonView>();
        if (!photonView.IsMine)
        {
            return;
        }
        hp = maxHp;
        state = STATE_NORMAL;

        rigidbody = GetComponent<Rigidbody>(); ;
        rightHand = GameObject.Find("RightHand");
        leftHand = GameObject.Find("LeftHand");

        camera = mainCamera;
        dbboard = GameObject.FindWithTag("DDBoard");
        indicator = GameObject.FindWithTag("indicator");

        this.mainCamera.depth = 0;
        this.tpCamera.depth = 0;

        canvas = GameObject.Find("Canvas");
        canvas.GetComponent<Canvas>().worldCamera = this.camera;

        photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
        SetColor(photonManager.getClothColor());

        //名前テキストの生成と表示
        GameObject clone = PhotonNetwork.Instantiate(nameText.name, gameObject.transform.position, Quaternion.identity);
        clone.transform.parent = canvas.transform;
        Vector3 pointTemp = camera.WorldToScreenPoint(Vector3.zero);
        pointTemp.z = 0f;
        clone.transform.position = pointTemp;
    }


    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

        // 3rd Person Camera時の遮蔽物削除
        /*
        // 前回、非表示したものを表示する
        Vector3 temp = tpCamera.transform.position - transform.position;
        Vector3 normal = temp.normalized;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, normal, 10);
        foreach (RaycastHit hitobj in hits)
        {
            // 子オブジェクトを検索するようにする＆非表示したものをリストで記憶しておく
            GameObject obj = hitobj.collider.gameObject;
            try
            {
                obj.GetComponent<Renderer>().enabled = false;
            }
            catch( MissingComponentException )
            {
                obj.GetComponent<MeshRenderer>().enabled = false;
            }
        }
        */

        switch( state)
        {
            case STATE_NORMAL:
                stateNormal();
                break;
            case STATE_OVER:
                stateOver();
                break;
        }

        if( hp <= 0)
        {
            hp = 0;
            state = STATE_OVER;
        }

        // アクションターゲットへのレイキャスト処理
        Text uitext = indicator.GetComponent<Text>();
        uitext.text = "";
        target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 10.0f))
        {
            target = hit.collider.gameObject;
            Vector3 selfPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float distance = Vector3.Distance(selfPos, targetPos);
            if (distance <= 3.0)
            {
                if (target.CompareTag("OBJECT"))
                {
                    target.GetComponent<ItemBehaviour>().Targeted();
                    uitext.text = String.Format(String.Format("{0}", target.name));
                }
            }
            else
            {
                target = null;
            }
        }

        int x = (int)sensorPoint.transform.position.x;
        int y = (int)sensorPoint.transform.position.y;
        int z = (int)sensorPoint.transform.position.z;

        /*
        Text dbb = dbboard.GetComponent<Text>();
        
        dbb.text = leftHand.GetComponent<HandBehaviour>().GetSlotList() + "\n" + rightHand.GetComponent<HandBehaviour>().GetSlotList()
            + "\n" + (target == null ? "None" : target.name)
            + "\n" + x + "," + y + "," + z; 
        */

        // Action-A
        // 左手(Action)
        if (Input.GetButtonDown("Fire1"))
        {
            if (target != null && !target.CompareTag("WALL"))
            {
                // ターゲティングされている
                //
                leftHand.GetComponent<HandBehaviour>().ActionForTargetedObject(target);
            }
            else
            {
                // ターゲティングされてない
                //手に持っているアイテムの動作を行う
                leftHand.GetComponent<HandBehaviour>().Action();
            }
        }
        // 右手(Action)
        if (Input.GetButtonDown("Fire2"))
        {
            if(target != null && !target.CompareTag("WALL"))
            {
                // ターゲティングされている
                rightHand.GetComponent<HandBehaviour>().ActionForTargetedObject(target);
            }
            else
            {
                // ターゲティングされていない
                rightHand.GetComponent<HandBehaviour>().Action();
            }
        }

        // Action-B
        // 左手(Item変更)
        if (Input.GetKeyUp(KeyCode.Q))
        {
            leftHand.GetComponent<HandBehaviour>().nextSlot();
        }

        // 右手(Item変更)
        if (Input.GetKeyUp(KeyCode.E))
        {
            rightHand.GetComponent<HandBehaviour>().nextSlot();
        }

        // Action-C
        // 左手(Item放出)
        if ( Input.GetKeyUp(KeyCode.Z))
        {
            GameObject dropItem = leftHand.GetComponent<HandBehaviour>().DropItem();
            if( dropItem != null )
            {
                dropItem.transform.parent = null;
                dropItem.transform.position = sensorPoint.transform.position;
                dropItem.transform.rotation = Quaternion.identity;
                dropItem.GetComponent<ItemBehaviour>().SetAbandoned();
            }
        }
        // 右手(Item放出)
        if( Input.GetKeyUp(KeyCode.C))
        {
            GameObject dropItem = rightHand.GetComponent<HandBehaviour>().DropItem();
            if (dropItem != null)
            {
                dropItem.transform.parent = null;
                dropItem.transform.position = sensorPoint.transform.position;
                dropItem.transform.rotation = Quaternion.identity;
                dropItem.GetComponent<ItemBehaviour>().SetAbandoned();
            }
        }

        // カメラの切替
        if(Input.GetKeyUp(KeyCode.P))
        {
            if(camera==mainCamera)
            {
                mainCamera.enabled = false;
                tpCamera.enabled = true;
                camera = tpCamera;
            }
            else
            {
                mainCamera.enabled = true;
                tpCamera.enabled = false;
                camera = mainCamera;
            }
        }
    }

    public void Damaged(GameObject attacker)
    {
        hp -= 5;
        Debug.Log("プレイヤーの現在体力：" + hp);
    }

    private void stateOver()
    {

    }

    public void stateNormal()
    {

    }

    private void clearIndicator()
    {
        Text uitext = indicator.GetComponent<Text>();
        uitext.text = "";
    }

        [PunRPC]
    public void SetName(string n)
    {
        nameText.text = n;
    }


    public void SetColor(Color c)
    {
        rightArm.GetComponent<Renderer>().material.color = c;
        leftArm.GetComponent<Renderer>().material.color = c;
        body.GetComponent<Renderer>().material.color = c;
    }

    public GameObject GetRightHandItem()
    {
        return this.rightHand;
    }

    public GameObject GetLeftHandItem()
    {
        return this.leftHand;
    }
}
