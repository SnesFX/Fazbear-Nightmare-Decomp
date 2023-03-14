using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

namespace RAIN.BehaviorTrees
{
	public class BTDynamicBindingNode : BTNode
	{
		public const string NODETYPE = "treebinding";

		public string bindingName = "";

		private BTAsset _loadedTree;

		public void LoadTree(List<BTAssetBinding> aTreeBindings)
		{
			if (aTreeBindings == null)
			{
				return;
			}
			BTAsset bTAsset = null;
			for (int i = 0; i < aTreeBindings.Count; i++)
			{
				if (aTreeBindings[i].binding == bindingName)
				{
					bTAsset = aTreeBindings[i].behaviorTree;
				}
			}
			if (!(_loadedTree == bTAsset))
			{
				_loadedTree = bTAsset;
				_children.Clear();
				if (bTAsset != null)
				{
					_children.Add(BTLoader.Load(bTAsset, aTreeBindings));
				}
			}
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			if (_children.Count == 1)
			{
				_children[0].Start(ai);
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 1)
			{
				return _children[0].Run(ai);
			}
			return ActionResult.SUCCESS;
		}
	}
}
