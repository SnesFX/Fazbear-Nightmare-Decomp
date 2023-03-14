using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Trigger_0020sound : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024removeovertime_002438 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Trigger_0020sound _0024self__002439;

			public _0024(Trigger_0020sound self_)
			{
				_0024self__002439 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForSeconds(1f)) ? 1 : 0);
					break;
				case 2:
					_0024self__002439.trigger.GetComponent<Renderer>().enabled = false;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Trigger_0020sound _0024self__002440;

		public _0024removeovertime_002438(Trigger_0020sound self_)
		{
			_0024self__002440 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002440);
		}
	}

	public GameObject trigger;

	public bool hasplayed;

	public bool entertrigger;

	public AudioClip screaminground;

	public virtual void Start()
	{
		entertrigger = false;
		trigger.GetComponent<Renderer>().enabled = false;
	}

	public virtual void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			entertrigger = true;
		}
	}

	public virtual void Update()
	{
		if (entertrigger)
		{
			trigger.GetComponent<Renderer>().enabled = true;
			StartCoroutine(removeovertime());
			makehimscream();
		}
	}

	public virtual IEnumerator removeovertime()
	{
		return new _0024removeovertime_002438(this).GetEnumerator();
	}

	public virtual void makehimscream()
	{
		if (!hasplayed)
		{
			hasplayed = true;
			GetComponent<AudioSource>().PlayOneShot(screaminground);
		}
	}

	public virtual void Main()
	{
	}
}
