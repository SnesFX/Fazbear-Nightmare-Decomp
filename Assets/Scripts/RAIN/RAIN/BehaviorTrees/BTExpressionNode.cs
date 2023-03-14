using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTExpressionNode : BTNode
	{
		public enum ReturnType
		{
			Success = 0,
			Failure = 1,
			Evaluate = 2
		}

		public const string NODETYPE = "expression";

		private Expression _expression = new Expression();

		private ReturnType _returnValue;

		public Expression EvaluatedExpression
		{
			get
			{
				return _expression;
			}
			set
			{
				_expression = value;
				if (_expression == null)
				{
					_expression = new Expression();
				}
			}
		}

		public ReturnType ReturnValue
		{
			get
			{
				return _returnValue;
			}
			set
			{
				_returnValue = value;
			}
		}

		public override ActionResult Execute(AI ai)
		{
			bool flag = _expression.Evaluate<bool>(ai.DeltaTime, ai.WorkingMemory);
			switch (_returnValue)
			{
			case ReturnType.Success:
				return ActionResult.SUCCESS;
			case ReturnType.Failure:
				return ActionResult.FAILURE;
			default:
				if (flag)
				{
					return ActionResult.SUCCESS;
				}
				return ActionResult.FAILURE;
			}
		}
	}
}
