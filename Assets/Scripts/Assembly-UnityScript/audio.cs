using System;
using UnityEngine;

[Serializable]
public class audio : MonoBehaviour
{
	public virtual void OnTriggerEnter()
	{
		GetComponent<AudioSource>().Play();
	}

	public virtual void OnTriggerExit()
	{
		GetComponent<AudioSource>().Stop();
	}

	public virtual void Main()
	{
	}
}
