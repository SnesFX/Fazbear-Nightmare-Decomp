using RAIN.Core;
using RAIN.Serialization;

namespace RAIN.BehaviorTrees
{
	public class BTAsset : RAINScriptableObject
	{
		[RAINSerializableField]
		private string treeData = "";

		[RAINSerializableField]
		private string[] treeBindings = new string[0];

		public void SetTreeData(string aData)
		{
			treeData = aData;
			Serialize();
		}

		public string GetTreeData()
		{
			return treeData;
		}

		public void SetTreeBindings(string[] aTreeBindings)
		{
			treeBindings = aTreeBindings;
			Serialize();
		}

		public string[] GetTreeBindings()
		{
			return treeBindings;
		}
	}
}
