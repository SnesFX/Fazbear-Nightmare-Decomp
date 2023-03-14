using System;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class Flashlight : MonoBehaviour
{
	public Light flight;

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (Input.GetKeyDown("f") || Input.GetMouseButtonDown(1))
		{
			GetComponent<AudioSource>().Play();
			if (flight.enabled)
			{
				flight.enabled = false;
				GetComponent<AudioSource>().Play();
			}
			else
			{
				flight.enabled = true;
			}
		}
	}

	public virtual void Main()
	{
	}
}
