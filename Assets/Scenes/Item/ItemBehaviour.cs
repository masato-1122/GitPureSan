using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// アイテム基本クラス
/// </summary>
public class ItemBehaviour : MonoBehaviourPunCallbacks
{
    public const int STATE_NORMAL = 0;
    public const int STATE_ABANDONED = 101;
    public const int STATE_OWNED = 102;
    public const int STATE_USING = 103;

    public const string ATTRIB_OWNABLE = "ownable";          // 所有可能
    public const string ATTRIB_ABANDONABLE = "abandonable";  // 放棄可能
    public const string ATTRIB_INSTALLABLE = "installable";  // 設置可能
    public const string ATTRIB_THROWABLE = "throwable";      // 投射可能
    public const string ATTRIB_BREAKABLE = "breakable";      // 破壊可能

    protected int state;
    protected bool targeted;
    protected Vector3 heldAngle;
    protected Vector3 heldSize;

    protected GameObject owner;
    public PhotonView playerPhoton;

    // Attributes
    protected Dictionary<string, string> attributes;

    // Start is called before the first frame update
    protected void Start()
    {
        state = STATE_NORMAL;
        attributes = new Dictionary<string, string>();
        targeted = false;
        heldAngle = Vector3.zero;
        heldSize = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    protected void Update()
    {
        try
        {
            if (targeted)
            {
                //GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                //GetComponent<Renderer>().material.color = Color.gray;
            }
        }
        catch( MissingComponentException )
        {
            // プリミティブ以外はRendererがない
        }

        switch (state)
        {
            case STATE_NORMAL:
                stateNormal();
                break;
            case STATE_ABANDONED:
                stateAbandoned();
                break;
            case STATE_OWNED:
                stateOwned();
                break;
            case STATE_USING:
                stateUsing();
                break;
        }
        targeted = false;
    }



    // 状態：通常
    protected void stateNormal()
    {

    }

    // 状態：放置
    protected void stateAbandoned()
    {
        transform.Rotate(new Vector3(0f, 90f, 0f) * Time.deltaTime, Space.World);
        transform.Translate(new Vector3(0f, Mathf.Sin(10 * Time.time) * 0.5f, 0f) * Time.deltaTime, Space.World);
    }

    // 状態：取得済み
    protected void stateOwned()
    {

    }

    // 状態：使用中
    protected void stateUsing()
    {
    }

    // 放置する
    public void SetAbandoned()
    {
        state = STATE_ABANDONED;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // 取得する
    public void SetOwned()
    {
        state = STATE_OWNED;
        gameObject.SetActive(false);
    }

    // 使用中にする
    public void SetUsing()
    {
        state = STATE_USING;
        gameObject.SetActive(true);
        gameObject.transform.localScale = heldSize;

    }

    // アイテムの機能を実行
    public void Action()
    {
        Debug.Log("Item Action");
    }

    // アイテムの機能をオブジェクトを対象に実行
    public void ActionForTargetedObject( GameObject target )
    {
        Debug.Log("Item Action for " + target.name);
    }

    // 属性を設定する
    public void SetAttribute( string k )
    {
        attributes.Add(k, "exists");
    }

    // 属性があるかチェックする
    public bool hasAttribute( string k )
    {
        return attributes.ContainsKey(k);
    }

    // ターゲティング
    public void Targeted()
    {
        targeted = true;
    }

    //Photonコンポーネントの主勇者譲渡関数
    public void SetOwner(PhotonView view)
    {
        GetComponent<PhotonView>().TransferOwnership(view.OwnerActorNr);
        Debug.Log("譲渡しました。譲渡ID:"+ view.OwnerActorNr);
    }

    public Vector3 GetHeldAngle()
    {
        return heldAngle;
    }

    // 放置されているか？
    public bool IsAbandoned()
    {
        if(state==STATE_ABANDONED)
        {
            return true;
        }
        return false;
    }

    public void SetOwner(GameObject player)
    {
        owner = player;
        playerPhoton = player.GetComponent<PhotonView>();
    }
}
