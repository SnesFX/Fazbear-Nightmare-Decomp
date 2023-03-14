using System;
using UnityEngine;

[Serializable]
public class Enemy_0020AI : MonoBehaviour
{
	public Transform target;

	public int moveSpeed;

	public int rotationSpeed;

	public float range;

	public float range2;

	public float stop;

	public Transform myTransform;

	public Enemy_0020AI()
	{
		moveSpeed = 3;
		rotationSpeed = 3;
		range = 10f;
		range2 = 10f;
	}

	public virtual void Awake()
	{
		myTransform = transform;
	}

	public virtual void Start()
	{
		target = GameObject.FindWithTag("Player").transform;
	}

	public virtual void Update()
	{
		float num = Vector3.Distance(myTransform.position, target.position);
		if (!(num > range2) && !(num < range))
		{
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), (float)rotationSpeed * Time.deltaTime);
		}
		else if (!(num > range) && !(num <= stop))
		{
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), (float)rotationSpeed * Time.deltaTime);
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		}
		else if (!(num > stop))
		{
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), (float)rotationSpeed * Time.deltaTime);
		}
	}

	public virtual void Main()
	{
	}
}
