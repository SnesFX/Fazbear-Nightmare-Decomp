using System.Collections.Generic;
using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.Animation
{
	[RAINSerializableClass]
	public abstract class RAINAnimator : RAINAIElement
	{
		public abstract bool StartState(string aStateName);

		public abstract bool IsStatePlaying(string aStateName);

		public abstract bool StopState(string aStateName);

		public abstract void UpdateAnimation();

		public abstract List<string> GetStates();

		public abstract void UpdateIK(int aLayerIndex);
	}
}
