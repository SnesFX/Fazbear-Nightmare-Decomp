using RAIN.Core;
using UnityEngine;

namespace RAIN.Animation
{
	public class IKProxy : MonoBehaviour
	{
		[SerializeField]
		private AIRig _rainAIRig;

		public void Awake()
		{
			_rainAIRig = base.gameObject.GetComponentInChildren<AIRig>();
		}

		public void OnAnimatorIK(int aLayerIndex)
		{
			if (_rainAIRig != null)
			{
				_rainAIRig.OnAnimatorIK(aLayerIndex);
			}
		}

		public void Cleanup()
		{
			if (Application.isEditor)
			{
				Object.DestroyImmediate(this);
			}
			else
			{
				Object.Destroy(this);
			}
		}
	}
}
