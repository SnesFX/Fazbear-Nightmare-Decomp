using System;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.BehaviorTrees;
using RAIN.Serialization;

namespace RAIN.Minds
{
	[RAINSerializableClass]
	public class BasicMind : RAINMind
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private BTAsset _behaviorTreeAsset;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private List<BTAssetBinding> _behaviorTreeBindings = new List<BTAssetBinding>();

		private BTNode _behaviorRoot;

		public BTAsset BehaviorTreeAsset
		{
			get
			{
				return _behaviorTreeAsset;
			}
		}

		public List<BTAssetBinding> BehaviorTreeBindings
		{
			get
			{
				return _behaviorTreeBindings;
			}
		}

		public BTNode BehaviorRoot
		{
			get
			{
				return _behaviorRoot;
			}
			set
			{
				if (_behaviorRoot != value)
				{
					ResetBehavior();
					_behaviorRoot = value;
				}
			}
		}

		public override void AIInit()
		{
			base.AIInit();
			if (_behaviorTreeAsset != null)
			{
				SetBehavior(_behaviorTreeAsset, _behaviorTreeBindings);
			}
		}

		public override void Think()
		{
			if (_behaviorRoot != null)
			{
				if (_behaviorRoot.ActionState != RAINAction.ActionResult.RUNNING)
				{
					_behaviorRoot.Reset();
				}
				_behaviorRoot.Run(AI);
			}
		}

		public virtual void SetBehavior(BTAsset aBTAsset, List<BTAssetBinding> aBindings)
		{
			if (aBTAsset == null)
			{
				ResetBehavior();
				_behaviorRoot = null;
			}
			_behaviorTreeAsset = aBTAsset;
			_behaviorTreeBindings = aBindings;
			if (_behaviorTreeBindings == null)
			{
				_behaviorTreeBindings = new List<BTAssetBinding>();
			}
			if (_behaviorTreeAsset != null)
			{
				_behaviorRoot = BTLoader.Load(_behaviorTreeAsset, _behaviorTreeBindings);
			}
		}

		[Obsolete("Use BehaviorRoot property instead")]
		public virtual void SetBehaviorRoot(BTNode node)
		{
			BehaviorRoot = node;
		}

		[Obsolete("Use BehaviorRoot property instead")]
		public virtual BTNode GetBehaviorRoot()
		{
			return BehaviorRoot;
		}

		public virtual void ResetBehavior()
		{
			if (_behaviorRoot != null)
			{
				if (_behaviorRoot.ActionState == RAINAction.ActionResult.RUNNING)
				{
					_behaviorRoot.Stop(AI);
				}
				_behaviorRoot.Reset();
			}
		}

		public BTAsset GetTreeForBinding(string aBinding)
		{
			for (int i = 0; i < _behaviorTreeBindings.Count; i++)
			{
				if (_behaviorTreeBindings[i].binding == aBinding)
				{
					return _behaviorTreeBindings[i].behaviorTree;
				}
			}
			return null;
		}

		public void SetTreeForBinding(string aBinding, BTAsset aTree)
		{
			for (int i = 0; i < _behaviorTreeBindings.Count; i++)
			{
				if (_behaviorTreeBindings[i].binding == aBinding)
				{
					_behaviorTreeBindings[i].behaviorTree = aTree;
					break;
				}
			}
			UpdateTreeBindings();
		}

		public void ReloadBinding(string aBinding)
		{
			if (_behaviorRoot == null || string.IsNullOrEmpty(aBinding))
			{
				return;
			}
			Queue<BTNode> queue = new Queue<BTNode>();
			queue.Enqueue(_behaviorRoot);
			while (queue.Count > 0)
			{
				BTNode bTNode = queue.Dequeue();
				if (bTNode is BTDynamicBindingNode && ((BTDynamicBindingNode)bTNode).bindingName == aBinding)
				{
					if (bTNode.ActionState == RAINAction.ActionResult.RUNNING)
					{
						bTNode.Stop(AI);
					}
					((BTDynamicBindingNode)bTNode).LoadTree(_behaviorTreeBindings);
				}
				else
				{
					for (int i = 0; i < bTNode.GetChildCount(); i++)
					{
						queue.Enqueue(bTNode.GetChild(i));
					}
				}
			}
		}

		public bool UpdateTreeBindings()
		{
			List<string> allTreeBindings = GetAllTreeBindings();
			List<BTAssetBinding> list = new List<BTAssetBinding>(_behaviorTreeBindings);
			bool flag = allTreeBindings.Count != list.Count;
			List<BTAssetBinding> list2 = new List<BTAssetBinding>();
			for (int i = 0; i < allTreeBindings.Count; i++)
			{
				BTAssetBinding bTAssetBinding = null;
				for (int j = 0; j < list.Count; j++)
				{
					if (allTreeBindings[i] == list[j].binding)
					{
						bTAssetBinding = list[j];
						list.RemoveAt(j);
						break;
					}
				}
				if (bTAssetBinding == null)
				{
					bTAssetBinding = new BTAssetBinding();
					bTAssetBinding.binding = allTreeBindings[i];
				}
				list2.Add(bTAssetBinding);
			}
			flag |= list.Count != 0;
			if (flag)
			{
				_behaviorTreeBindings = list2;
			}
			return flag;
		}

		private List<string> GetAllTreeBindings()
		{
			List<string> list = new List<string>();
			if (_behaviorTreeAsset == null)
			{
				return list;
			}
			Stack<BTAsset> stack = new Stack<BTAsset>();
			stack.Push(_behaviorTreeAsset);
			while (stack.Count > 0)
			{
				BTAsset bTAsset = stack.Pop();
				int count = list.Count;
				string[] treeBindings = bTAsset.GetTreeBindings();
				for (int i = 0; i < treeBindings.Length; i++)
				{
					if (!list.Contains(treeBindings[i]))
					{
						list.Add(treeBindings[i]);
					}
				}
				for (int j = count; j < list.Count; j++)
				{
					for (int k = 0; k < _behaviorTreeBindings.Count; k++)
					{
						if (list[j] == _behaviorTreeBindings[k].binding && _behaviorTreeBindings[k].behaviorTree != null && !stack.Contains(_behaviorTreeBindings[k].behaviorTree))
						{
							stack.Push(_behaviorTreeBindings[k].behaviorTree);
							break;
						}
					}
				}
			}
			return list;
		}
	}
}
