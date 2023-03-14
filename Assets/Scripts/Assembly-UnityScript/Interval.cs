using System;
using UnityEngine;

[Serializable]
public class Interval : MonoBehaviour
{
	public float interval;

	public Interval()
	{
		interval = 1f;
	}

	public virtual void Start()
	{
		InvokeRepeating("Toggle", interval, interval);
	}

	public virtual void Toggle()
	{
		gameObject.active = !gameObject.active;
	}

	public virtual void Main()
	{
	}
}
