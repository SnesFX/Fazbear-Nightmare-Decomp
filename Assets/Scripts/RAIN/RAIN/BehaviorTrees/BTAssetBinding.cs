using RAIN.Serialization;

namespace RAIN.BehaviorTrees
{
	[RAINSerializableClass]
	public class BTAssetBinding
	{
		public string binding;

		public BTAsset behaviorTree;
	}
}
