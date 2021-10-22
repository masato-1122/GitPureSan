using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerBehaviour : MonoBehaviour
{
    public const int STATE_NORMAL = 0;
    public const int STATE_PUNCH = 1;

    protected int state;

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

    public GameObject canvas;
    public Text nameText;

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

        rightHand = GameObject.Find("RightHand");
        leftHand = GameObject.Find("LeftHand");

        camera = mainCamera;
        dbboard = GameObject.FindWithTag("DDBoard");
        indicator = GameObject.FindWithTag("indicator");

        this.mainCamera.depth = 0;
        this.tpCamera.depth = 0;

        photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
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

        Text dbb = dbboard.GetComponent<Text>();
        dbb.text = leftHand.GetComponent<HandBehaviour>().GetSlotList() + "\n" + rightHand.GetComponent<HandBehaviour>().GetSlotList()
            + "\n" + (target == null ? "None" : target.name)
            + "\n" + x + "," + y + "," + z; 

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

        if (Input.GetKeyUp(KeyCode.O))
        {
            photonView.RPC("setClothColor", RpcTarget.AllBuffered,photonManager.clone);
        }
    }

    private void clearIndicator()
    {
        Text uitext = indicator.GetComponent<Text>();
        uitext.text = "";
    }

    [PunRPC]
    public void setName(string n)
    {
        nameText.text = n;
    }

    /*
    //プレイヤーの体の色を変化
    [PunRPC]
    public void setBodyColor()
    {
        Color playerBodyColor = 
        GameObject clone = GameObject.Find("PhotonManager").GetComponent<PhotonManager>().clone;
        foreach( Transform childTransform in clone.transform)
        {
            foreach( Transform grandChildTransform in childTransform)
            {
                if( grandChildTransform.gameObject.name == "Head")
                {
                    grandChildTransform.gameObject.GetComponent<Renderer>().material.color = bodyColor;
                }
            }
        }
    }
    */

    //プレイヤーの服の色が変化
    [PunRPC]
    public void setClothColor(GameObject clone)
    {
        Color playerColor = GameObject.Find("PhotonManager").GetComponent<PhotonManager>().getClothColor();
        //GameObject clone = GameObject.Find("PhotonManager").GetComponent<PhotonManager>().clone;
        foreach (Transform childTransform in clone.transform)
        {
            //Debug.Log("子オブジェクト:" + childTransform.gameObject.name); // 子オブジェクト名を出力
            foreach (Transform grandChildTransform in childTransform)
            {
                // Debug.Log("孫オブジェクト:" + grandChildTransform.gameObject.name); // 孫オブジェクト名を出力
                if (grandChildTransform.gameObject.name == "Body")
                {
                    grandChildTransform.gameObject.GetComponent<Renderer>().material.color = playerColor;
                }

                if (grandChildTransform.gameObject.name == "RightHand")
                {
                    foreach (Transform grandChild2Transform in grandChildTransform)
                    {
                        foreach (Transform grandChild3Transform in grandChild2Transform)
                        {
                            foreach (Transform grandChild4Transform in grandChild3Transform)
                            {
                                foreach (Transform grandChild5Transform in grandChild4Transform)
                                {
                                    if (grandChild5Transform.gameObject.name == "ID20")
                                    {
                                        grandChild5Transform.gameObject.GetComponent<Renderer>().material.color = playerColor;
                                    }
                                }
                            }
                        }
                    }
                }

                if (grandChildTransform.gameObject.name == "LeftHand")
                {
                    foreach (Transform grandChild2Transform in grandChildTransform)
                    {
                        foreach (Transform grandChild3Transform in grandChild2Transform)
                        {
                            foreach (Transform grandChild4Transform in grandChild3Transform)
                            {
                                foreach (Transform grandChild5Transform in grandChild4Transform)
                                {
                                    if (grandChild5Transform.gameObject.name == "ID20")
                                    {
                                        grandChild5Transform.gameObject.GetComponent<Renderer>().material.color = playerColor;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

    }

}
