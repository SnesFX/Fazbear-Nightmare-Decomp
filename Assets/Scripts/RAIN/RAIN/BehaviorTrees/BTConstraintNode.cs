using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTConstraintNode : BTNode
	{
		public const string NODETYPE = "constraint";

		private Expression _constraint = new Expression();

		private bool _constraintFailed;

		private int _lastRunning;

		public Expression Constraint
		{
			get
			{
				return _constraint;
			}
			set
			{
				_constraint = value;
				if (_constraint == null)
				{
					_constraint = new Expression();
				}
			}
		}

		public bool ConstraintFailed
		{
			get
			{
				return _constraintFailed;
			}
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			_lastRunning = 0;
			_constraintFailed = false;
		}

		public override ActionResult Execute(AI ai)
		{
			if (!_constraint.Evaluate<bool>(ai.DeltaTime, ai.WorkingMemory))
			{
				_constraintFailed = true;
				return ActionResult.FAILURE;
			}
			if (_children.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			ActionResult actionResult = ActionResult.FAILURE;
			while (_lastRunning < _children.Count)
			{
				actionResult = _children[_lastRunning].Run(ai);
				if (actionResult != 0)
				{
					break;
				}
				_lastRunning++;
			}
			return actionResult;
		}
	}
}
