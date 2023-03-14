using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Boo.Lang;
using UnityEngine;

[Serializable]
public class Foootsteps_00202 : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024CamminaSuForest_002426 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Foootsteps_00202 _0024self__002427;

			public _0024(Foootsteps_00202 self_)
			{
				_0024self__002427 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002427.step = false;
					_0024self__002427.GetComponent<AudioSource>().clip = _0024self__002427.forest[UnityEngine.Random.Range(0, _0024self__002427.forest.Length)];
					_0024self__002427.GetComponent<AudioSource>().volume = 1f;
					_0024self__002427.GetComponent<AudioSource>().Play();
					result = (Yield(2, new WaitForSeconds(_0024self__002427.lunghezzaPausa)) ? 1 : 0);
					break;
				case 2:
					_0024self__002427.step = true;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Foootsteps_00202 _0024self__002428;

		public _0024CamminaSuForest_002426(Foootsteps_00202 self_)
		{
			_0024self__002428 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002428);
		}
	}

	[Serializable]
	[CompilerGenerated]
	internal sealed class _0024CamminaSuWood_002429 : GenericGenerator<WaitForSeconds>
	{
		[Serializable]
		[CompilerGenerated]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForSeconds>, IEnumerator
		{
			internal Foootsteps_00202 _0024self__002430;

			public _0024(Foootsteps_00202 self_)
			{
				_0024self__002430 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					_0024self__002430.step = false;
					_0024self__002430.GetComponent<AudioSource>().clip = _0024self__002430.wood[UnityEngine.Random.Range(0, _0024self__002430.wood.Length)];
					_0024self__002430.GetComponent<AudioSource>().volume = 1f;
					_0024self__002430.GetComponent<AudioSource>().Play();
					result = (Yield(2, new WaitForSeconds(_0024self__002430.lunghezzaPausa)) ? 1 : 0);
					break;
				case 2:
					_0024self__002430.step = true;
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal Foootsteps_00202 _0024self__002431;

		public _0024CamminaSuWood_002429(Foootsteps_00202 self_)
		{
			_0024self__002431 = self_;
		}

		public override IEnumerator<WaitForSeconds> GetEnumerator()
		{
			return new _0024(_0024self__002431);
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
		if (Physics.Raycast(transform.position, transform.forward, out hitInfo, 2f) && (Input.GetKey("left shift") || Input.GetKey("right shift")) && ((CharacterController)player.GetComponent(typeof(CharacterController))).isGrounded)
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
		return new _0024CamminaSuForest_002426(this).GetEnumerator();
	}

	public virtual IEnumerator CamminaSuWood()
	{
		return new _0024CamminaSuWood_002429(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
