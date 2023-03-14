using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTPriorityChild
	{
		private BTNode _child;

		private Expression _startingPriority;

		private float _savedStartingPriority;

		private bool _hasSavedStartingPriority;

		private Expression _runningPriority;

		private float _savedRunningPriority;

		private bool _hasSavedRunningPriority;

		public BTNode Child
		{
			get
			{
				return _child;
			}
		}

		public BTPriorityChild(BTNode aChild, Expression aStartingPriority, Expression aRunningPriority)
		{
			_child = aChild;
			_startingPriority = aStartingPriority;
			_runningPriority = aRunningPriority;
		}

		public void SetPriorities(Expression aStartingPriority, Expression aRunningPriority)
		{
			_startingPriority = aStartingPriority;
			_runningPriority = aRunningPriority;
			ResetPriority();
		}

		public void ResetPriority()
		{
			_savedStartingPriority = 0f;
			_hasSavedStartingPriority = false;
			_savedRunningPriority = 0f;
			_hasSavedRunningPriority = false;
		}

		public float CalculatePriority(AI ai)
		{
			if (_child.ActionState == RAINAction.ActionResult.RUNNING)
			{
				if (_hasSavedRunningPriority)
				{
					return _savedRunningPriority;
				}
				_hasSavedRunningPriority = true;
				_savedRunningPriority = 0f;
				if (_runningPriority != null)
				{
					_savedRunningPriority = _runningPriority.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
				}
				return _savedRunningPriority;
			}
			if (_hasSavedStartingPriority)
			{
				return _savedStartingPriority;
			}
			_hasSavedStartingPriority = true;
			_savedStartingPriority = 0f;
			if (_startingPriority != null)
			{
				_savedStartingPriority = _startingPriority.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
			}
			return _savedStartingPriority;
		}
	}
}
