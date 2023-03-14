using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees
{
	public class BTRandomNode : BTNode
	{
		public const string NODETYPE = "random";

		private List<Expression> _weights = new List<Expression>();

		private BTNode _activeNode;

		public override void AddChild(BTNode child)
		{
			base.AddChild(child);
			for (int i = _weights.Count; i < _children.Count; i++)
			{
				_weights.Add(null);
			}
		}

		public override BTNode RemoveChild(int index)
		{
			if (index >= 0)
			{
				_weights.RemoveAt(index);
			}
			return base.RemoveChild(index);
		}

		public void SetWeight(int aChildIndex, Expression aWeight)
		{
			if (aChildIndex >= 0)
			{
				int count = _weights.Count;
				for (int i = count; i <= aChildIndex; i++)
				{
					_weights.Add(null);
				}
				_weights[aChildIndex] = aWeight;
			}
		}

		public BTNode GetActiveNode()
		{
			return _activeNode;
		}

		public override void Start(AI ai)
		{
			base.Start(ai);
			_activeNode = null;
			if (_children.Count == 0)
			{
				return;
			}
			List<float> list = new List<float>();
			float num = 0f;
			for (int i = 0; i < _weights.Count; i++)
			{
				if (_weights[i] == null || !_weights[i].IsValid)
				{
					list.Add(0f);
				}
				else
				{
					list.Add(_weights[i].Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
				}
				num += list[i];
			}
			if (num == 0f)
			{
				int index = Random.Range(0, _children.Count);
				_activeNode = _children[index];
				return;
			}
			for (int num2 = list.Count - 1; num2 >= 0; num2--)
			{
				if (list[num2] > 0f)
				{
					_activeNode = _children[num2];
					break;
				}
			}
			float num3 = Random.value * num;
			for (int j = 0; j < list.Count; j++)
			{
				num3 -= list[j];
				if (num3 <= 0f && list[j] > 0f)
				{
					_activeNode = _children[j];
					break;
				}
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (_activeNode == null)
			{
				return ActionResult.SUCCESS;
			}
			return _activeNode.Run(ai);
		}

		public override void Stop(AI ai)
		{
			if (_activeNode != null && _activeNode.ActionState == ActionResult.RUNNING)
			{
				_activeNode.Stop(ai);
			}
			base.Stop(ai);
		}
	}
}
