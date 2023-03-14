using System;
using UnityEngine;

[Serializable]
public class GrainInTrigger : MonoBehaviour
{
	private NoiseAndGrain noise;

	public virtual void Start()
	{
		noise = (NoiseAndGrain)GameObject.Find("MainCamera").GetComponent(typeof(NoiseAndGrain));
		noise.enabled = false;
	}

	public virtual void OnTriggerEnter(Collider Col)
	{
		if (Col.tag == "Player")
		{
			noise.enabled = true;
		}
	}

	public virtual void OnTriggerExit(Collider Col)
	{
		if (Col.tag == "Player")
		{
			noise.enabled = false;
		}
	}

	public virtual void Main()
	{
	}
}
