using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour, ReceiveMessage
{
    static public int numberOfZombies = 0;

    public const int WALK = 0;
    public const int DEAD = 1;
    public const int ATTACK = 2;
    public const int ATTACK_WAIT = 3;

    protected string[] stateName = { "WALK", "DEAD", "ATTACK", "ATKWAIT" };


    public TextMesh stateBoard = null;
    public float attackSpeed = 1.5f; // 攻撃時の突撃速度 

    protected int state;

    protected int count;
    protected Vector3 direction;
    protected int wait;
    protected int attackWaitCount;
    protected int hitCount;
    protected int hitFlag;

    protected GameObject player;

    private float now;

    // Sight
    protected Collider sight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        hitCount = 0;
        hitFlag = 0;

        now = 0.0f;
        count = 0;
        direction = new Vector3();

        // 視野(sight)の取得
        Component[]  temp = GetComponentsInChildren(typeof(BoxCollider));
        for( int i = 0; i < temp.Length; i++ )
        {
            if( temp[i].name == "Sight" )
            {
                sight = temp[i] as Collider;
                break;
            }
        }

        stateBoard.text = "";

        numberOfZombies = numberOfZombies + 1;
    }

    // Update is called once per frame
    void Update()
    {
        stateBoard.text = String.Format( "{0}[{1}:{2}:{3}]", stateName[state], hitFlag, hitCount, attackWaitCount );
        stateBoard.transform.LookAt(player.transform);
        switch(state)
        {
            case WALK:
                stateWalk();
                break;
            case DEAD:
                stateDead();
                break;
            case ATTACK:
                stateAttack();
                break;
            case ATTACK_WAIT:
                stateAttackWait();
                break;
        }
    }

    private void stateWalk()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();

        count = count - 1;
        if( count < 0 )
        {
            count = (int)UnityEngine.Random.Range(5f, 10f) * 5;
            transform.Rotate( new Vector3(0f, UnityEngine.Random.Range(-90, 90), 0f ) );
        }

        // 転びそうになったら起きようとする
        Vector3 angles = transform.rotation.eulerAngles;
        if (angles.z > 40 )
        {
            transform.Rotate(new Vector3(-angles.x, 0f, -angles.z));
        }

        // 前へ進む
        rb.transform.Translate(new Vector3( 0f, 0f, 1f) * Time.deltaTime );
    }

    private void stateDead()
    {
        wait = wait - 1;
        if( wait < 0 )
        {
            Destroy(gameObject);
            numberOfZombies = numberOfZombies - 1;
            PlayerBehaviour.kills = PlayerBehaviour.kills + 1;
        }
    }

    public void stateAttack()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        transform.LookAt(player.transform);
        rb.transform.Translate(Vector3.forward * Time.deltaTime * attackSpeed);
    }

    public void stateAttackWait()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        transform.LookAt(player.transform);
        //rb.transform.Translate(Vector3.forward * Time.deltaTime * attackSpeed);  // 無い方が良さげ

        attackWaitCount = attackWaitCount - 1;
        if (attackWaitCount <= 0)
        {
            hitFlag = 0;
            state = ATTACK;
        }
    }

    public void setDead()
    {
        if (state != DEAD)
        {
            state = DEAD;
            wait = 25;
        }
    }
   
    void OnTriggerEnter( Collider other )
    {
        if( state != DEAD && other.CompareTag("Player") )
        {
            state = ATTACK;
        }
    }

    void OnTriggerExit( Collider other )
    {
        if( state != DEAD && other.CompareTag("Player") )
        {
            state = WALK;
        }
    }

    void OnCollisionEnter( Collision cInfo )
    {
        if (state == ATTACK)
        {
            if (cInfo.collider.name == "Player")
            {
                hitFlag = 1;
                hitCount = hitCount + 1;
                attackWaitCount = 30;
                PlayerBehaviour.hp = PlayerBehaviour.hp - UnityEngine.Random.Range( 1, 10 );
                Rigidbody rb = GetComponent<Rigidbody>();
                Vector3 curRotation = rb.transform.rotation.eulerAngles;
                rb.transform.rotation = Quaternion.Euler( new Vector3(0f, curRotation.y, 0f) );
                rb.AddForce(-transform.forward * 3f, ForceMode.VelocityChange);
                state = ATTACK_WAIT;
            }
        }
    }
}
