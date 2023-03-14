using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class Return_0020to_0020main_0020menu : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024OnMouseUp_002434 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Return_0020to_0020main_0020menu _0024self__002435;

			public _0024(Return_0020to_0020main_0020menu self_)
			{
				_0024self__002435 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002435.GetComponent<AudioSource>().PlayOneShot(_0024self__002435.beep);
					result = (Yield(2, new WaitForSeconds(0.35f)) ? 1 : 0);
					break;
				case 2:
					if (_0024self__002435.QuitButton)
					{
						Application.Quit();
					}
					else
					{
						Application.LoadLevel("Main Menu");
					}
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Return_0020to_0020main_0020menu _0024self__002436;

		public _0024OnMouseUp_002434(Return_0020to_0020main_0020menu self_)
		{
			_0024self__002436 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002436);
		}
	}

	public AudioClip soundhover;

	public AudioClip beep;

	public bool QuitButton;

	public virtual void OnMouseEnter()
	{
		GetComponent<AudioSource>().PlayOneShot(soundhover);
	}

	public virtual IEnumerator OnMouseUp()
	{
		return new _0024OnMouseUp_002434(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
