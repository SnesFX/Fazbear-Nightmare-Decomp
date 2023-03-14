using System;
using UnityEngine;

[Serializable]
public class HideMouse : MonoBehaviour
{
	public virtual void Start()
	{
	}

	public virtual void Update()
	{
	}

	public virtual void Main()
	{
		Cursor.visible = false;
	}
}
