using System;
using UnityEngine;

[Serializable]
public class Crosshair : MonoBehaviour
{
	public Texture2D TextureCrosshair;

	public Texture2D TextureUsa;

	public Texture2D TextureNormale;

	public bool MostraCrosshair;

	public bool Cambia;

	public Crosshair()
	{
		MostraCrosshair = true;
	}

	public virtual void Start()
	{
		Screen.lockCursor = true;
	}

	public virtual void OnGUI()
	{
		if (MostraCrosshair)
		{
			GUI.Label(new Rect((Screen.width - TextureCrosshair.width) / 2, (Screen.height - TextureCrosshair.height) / 2, TextureCrosshair.width, TextureCrosshair.height), TextureCrosshair);
		}
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Porta")
		{
			Cambia = false;
			TextureCrosshair = TextureUsa;
		}
	}

	public virtual void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Porta")
		{
			Cambia = true;
			TextureCrosshair = TextureNormale;
		}
	}

	public virtual void TextureChange()
	{
		if (!Cambia)
		{
			TextureCrosshair = TextureUsa;
			Cambia = true;
		}
	}

	public virtual void BackToNormal()
	{
		if (Cambia)
		{
			Cambia = false;
			TextureCrosshair = TextureNormale;
		}
	}

	public virtual void Main()
	{
	}
}
