using System;
using UnityEngine;

[Serializable]
public class rotater : MonoBehaviour
{
	public float xSpeed;

	public float ySpeed;

	public float zSpeed;

	public bool manual;

	public rotater()
	{
		xSpeed = 1f;
		ySpeed = 1f;
		zSpeed = 1f;
	}

	public virtual void Update()
	{
		if (!manual)
		{
			transform.RotateAround(transform.position, Vector3.right, ySpeed * Time.deltaTime);
			transform.RotateAround(transform.position, Vector3.up, xSpeed * Time.deltaTime);
			transform.RotateAround(transform.position, Vector3.forward, zSpeed * Time.deltaTime);
			return;
		}
		if (Input.GetAxis("Horizontal") != 0f)
		{
			transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Horizontal") * xSpeed * Time.deltaTime);
		}
		if (Input.GetAxis("Vertical") != 0f)
		{
			transform.RotateAround(transform.position, Vector3.right, Input.GetAxis("Vertical") * ySpeed * Time.deltaTime);
		}
	}

	public virtual void Main()
	{
	}
}
