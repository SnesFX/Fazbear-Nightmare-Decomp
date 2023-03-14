using UnityEngine;

namespace RAIN.Core
{
	public class RAINUnityMessageProxy : MonoBehaviour
	{
		public AIRig rainAIRig;

		public void OnAnimatorMove()
		{
			if (rainAIRig != null)
			{
				rainAIRig.OnAnimatorMove();
			}
		}

		public void OnAnimatorIK(int aLayerIndex)
		{
			if (rainAIRig != null)
			{
				rainAIRig.OnAnimatorIK(aLayerIndex);
			}
		}
	}
}
