using RAIN.Action;
using RAIN.Core;

namespace RAIN.BehaviorTrees
{
	public class BTParallelNode : BTNode
	{
		public const string NODETYPE = "parallel";

		public bool failOnSingle;

		public bool succeedOnSingle;

		public bool failOnTie = true;

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i].ActionState == ActionResult.NONE || _children[i].ActionState == ActionResult.RUNNING)
				{
					_children[i].Run(ai);
				}
				if (_children[i].ActionState == ActionResult.SUCCESS)
				{
					num++;
				}
				else if (_children[i].ActionState == ActionResult.FAILURE)
				{
					num2++;
				}
				else
				{
					num3++;
				}
			}
			if (failOnTie)
			{
				if (failOnSingle && num2 > 0)
				{
					return ActionResult.FAILURE;
				}
				if (succeedOnSingle && num > 0)
				{
					return ActionResult.SUCCESS;
				}
			}
			else
			{
				if (succeedOnSingle && num > 0)
				{
					return ActionResult.SUCCESS;
				}
				if (failOnSingle && num2 > 0)
				{
					return ActionResult.FAILURE;
				}
			}
			if (num == _children.Count)
			{
				return ActionResult.SUCCESS;
			}
			if (num2 == _children.Count)
			{
				return ActionResult.FAILURE;
			}
			if (num3 > 0)
			{
				return ActionResult.RUNNING;
			}
			if (failOnTie)
			{
				return ActionResult.FAILURE;
			}
			return ActionResult.SUCCESS;
		}
	}
}
