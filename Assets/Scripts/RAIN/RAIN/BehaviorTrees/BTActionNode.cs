using System.Reflection;
using RAIN.Action;
using RAIN.Core;

namespace RAIN.BehaviorTrees
{
	public class BTActionNode : BTNode
	{
		public const string NODETYPE = "action";

		public RAINAction actionInstance;

		public void SetAction(Assembly aAssembly, string aCustomActionName)
		{
			actionInstance = RAINAction.LoadActionInstance(aAssembly, aCustomActionName);
			if (actionInstance != null)
			{
				actionInstance.ActionName = actionName;
			}
		}

		public void SetAction(string aNamespace, string aFullClassName)
		{
			actionInstance = RAINAction.LoadActionInstance(aNamespace, aFullClassName);
			if (actionInstance != null)
			{
				actionInstance.ActionName = actionName;
			}
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			if (actionInstance != null)
			{
				actionInstance.Start(ai);
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (actionInstance == null)
			{
				return ActionResult.FAILURE;
			}
			return actionInstance.Execute(ai);
		}

		public override void Stop(AI ai)
		{
			if (actionInstance != null)
			{
				actionInstance.Stop(ai);
			}
			base.Stop(ai);
		}
	}
}
