using System;
using UnityEngine;

[Serializable]
public class FadeOut : MonoBehaviour
{
	public float fadeTime;

	public Texture fadeTexture;

	private bool fadeOut;

	private float tempTime;

	private float time;

	public AudioClip fadeOutSound;

	public FadeOut()
	{
		fadeTime = 5f;
	}

	public virtual void Start()
	{
		fadeOut = true;
		GetComponent<AudioSource>().clip = fadeOutSound;
		GetComponent<AudioSource>().Play();
	}

	public virtual void Update()
	{
		if (fadeOut)
		{
			if (!(time >= fadeTime))
			{
				time += Time.deltaTime;
			}
			tempTime = Mathf.InverseLerp(0f, fadeTime, time);
		}
		if (!(tempTime < 1f))
		{
			enabled = false;
		}
	}

	public virtual void OnGUI()
	{
		if (fadeOut)
		{
			float a = 1f - tempTime;
			Color color = GUI.color;
			float num = (color.a = a);
			Color color3 = (GUI.color = color);
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), fadeTexture);
		}
	}

	public virtual void Main()
	{
	}
}
