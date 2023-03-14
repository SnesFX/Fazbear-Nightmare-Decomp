using System;
using UnityEngine;

[Serializable]
public class LockScreen : MonoBehaviour
{
	public virtual void Update()
	{
		Screen.lockCursor = true;
		Screen.lockCursor = false;
	}

	public virtual void Main()
	{
	}
}
