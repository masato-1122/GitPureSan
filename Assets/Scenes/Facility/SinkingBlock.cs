using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingBlock : MonoBehaviour
{
	private Rigidbody rigidBody;
	private Vector3 defaultPosition;
	private Vector3 velocity;
	[SerializeField]
	private float returnSpeed = 2f;
	[SerializeField]
	private float sinkingSpeed = 2f;
	private bool characterIsOnBoard;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = this.GetComponent<Rigidbody>();
		defaultPosition = rigidBody.position;
	}

	void OnCollisionStay(Collision collision)
    {
		if( collision.gameObject.tag == ("Player"))
        {
			characterIsOnBoard = true;

		}
		/*
		if (collision.gameObject.tag == "WALL")
		{
			velocity = Vector3.zero;
		}
		*/
	}

	private void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			characterIsOnBoard = false;
		}
	}


	void Update()
	{
		if (!characterIsOnBoard)
        {
			velocity = Vector3.up * returnSpeed;
		}
        else
        {
			velocity = Vector3.down * sinkingSpeed;
		}
	}

	private void FixedUpdate()
	{
		//�@�ړ���������i���ɖ߂�����j�ŏ����ʒu�ȏ�̏ꍇ�͈ړ������Ȃ�
		if (velocity.y > 0f && rigidBody.position.y >= defaultPosition.y)
		{
			//�@���S�Ɉړ�������
			rigidBody.MovePosition(defaultPosition);
			return;
		}
		rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
	}
}
