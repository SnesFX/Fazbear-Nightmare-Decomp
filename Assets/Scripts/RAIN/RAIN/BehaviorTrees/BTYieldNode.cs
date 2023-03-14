using RAIN.Action;
using RAIN.Core;

namespace RAIN.BehaviorTrees
{
	public class BTYieldNode : BTNode
	{
		public const string NODETYPE = "yield";

		private bool yieldState;

		public override void Start(AI ai)
		{
			base.Start(ai);
			yieldState = false;
		}

		public override ActionResult Execute(AI ai)
		{
			if (!yieldState)
			{
				yieldState = true;
				return ActionResult.RUNNING;
			}
			return ActionResult.SUCCESS;
		}
	}
}
