using System;
using UnityEngine;

[Serializable]
public class Stop_0020anim : MonoBehaviour
{
	public AnimationClip AnimationClip;

	public virtual void Update()
	{
		if (Input.GetKeyUp(KeyCode.W))
		{
			GetComponent<Animation>().Stop();
		}
	}

	public virtual void Main()
	{
	}
}
