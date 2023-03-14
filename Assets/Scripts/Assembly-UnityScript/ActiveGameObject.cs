using System;
using UnityEngine;

[Serializable]
public class ActiveGameObject : MonoBehaviour
{
	public virtual void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			gameObject.SetActive(true);
		}
	}

	public virtual void Main()
	{
	}
}
