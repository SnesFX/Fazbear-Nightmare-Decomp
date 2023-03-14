using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.Graph
{
	public class NavigationGraphNode
	{
		public EventHandler OnNodeChanged;

		private RAINNavigationGraph _graph;

		[Obsolete]
		private int _nodeIndex = -1;

		private bool _nodeChanged;

		private List<NavigationGraphEdge> _edgesIn = new List<NavigationGraphEdge>();

		private List<NavigationGraphEdge> _edgesOut = new List<NavigationGraphEdge>();

		public RAINNavigationGraph Graph
		{
			get
			{
				return _graph;
			}
		}

		[Obsolete("NodeIndex is no longer required by the base class, reimplement in subclasses if necessary")]
		public int NodeIndex
		{
			get
			{
				return _nodeIndex;
			}
			set
			{
				_nodeIndex = value;
			}
		}

		public bool NodeChanged
		{
			get
			{
				return _nodeChanged;
			}
		}

		public int InEdgeCount
		{
			get
			{
				return _edgesIn.Count;
			}
		}

		public int OutEdgeCount
		{
			get
			{
				return _edgesOut.Count;
			}
		}

		public NavigationGraphNode(RAINNavigationGraph aGraph)
		{
			_graph = aGraph;
		}

		[Obsolete("Use NavigationGraphNode(RAINNavigationGraph aGraph) instead")]
		public NavigationGraphNode(RAINNavigationGraph aGraph, int aNodeIndex)
		{
			_graph = aGraph;
			_nodeIndex = aNodeIndex;
		}

		public void AddEdgeIn(NavigationGraphEdge aEdge)
		{
			if (aEdge != null)
			{
				if (aEdge.ToNode != this)
				{
					throw new NavigationGraphException("Attempted to call AddEdgeIn with an edge that doesn't point to this node.");
				}
				_edgesIn.Add(aEdge);
			}
		}

		public void AddEdgeOut(NavigationGraphEdge aEdge)
		{
			if (aEdge != null)
			{
				if (aEdge.FromNode != this)
				{
					throw new NavigationGraphException("Attempted to call AddEdgeOut with an edge that doesn't point from this node.");
				}
				_edgesOut.Add(aEdge);
			}
		}

		public void RemoveEdgeIn(NavigationGraphEdge aEdge)
		{
			_edgesIn.Remove(aEdge);
		}

		public void RemoveEdgeOut(NavigationGraphEdge aEdge)
		{
			_edgesOut.Remove(aEdge);
		}

		[Obsolete("Use RemoveAllEdgesTo(NavigationGraphNode aNode) instead")]
		public void RemoveAllEdgesTo(int nodeIndex)
		{
			RemoveAllEdgesTo(_graph.GetNode(nodeIndex));
		}

		public void RemoveAllEdgesTo(NavigationGraphNode aNode)
		{
			int num = 0;
			while (num < _edgesOut.Count)
			{
				if (_edgesOut[num].ToNode == aNode)
				{
					_edgesOut.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		[Obsolete("Use RemoveAllEdgesFrom(NavigationGraphNode aNode) instead")]
		public void RemoveAllEdgesFrom(int nodeIndex)
		{
			RemoveAllEdgesFrom(_graph.GetNode(nodeIndex));
		}

		public void RemoveAllEdgesFrom(NavigationGraphNode aNode)
		{
			int num = 0;
			while (num < _edgesIn.Count)
			{
				if (_edgesIn[num].FromNode == aNode)
				{
					_edgesIn.RemoveAt(num);
				}
				else
				{
					num++;
				}
			}
		}

		public NavigationGraphEdge EdgeOut(int index)
		{
			return _edgesOut[index];
		}

		public NavigationGraphEdge EdgeIn(int index)
		{
			return _edgesIn[index];
		}

		[Obsolete("Use EdgeFrom(NavigationGraphNode aNode) instead")]
		public NavigationGraphEdge EdgeFrom(int nodeIndex)
		{
			return EdgeFrom(_graph.GetNode(nodeIndex));
		}

		public NavigationGraphEdge EdgeFrom(NavigationGraphNode aNode)
		{
			for (int i = 0; i < InEdgeCount; i++)
			{
				if (_edgesIn[i].FromNode == aNode)
				{
					return _edgesIn[i];
				}
			}
			return null;
		}

		[Obsolete("Use EdgeFrom(NavigationGraphNode aNode, int aNth) instead")]
		public NavigationGraphEdge EdgeFrom(int nodeIndex, int n)
		{
			return EdgeFrom(_graph.GetNode(nodeIndex), n);
		}

		public NavigationGraphEdge EdgeFrom(NavigationGraphNode aNode, int aNth)
		{
			int num = aNth;
			for (int i = 0; i < InEdgeCount; i++)
			{
				if (_edgesIn[i].FromNode == aNode)
				{
					if (num == 0)
					{
						return _edgesIn[i];
					}
					num--;
				}
			}
			return null;
		}

		[Obsolete("Use EdgeTo(NavigationGraphNode aNode) instead")]
		public NavigationGraphEdge EdgeTo(int nodeIndex)
		{
			return EdgeTo(_graph.GetNode(nodeIndex));
		}

		public NavigationGraphEdge EdgeTo(NavigationGraphNode aNode)
		{
			for (int i = 0; i < OutEdgeCount; i++)
			{
				if (_edgesOut[i].ToNode == aNode)
				{
					return _edgesOut[i];
				}
			}
			return null;
		}

		[Obsolete("Use EdgeTo(NavigationGraphNode aNode, int aNth) instead")]
		public NavigationGraphEdge EdgeTo(int nodeIndex, int n)
		{
			return EdgeTo(_graph.GetNode(nodeIndex), n);
		}

		public NavigationGraphEdge EdgeTo(NavigationGraphNode aNode, int aNth)
		{
			int num = aNth;
			for (int i = 0; i < OutEdgeCount; i++)
			{
				if (_edgesOut[i].ToNode == aNode)
				{
					if (num == 0)
					{
						return _edgesOut[i];
					}
					num--;
				}
			}
			return null;
		}

		[Obsolete("Use SetNodeChanged instead")]
		public virtual void SetDirty()
		{
			SetNodeChanged();
		}

		public virtual void SetNodeChanged()
		{
			_nodeChanged = true;
			FireNodeChanged(null);
		}

		[Obsolete("Use UpdateChangedNode instead")]
		public virtual void UpdateDirtyNode()
		{
			UpdateChangedNode();
		}

		public virtual void UpdateChangedNode()
		{
			_nodeChanged = false;
		}

		public virtual Vector3 Localize()
		{
			return Vector3.zero;
		}

		public virtual Vector3 NodeIntersection(Vector3 aPosition)
		{
			return Vector3.zero;
		}

		[Obsolete("No longer produces useful information")]
		public void DebugDump()
		{
		}

		protected virtual void FireNodeChanged(EventArgs aArgs)
		{
			EventHandler onNodeChanged = OnNodeChanged;
			if (onNodeChanged != null)
			{
				onNodeChanged(this, aArgs);
			}
		}
	}
}
