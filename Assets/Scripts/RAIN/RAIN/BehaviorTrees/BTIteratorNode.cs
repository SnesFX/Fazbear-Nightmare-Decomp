using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTIteratorNode : BTNode
	{
		public const string NODETYPE = "iterator";

		private Expression _countInitializer = new Expression();

		private int _lastRunning;

		private int _loopsRemaining;

		private bool _resetLoop;

		public BTNode LastRunningNode
		{
			get
			{
				if (_lastRunning >= _children.Count)
				{
					return null;
				}
				return _children[_lastRunning];
			}
		}

		public Expression CountInitializer
		{
			get
			{
				return _countInitializer;
			}
			set
			{
				_countInitializer = value;
				if (_countInitializer == null)
				{
					_countInitializer = new Expression();
				}
			}
		}

		public bool ResetLoop
		{
			get
			{
				return _resetLoop;
			}
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			_lastRunning = 0;
			_loopsRemaining = _countInitializer.Evaluate<int>(ai.DeltaTime, ai.WorkingMemory);
			_resetLoop = false;
		}

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			if (_loopsRemaining <= 0)
			{
				return ActionResult.SUCCESS;
			}
			if (_resetLoop)
			{
				_lastRunning = 0;
				_resetLoop = false;
				for (int i = 0; i < _children.Count; i++)
				{
					_children[i].Reset();
				}
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
			if (actionResult == ActionResult.SUCCESS)
			{
				_loopsRemaining--;
				if (_loopsRemaining <= 0)
				{
					return ActionResult.SUCCESS;
				}
				_resetLoop = true;
				return ActionResult.RUNNING;
			}
			return actionResult;
		}
	}
}
