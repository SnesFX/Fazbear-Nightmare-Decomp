using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTTimerNode : BTNode
	{
		public enum ReturnType
		{
			Success = 0,
			Failure = 1
		}

		public const string NODETYPE = "timer";

		private Expression _waitTime = new Expression();

		private ReturnType _returnValue;

		private float _timeRemaining;

		private bool _started;

		public Expression WaitTime
		{
			get
			{
				return _waitTime;
			}
			set
			{
				_waitTime = value ?? new Expression();
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

		public override void Start(AI ai)
		{
			base.Start(ai);
			_timeRemaining = _waitTime.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
		}

		public override ActionResult Execute(AI ai)
		{
			if (!_started)
			{
				_started = true;
			}
			else
			{
				_timeRemaining -= ai.DeltaTime;
			}
			if (_timeRemaining <= 0f)
			{
				if (_returnValue == ReturnType.Success)
				{
					return ActionResult.SUCCESS;
				}
				return ActionResult.FAILURE;
			}
			return ActionResult.RUNNING;
		}
	}
}
