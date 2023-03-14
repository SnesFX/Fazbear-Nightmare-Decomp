using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using UnityEngine;

namespace RAIN.BehaviorTrees
{
	public abstract class BTNode : RAINAction
	{
		protected bool _debugBreak;

		protected ActionResult _actionState = ActionResult.NONE;

		protected ActionResult _repeatUntilState = ActionResult.NONE;

		protected List<BTNode> _children = new List<BTNode>();

		private bool _resetRepeat;

		public ActionResult ActionState
		{
			get
			{
				return _actionState;
			}
			set
			{
				_actionState = value;
			}
		}

		public bool DebugBreak
		{
			get
			{
				return _debugBreak;
			}
			set
			{
				_debugBreak = value;
			}
		}

		public ActionResult RepeatUntilState
		{
			get
			{
				return _repeatUntilState;
			}
			set
			{
				_repeatUntilState = value;
			}
		}

		public bool ResetRepeat
		{
			get
			{
				return _resetRepeat;
			}
		}

		public BTNode()
		{
			actionName = "__unnamed";
		}

		public ActionResult Run(AI ai)
		{
			if (_resetRepeat)
			{
				_resetRepeat = false;
				Reset();
			}
			if (_actionState == ActionResult.NONE)
			{
				Start(ai);
			}
			_actionState = Execute(ai);
			if (_actionState != ActionResult.RUNNING)
			{
				Stop(ai);
				if (_repeatUntilState != ActionResult.NONE && _repeatUntilState != _actionState)
				{
					_resetRepeat = true;
					_actionState = ActionResult.RUNNING;
				}
			}
			return _actionState;
		}

		public override void Start(AI ai)
		{
			if (_debugBreak)
			{
				Debug.LogWarning("BTNode: Debug Break at " + actionName);
				Debug.Break();
			}
		}

		public override void Stop(AI ai)
		{
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i]._actionState == ActionResult.RUNNING)
				{
					_children[i].Stop(ai);
				}
			}
		}

		public virtual void Reset()
		{
			_actionState = ActionResult.NONE;
			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Reset();
			}
		}

		public virtual void ResetChildren()
		{
			for (int i = 0; i < _children.Count; i++)
			{
				_children[i].Reset();
			}
		}

		public virtual void AddChild(BTNode child)
		{
			if (child != null)
			{
				_children.Add(child);
			}
		}

		public virtual BTNode GetChild(int index)
		{
			return _children[index];
		}

		public virtual int GetChildIndex(string actionName)
		{
			for (int i = 0; i < _children.Count; i++)
			{
				if (_children[i].actionName == actionName)
				{
					return i;
				}
			}
			return -1;
		}

		public virtual BTNode RemoveChild(int index)
		{
			BTNode result = _children[index];
			_children.RemoveAt(index);
			return result;
		}

		public int GetChildCount()
		{
			return _children.Count;
		}
	}
}
