using System;
using UnityEngine;

[Serializable]
public class StopAnimation : MonoBehaviour
{
	public virtual void Update()
	{
		if (Input.GetKeyUp("left shift"))
		{
			GetComponent<Animation>().Stop("Run");
		}
	}

	public virtual void Main()
	{
	}
}
