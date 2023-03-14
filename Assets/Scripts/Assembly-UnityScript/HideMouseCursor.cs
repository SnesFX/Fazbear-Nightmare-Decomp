using System;
using UnityEngine;

[Serializable]
public class HideMouseCursor : MonoBehaviour
{
	public virtual void Update()
	{
		Cursor.visible = false;
	}

	public virtual void Main()
	{
	}
}
