using System;
using UnityEngine;

[Serializable]
public class run_0020script : MonoBehaviour
{
	public float walkSpeed;

	public float crchSpeed;

	public float runSpeed;

	private CharacterMotor chMotor;

	private CharacterController ch;

	private Transform tr;

	private float height;

	public run_0020script()
	{
		walkSpeed = 2.5f;
		crchSpeed = 3f;
		runSpeed = 4f;
	}

	public virtual void Start()
	{
		chMotor = (CharacterMotor)GetComponent(typeof(CharacterMotor));
		tr = transform;
		ch = (CharacterController)GetComponent(typeof(CharacterController));
		height = ch.height;
	}

	public virtual void Update()
	{
		float to = height;
		float maxForwardSpeed = walkSpeed;
		if ((ch.isGrounded && Input.GetKey("left shift")) || Input.GetKey("right shift"))
		{
			maxForwardSpeed = runSpeed;
		}
		if (Input.GetKey("c"))
		{
			to = 0.5f * height;
			maxForwardSpeed = crchSpeed;
		}
		chMotor.movement.maxForwardSpeed = maxForwardSpeed;
		float num = ch.height;
		ch.height = Mathf.Lerp(ch.height, to, 5f * Time.deltaTime);
		float y = tr.position.y + (ch.height - num) / 2f;
		Vector3 position = tr.position;
		float num2 = (position.y = y);
		Vector3 vector2 = (tr.position = position);
	}

	public virtual void Main()
	{
	}
}
