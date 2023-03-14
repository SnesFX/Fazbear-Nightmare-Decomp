using System;
using UnityEngine;

[Serializable]
public class PowerUsage : MonoBehaviour
{
	public GameObject doorLbutton;

	public GameObject doorRbutton;

	public GameObject lightL;

	public GameObject lightR;

	public GameObject lightGlobal;

	public GameObject lightLocal;

	public GameObject batteryUsageGreen;

	public GameObject batteryUsageYellow;

	public GameObject batteryUsageOrange;

	public GameObject batteryUsageRed;

	public int usageCounter;

	public int powerUsage;

	public float time;

	public int timeInteger;

	public string timeString;

	public AudioClip noPowerSound;

	public bool havePower;

	public PowerUsage()
	{
		time = 360f;
		timeString = "Hour 1";
		havePower = true;
	}

	public virtual void Start()
	{
	}

	public virtual void Update()
	{
		time -= Time.deltaTime;
		timeInteger = (int)time;
		if (!(time <= 300f) && !(time >= 360f))
		{
			timeString = "12:00";
		}
		if (!(time <= 240f) && !(time >= 300f))
		{
			timeString = "12:10";
		}
		if (!(time <= 180f) && !(time >= 240f))
		{
			timeString = "12:20";
		}
		if (!(time <= 120f) && !(time >= 180f))
		{
			timeString = "12:30";
		}
		if (!(time <= 60f) && !(time >= 120f))
		{
			timeString = "12:40";
		}
		if (!(time <= 0f) && !(time >= 60f))
		{
			timeString = "12:50";
		}
		if (!(time >= 0f))
		{
			time = 0f;
			timeString = "12:59";
			Application.LoadLevel("Loading 1");
		}
	}

	public virtual void OnGUI()
	{
		GUI.Label(new Rect(10f, Screen.height - 64, 100f, 20f), timeString);
	}

	public virtual void Main()
	{
	}
}
