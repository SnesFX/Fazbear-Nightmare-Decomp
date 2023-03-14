using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Footsteps : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024CamminaSuForest_00248 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Footsteps _0024self__00249;

			public _0024(Footsteps self_)
			{
				_0024self__00249 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__00249.step = false;
					_0024self__00249.GetComponent<AudioSource>().clip = _0024self__00249.forest[UnityEngine.Random.Range(0, _0024self__00249.forest.Length)];
					_0024self__00249.GetComponent<AudioSource>().volume = 1f;
					_0024self__00249.GetComponent<AudioSource>().Play();
					result = (Yield(2, new WaitForSeconds(_0024self__00249.lunghezzaPausa)) ? 1 : 0);
					break;
				case 2:
					_0024self__00249.step = true;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Footsteps _0024self__002410;

		public _0024CamminaSuForest_00248(Footsteps self_)
		{
			_0024self__002410 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002410);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024CamminaSuWood_002411 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Footsteps _0024self__002412;

			public _0024(Footsteps self_)
			{
				_0024self__002412 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002412.step = false;
					_0024self__002412.GetComponent<AudioSource>().clip = _0024self__002412.wood[UnityEngine.Random.Range(0, _0024self__002412.wood.Length)];
					_0024self__002412.GetComponent<AudioSource>().volume = 1f;
					_0024self__002412.GetComponent<AudioSource>().Play();
					result = (Yield(2, new WaitForSeconds(_0024self__002412.lunghezzaPausa)) ? 1 : 0);
					break;
				case 2:
					_0024self__002412.step = true;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Footsteps _0024self__002413;

		public _0024CamminaSuWood_002411(Footsteps self_)
		{
			_0024self__002413 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002413);
		}
	}

	public AudioClip[] forest;

	public AudioClip[] wood;

	public float lunghezzaPausa;

	public bool step;

	public Transform player;

	public virtual void Awake()
	{
		step = true;
	}

	public virtual void Update()
	{
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2f) && (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f) && ((CharacterController)player.GetComponent(typeof(CharacterController))).isGrounded)
		{
			if (hitInfo.transform.tag == "Forest" && step)
			{
				StartCoroutine(CamminaSuForest());
			}
			if (hitInfo.transform.tag == "Wood" && step)
			{
				StartCoroutine(CamminaSuWood());
			}
		}
	}

	public virtual IEnumerator CamminaSuForest()
	{
		return new _0024CamminaSuForest_00248(this).GetEnumerator();
	}

	public virtual IEnumerator CamminaSuWood()
	{
		return new _0024CamminaSuWood_002411(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
