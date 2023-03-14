using RAIN.BehaviorTrees;
using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.Entities.Elements
{
	[RAINSerializableClass]
	[RAINElement("Behavior")]
	public class EntityBehaviorElement : CustomEntityElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Behavior Asset")]
		private BTAsset _behavior;

		public BTNode GetBehavior()
		{
			if (_behavior == null)
			{
				return null;
			}
			return BTLoader.Load(_behavior, null);
		}
	}
}
