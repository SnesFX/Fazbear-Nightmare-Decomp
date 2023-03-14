using RAIN.Action;
using RAIN.Core;

namespace RAIN.BehaviorTrees
{
	public class BTSelectorNode : BTPriorityNode
	{
		public new const string NODETYPE = "selector";

		protected bool _usePriorities;

		public bool UsePriorities
		{
			get
			{
				return _usePriorities;
			}
			set
			{
				_usePriorities = value;
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			ActionResult actionResult = ActionResult.FAILURE;
			if (_lastRunning == null)
			{
				if (_usePriorities)
				{
					_lastRunning = ChooseNodeByPriority(ai);
				}
				else
				{
					_lastRunning = ChooseNodeByOrder();
				}
			}
			while (_lastRunning != null)
			{
				actionResult = _lastRunning.Child.Run(ai);
				if (actionResult != ActionResult.FAILURE)
				{
					break;
				}
				if (_usePriorities)
				{
					_lastRunning = ChooseNodeByPriority(ai);
				}
				else
				{
					_lastRunning = ChooseNodeByOrder();
				}
			}
			return actionResult;
		}

		protected BTPriorityChild ChooseNodeByOrder()
		{
			BTPriorityChild result = null;
			if (_currentOptions.Count > 0)
			{
				result = _currentOptions.First.Value;
				_currentOptions.RemoveFirst();
			}
			return result;
		}
	}
}
