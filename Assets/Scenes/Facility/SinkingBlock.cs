using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingBlock : MonoBehaviour
{
	private Rigidbody rigidBody;
	private Vector3 defaultPosition;
	private Vector3 velocity;
	[SerializeField]
	private float rayDistance = 1f;
	private Collider myCollider;
	[SerializeField]
	private Collider floorCollider;
	[SerializeField]
	private float returnSpeed = 0.1f;
	[SerializeField]
	private float sinkingSpeed = 2f;
	private bool characterIsOnBoard;
	[SerializeField]
	private Vector3 blockSize = Vector3.one;

	// Start is called before the first frame update
	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
		defaultPosition = rigidBody.position;
		myCollider = GetComponent<Collider>();
		Physics.IgnoreCollision(myCollider, floorCollider, true);
	}

	// Update is called once per frame
	void Update()
	{
		if (!characterIsOnBoard)
		{
			if (Physics.CheckBox(transform.position + Vector3.up * rayDistance, blockSize * 0.52f, Quaternion.identity, LayerMask.GetMask("Player")))
			{
				velocity = Vector3.down * sinkingSpeed;
				characterIsOnBoard = true;
			}
			else
			{
				velocity = Vector3.up * returnSpeed;
			}
			//　キャラクターが乗っている時
		}
		else
		{
			// キャラクターが乗っているとされている時に、ボックスとキャラクターが接触しているか確認し、していなければ乗っていないに変更
			if (!Physics.CheckBox(transform.position + Vector3.up * rayDistance, blockSize * 0.52f, Quaternion.identity, LayerMask.GetMask("Player")))
			{
				characterIsOnBoard = false;
			}
		}
	}
	private void FixedUpdate()
	{
		//　移動が上向き（元に戻る向き）で初期位置以上の場合は移動させない
		if (velocity.y > 0f
			&& rigidBody.position.y >= defaultPosition.y
			)
		{
			//　完全に移動させる
			rigidBody.MovePosition(defaultPosition);
			return;
		}
		rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
	}
}
