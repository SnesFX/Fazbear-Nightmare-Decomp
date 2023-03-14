using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

namespace RAIN.BehaviorTrees
{
	public class BTPriorityNode : BTNode
	{
		public const string NODETYPE = "priority";

		protected float _refreshRate = 0.1f;

		protected List<BTPriorityChild> _priorityChildren = new List<BTPriorityChild>();

		protected List<Expression> _startingPriorities = new List<Expression>();

		protected List<Expression> _runningPriorities = new List<Expression>();

		protected BTPriorityChild _lastRunning;

		protected LinkedList<BTPriorityChild> _originalOptions = new LinkedList<BTPriorityChild>();

		protected LinkedList<BTPriorityChild> _currentOptions = new LinkedList<BTPriorityChild>();

		protected float _refreshDelay;

		private bool _resetLastRunning;

		public float RefreshRate
		{
			get
			{
				return _refreshRate;
			}
			set
			{
				_refreshRate = value;
			}
		}

		public BTNode LastRunningNode
		{
			get
			{
				if (_lastRunning == null)
				{
					return null;
				}
				return _lastRunning.Child;
			}
		}

		public override void AddChild(BTNode child)
		{
			int count = _children.Count;
			base.AddChild(child);
			_priorityChildren.Add(new BTPriorityChild(child, null, null));
			if (_startingPriorities.Count < _children.Count)
			{
				SetPriorities(_children.Count - 1, null, null);
			}
		}

		public override BTNode RemoveChild(int index)
		{
			if (index >= 0)
			{
				_startingPriorities.RemoveAt(index);
				_runningPriorities.RemoveAt(index);
				_priorityChildren.RemoveAt(index);
			}
			return base.RemoveChild(index);
		}

		public void SetPriorities(int aChildIndex, Expression aStartingPriority, Expression aRunningPriority)
		{
			if (aChildIndex >= 0)
			{
				int count = _startingPriorities.Count;
				for (int i = count; i <= aChildIndex; i++)
				{
					_startingPriorities.Add(null);
					_runningPriorities.Add(null);
				}
				_startingPriorities[aChildIndex] = aStartingPriority;
				_runningPriorities[aChildIndex] = aRunningPriority;
			}
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			_lastRunning = null;
			_resetLastRunning = false;
			_originalOptions.Clear();
			_currentOptions.Clear();
			for (int i = 0; i < _children.Count; i++)
			{
				_priorityChildren[i].SetPriorities(_startingPriorities[i], _runningPriorities[i]);
				_currentOptions.AddLast(_priorityChildren[i]);
			}
			_refreshDelay = _refreshRate;
		}

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 0)
			{
				return ActionResult.RUNNING;
			}
			if (_resetLastRunning)
			{
				_resetLastRunning = false;
				_lastRunning.Child.Stop(ai);
				_lastRunning.Child.Reset();
				_lastRunning.ResetPriority();
				_lastRunning = null;
			}
			if (_refreshDelay <= 0f || _lastRunning == null)
			{
				_refreshDelay = _refreshRate;
				while (_originalOptions.Count > 0)
				{
					LinkedListNode<BTPriorityChild> first = _originalOptions.First;
					_originalOptions.RemoveFirst();
					_currentOptions.AddLast(first);
				}
				foreach (BTPriorityChild currentOption in _currentOptions)
				{
					currentOption.ResetPriority();
				}
				BTPriorityChild bTPriorityChild = ChooseNodeByPriority(ai);
				if (_lastRunning != bTPriorityChild)
				{
					if (_lastRunning != null)
					{
						_lastRunning.Child.Stop(ai);
						_lastRunning.Child.Reset();
						_lastRunning.ResetPriority();
					}
					_lastRunning = bTPriorityChild;
				}
			}
			else
			{
				_refreshDelay -= ai.DeltaTime;
			}
			if (_lastRunning != null)
			{
				ActionResult actionResult = _lastRunning.Child.Run(ai);
				if (actionResult != ActionResult.RUNNING)
				{
					_resetLastRunning = true;
				}
			}
			return ActionResult.RUNNING;
		}

		protected BTPriorityChild ChooseNodeByPriority(AI ai)
		{
			LinkedListNode<BTPriorityChild> linkedListNode = null;
			float num = float.MinValue;
			for (LinkedListNode<BTPriorityChild> linkedListNode2 = _currentOptions.First; linkedListNode2 != null; linkedListNode2 = linkedListNode2.Next)
			{
				float num2 = linkedListNode2.Value.CalculatePriority(ai);
				if (num2 > num)
				{
					linkedListNode = linkedListNode2;
					num = num2;
				}
			}
			if (linkedListNode != null)
			{
				_currentOptions.Remove(linkedListNode);
				_originalOptions.AddLast(linkedListNode);
				return linkedListNode.Value;
			}
			return null;
		}
	}
}
