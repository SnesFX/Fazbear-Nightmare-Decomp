using RAIN.Core;
using UnityEngine;

namespace RAIN.Motion
{
	public class RootMotionProxy : MonoBehaviour
	{
		public AIRig rainAIRig;

		public void OnAnimatorMove()
		{
			if (rainAIRig != null)
			{
				rainAIRig.OnAnimatorMove();
			}
		}
	}
}
