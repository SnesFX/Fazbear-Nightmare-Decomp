using System;
using UnityEngine;

[Serializable]
public class cursor_0020false : MonoBehaviour
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
