using System;
using UnityEngine;

[Serializable]
public class Zoom : MonoBehaviour
{
	public int zoom;

	public int normal;

	public float smooth;

	private bool isZoomed;

	public Zoom()
	{
		zoom = 20;
		normal = 60;
		smooth = 5f;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			GetComponent<AudioSource>().Play();
			isZoomed = !isZoomed;
		}
		if (isZoomed)
		{
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
		}
		else
		{
			GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
		}
	}

	public virtual void Main()
	{
	}
}
