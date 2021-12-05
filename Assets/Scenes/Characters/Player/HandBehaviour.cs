using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandBehaviour : MonoBehaviour, HandReceiveMessage
{
    public const int STATE_NORMAL = 0;
    public const int STATE_PUNCH = 1;
    public const int STATE_TAKEOBJECT = 2;

    protected int state;
    protected float now;

    // ターゲットぽいんt
    public GameObject targetPoint = null;

    // センサーに入っているオブジェクト
    protected GameObject targetObject;

    // 把持アイテム
    public GameObject palm;  // 手のひらオブジェクト
    public GameObject item;  // 現在、把持しているアイテム
    public GameObject defaultItem;  // アイテムを手放したときに補充するアイテム

    // アイテムスロット
    protected GameObject[] slot;
    protected int slotIndex;

    // Start is called before the first frame update
    void Start()
    {
        state = STATE_NORMAL;
        now = 0.0f;
        targetObject = null;
        item = null;

        slot = new GameObject[3];
        for( int i = 0; i < slot.Length; i++ )
        {
            slot[i] = Instantiate(defaultItem, palm.transform.position, palm.transform.rotation);
            slot[i].GetComponent<ItemBehaviour>().SetOwned();
        }

        slotIndex = 0;
        SetItemFromSlot();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case STATE_NORMAL:
                stateNormal();
                break;
            case STATE_PUNCH:
                statePunch();
                break;
            case STATE_TAKEOBJECT:
                stateTakeObject();
                break;
        }
    }

    // 状態：通常時
    void stateNormal()
    {
    }

    // 状態：パンチ
    void statePunch()
    {
        now += Time.deltaTime;
        if( now >= 0.1f )
        {
            transform.Translate(0.0f, 0.0f, -1.0f);
            state = STATE_NORMAL;
        }
    }

    // 状態：アイテム取得
    void stateTakeObject()
    {
        now += Time.deltaTime;
        if(now>=0.1f)
        {
            slot[slotIndex] = targetObject;
            SetItemFromSlot();
            item.GetComponent<ItemBehaviour>().SetUsing();
            targetObject = null;
            
            // 手を戻す
            transform.Translate(0.0f, 0.0f, -1.0f);
            state = STATE_NORMAL;

            /*
            for(int i=0;i<slot.Length;i++)
            {
                Debug.Log(slot[i].name);
            }
            Debug.Log("hand:"+item.name);
            */
        }
    }

    // アイテムを捨てる
    public GameObject DropItem()
    {
        GameObject result = item;
        if( item != null && !item.name.Contains("Punch") )
        {
            item = null;
            slot[slotIndex] = Instantiate( defaultItem, palm.transform.position, palm.transform.rotation);
            SetItemFromSlot();
        }
        else
        {
            // 捨てられない場合はnullを返す
            result = null;
        }
        return result;
    }
    
    // 把持アイテムを取得する
    public GameObject GetItem()
    {
        return item;
    }

    // パンチする
    public void Punch()
    {
        if (state == STATE_NORMAL)
        {
            now = 0.0f;
            transform.Translate(0.0f, 0.0f, 1.0f);
            state = STATE_PUNCH;
        }
    }

    // Takeする　空間のアイテムをとるとき

    public void TakeObject( GameObject target )
    {
        if(state == STATE_NORMAL)
        {
            if (target.GetComponent<ItemBehaviour>().hasAttribute(ItemBehaviour.ATTRIB_OWNABLE))
            {
                // 所有できるならTakeを進める
                targetObject = target;
                now = 0.0f;
                transform.Translate(0.0f, 0.0f, 1.0f);
                state = STATE_TAKEOBJECT;
            }
        }
    }

    // 把持アイテムのアクションを実行する
    public void Action()
    {
        //Debug.Log(item.name);
        if (!item.name.Contains("Punch"))
        {   
            //メッセージを送ることでアイテムの行動を行う
            ExecuteEvents.Execute<ItemReceiveMessage>(
                    target: item,
                    eventData: null,
                    functor: (receiver, eventData) => receiver.Action(targetPoint)
                );
        }
        else
        {
            // 手ぶらなのでパンチ
            Punch();
        }
    }

    // ターゲティングしてるアイテムのアクションを実行する
    public void ActionForTargetedObject(GameObject target)
    {
        if (!item.name.Contains("Punch"))
        {
            ExecuteEvents.Execute<ItemReceiveMessage>(
                    target: item,
                    eventData: null,
                    functor: (receiver, eventData) => receiver.ActionForTargetedObject(target)
                );
        }
        else   //手が空いている時
        {
            if (target.CompareTag("OBJECT"))
            {
                // OBJECTなのでTakeする
                TakeObject(target);
            }
            else if(target.CompareTag("FACILITY"))  //ドアなど施設を利用するとき
            {
                // FACILITYなので利用する
                ExecuteEvents.Execute<ItemReceiveMessage>(
                        target: target,
                        eventData: null,
                        functor: (receiver, eventData) => receiver.ActionForTargetedObject(target)
                    ) ;
            }
        }

    }

    public void Damaged(GameObject attacker)
    {
        // Nothing
    }

    // スロットを動かす
    public void nextSlot()
    {
        slotIndex = slotIndex + 1;
        if (slotIndex >= slot.Length)
        {
            slotIndex = 0;
        }
        SetItemFromSlot();
        item.GetComponent<ItemBehaviour>().SetUsing();
    }

    // スロットからアイテムを取り出す
    public void SetItemFromSlot()
    {
        if ( item != null && !item.name.Contains("Punch"))
        {
            item.GetComponent<ItemBehaviour>().SetOwned(); // 片付ける
        }
        item = slot[slotIndex];
        item.transform.SetParent( palm.transform );
        item.transform.position = palm.transform.position;
        item.transform.rotation = palm.transform.rotation;
        Hold();
        //item.GetComponent<ItemBehaviour>().ResetRotation();
        item.GetComponent<ItemBehaviour>().SetUsing();
    }

    // 構える
    public void Hold()
    {
        Vector3 heldAngle = item.GetComponent<ItemBehaviour>().GetHeldAngle();
        palm.transform.localEulerAngles = heldAngle;
    }

    /*
    // ---- for Debug
    public string GetSlotList()
    {
        string result = "";
        for(int i=0;i<slot.Length;i++)
        {
            result += slot[i].name + ":";
        }
        return result;
    }
    */
}
