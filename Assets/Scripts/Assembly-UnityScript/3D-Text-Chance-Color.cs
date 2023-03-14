using System;
using UnityEngine;

[Serializable]
public class _3D_0020Text_0020Chance_0020Color : MonoBehaviour
{
	public virtual void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.red;
	}

	public virtual void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}

	public virtual void Main()
	{
	}
}
