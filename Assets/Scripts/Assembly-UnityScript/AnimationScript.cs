using System;
using UnityEngine;

[Serializable]
public class AnimationScript : MonoBehaviour
{
	public virtual void Update()
	{
		if (Input.GetKeyDown("left shift"))
		{
			GetComponent<Animation>().CrossFade("Run");
		}
	}

	public virtual void Main()
	{
	}
}
