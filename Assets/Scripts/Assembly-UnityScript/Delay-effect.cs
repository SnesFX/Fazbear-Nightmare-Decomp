using System;
using UnityEngine;

[Serializable]
public class Delay_0020effect : MonoBehaviour
{
	public float amount;

	public float maxAmount;

	public float smooth;

	private Vector3 def;

	public Delay_0020effect()
	{
		amount = 0.02f;
		maxAmount = 0.03f;
		smooth = 3f;
	}

	public virtual void Start()
	{
		def = transform.localPosition;
	}

	public virtual void Update()
	{
		float num = (0f - Input.GetAxis("Mouse X")) * amount;
		float num2 = (0f - Input.GetAxis("Mouse Y")) * amount;
		if (!(num <= maxAmount))
		{
			num = maxAmount;
		}
		if (!(num >= 0f - maxAmount))
		{
			num = 0f - maxAmount;
		}
		if (!(num2 <= maxAmount))
		{
			num2 = maxAmount;
		}
		if (!(num2 >= 0f - maxAmount))
		{
			num2 = 0f - maxAmount;
		}
		Vector3 to = new Vector3(def.x + num, def.y + num2, def.z);
		transform.localPosition = Vector3.Lerp(transform.localPosition, to, Time.deltaTime * smooth);
	}

	public virtual void Main()
	{
	}
}
