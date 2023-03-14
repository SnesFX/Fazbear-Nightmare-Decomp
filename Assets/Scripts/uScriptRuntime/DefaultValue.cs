using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
public class DefaultValue : Attribute
{
	public object Default;

	public DefaultValue(object o)
	{
		Default = o;
	}

	public DefaultValue(Type t, float[] f)
	{
		if (t == typeof(Rect) && f.Length == 4)
		{
			Default = new Rect(f[0], f[1], f[2], f[3]);
			return;
		}
		if (t == typeof(Color) && f.Length == 3)
		{
			Default = new Color(f[0], f[1], f[2]);
			return;
		}
		if (t == typeof(Color) && f.Length == 4)
		{
			Default = new Color(f[0], f[1], f[2], f[3]);
			return;
		}
		if (t == typeof(Vector2) && f.Length == 2)
		{
			Default = new Vector2(f[0], f[1]);
			return;
		}
		if (t == typeof(Vector3) && f.Length == 2)
		{
			Default = new Vector3(f[0], f[1]);
			return;
		}
		if (t == typeof(Vector3) && f.Length == 3)
		{
			Default = new Vector3(f[0], f[1], f[2]);
			return;
		}
		if (t == typeof(Vector4) && f.Length == 2)
		{
			Default = new Vector4(f[0], f[1]);
			return;
		}
		if (t == typeof(Vector4) && f.Length == 3)
		{
			Default = new Vector4(f[0], f[1], f[2]);
			return;
		}
		if (t == typeof(Vector4) && f.Length == 4)
		{
			Default = new Vector4(f[0], f[1], f[2], f[3]);
			return;
		}
		Debug.LogError("Unhandled DefaultValue type and float[] length pair:\n\t" + t.ToString() + " cannot have " + f.Length + " parameters or the type isn't yet supported.\n");
	}
}
