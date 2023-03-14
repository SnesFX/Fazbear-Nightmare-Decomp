using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class _3D_0020Text_0020script : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024OnMouseUp_002414 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal _3D_0020Text_0020script _0024self__002415;

			public _0024(_3D_0020Text_0020script self_)
			{
				_0024self__002415 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002415.GetComponent<AudioSource>().PlayOneShot(_0024self__002415.beep);
					result = (Yield(2, new WaitForSeconds(0.35f)) ? 1 : 0);
					break;
				case 2:
					if (_0024self__002415.QuitButton)
					{
						Application.Quit();
					}
					else
					{
						Application.LoadLevel("Loading");
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

		internal _3D_0020Text_0020script _0024self__002416;

		public _0024OnMouseUp_002414(_3D_0020Text_0020script self_)
		{
			_0024self__002416 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002416);
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
		return new _0024OnMouseUp_002414(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
