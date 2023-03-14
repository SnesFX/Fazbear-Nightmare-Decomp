using System;
using UnityEngine;

[Serializable]
public class play_0020anim : MonoBehaviour
{
	public AnimationClip AnimationClip;

	public virtual void Update()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			GetComponent<Animation>().Play();
		}
	}

	public virtual void Main()
	{
	}
}
