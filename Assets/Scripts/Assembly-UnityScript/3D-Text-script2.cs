using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(AudioSource))]
public class _3D_0020Text_0020script2 : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024OnMouseUp_002420 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal _3D_0020Text_0020script2 _0024self__002421;

			public _0024(_3D_0020Text_0020script2 self_)
			{
				_0024self__002421 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002421.GetComponent<AudioSource>().PlayOneShot(_0024self__002421.beep);
					result = (Yield(2, new WaitForSeconds(0.35f)) ? 1 : 0);
					break;
				case 2:
					if (_0024self__002421.QuitButton)
					{
						Application.Quit();
					}
					else
					{
						Application.LoadLevel("Night 2");
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

		internal _3D_0020Text_0020script2 _0024self__002422;

		public _0024OnMouseUp_002420(_3D_0020Text_0020script2 self_)
		{
			_0024self__002422 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002422);
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
		return new _0024OnMouseUp_002420(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
