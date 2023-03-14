using System;
using System.Collections.Generic;
using RAIN.Navigation.Graph;
using RAIN.Navigation.NavMesh;
using RAIN.Navigation.Targets;
using RAIN.Navigation.Waypoints;
using UnityEngine;

namespace RAIN.Navigation
{
	public class NavigationManager
	{
		public enum GraphType : byte
		{
			Waypoint = 1,
			Navmesh = 2,
			Custom = 4,
			All = 7
		}

		private static NavigationManager _instance = new NavigationManager();

		private List<WaypointSet> waypointSets = new List<WaypointSet>();

		private List<NavigationTarget> navigationTargets = new List<NavigationTarget>();

		private List<NavMeshPathGraph> _navMeshGraphs = new List<NavMeshPathGraph>();

		private List<RAINNavigationGraph> customGraphs = new List<RAINNavigationGraph>();

		private List<RAINNavigationGraph> graphs = new List<RAINNavigationGraph>();

		[Obsolete("Use Instance instead of instance (note the capital I)", true)]
		public static NavigationManager instance
		{
			get
			{
				return Instance;
			}
		}

		public static NavigationManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new NavigationManager();
				}
				return _instance;
			}
		}

		public IList<NavMeshPathGraph> NavMeshGraphs
		{
			get
			{
				return _navMeshGraphs.AsReadOnly();
			}
		}

		private NavigationManager()
		{
		}

		public void Register(NavigationTarget aTarget)
		{
			if (aTarget != null && !navigationTargets.Contains(aTarget))
			{
				navigationTargets.Add(aTarget);
			}
		}

		public void Unregister(NavigationTarget aTarget)
		{
			if (aTarget != null)
			{
				navigationTargets.Remove(aTarget);
			}
		}

		public NavigationTarget GetNavigationTarget(string aTargetName)
		{
			if (string.IsNullOrEmpty(aTargetName))
			{
				return null;
			}
			for (int i = 0; i < navigationTargets.Count; i++)
			{
				if (navigationTargets[i] != null && navigationTargets[i].TargetName == aTargetName)
				{
					return navigationTargets[i];
				}
			}
			return null;
		}

		public void Register(WaypointSet aWaypointSet)
		{
			if (aWaypointSet != null && !waypointSets.Contains(aWaypointSet))
			{
				waypointSets.Add(aWaypointSet);
			}
		}

		public void Unregister(WaypointSet aWaypointSet)
		{
			if (aWaypointSet != null)
			{
				waypointSets.Remove(aWaypointSet);
			}
		}

		public WaypointSet GetWaypointSet(string aWaypointSetName)
		{
			if (string.IsNullOrEmpty(aWaypointSetName))
			{
				return null;
			}
			for (int i = 0; i < waypointSets.Count; i++)
			{
				if (waypointSets[i] != null && waypointSets[i].Name == aWaypointSetName)
				{
					return waypointSets[i];
				}
			}
			return null;
		}

		public void Register(RAINNavigationGraph aGraph)
		{
			if (aGraph != null && !graphs.Contains(aGraph))
			{
				graphs.Add(aGraph);
				if (aGraph is NavMeshPathGraph)
				{
					_navMeshGraphs.Add((NavMeshPathGraph)aGraph);
				}
				else
				{
					customGraphs.Add(aGraph);
				}
			}
		}

		public void Unregister(RAINNavigationGraph aGraph)
		{
			if (aGraph != null)
			{
				graphs.Remove(aGraph);
				if (aGraph is NavMeshPathGraph)
				{
					_navMeshGraphs.Remove((NavMeshPathGraph)aGraph);
				}
				else
				{
					customGraphs.Remove(aGraph);
				}
			}
		}

		public RAINNavigationGraph GetNavigationGraph(string aGraphName)
		{
			if (string.IsNullOrEmpty(aGraphName))
			{
				return null;
			}
			for (int i = 0; i < graphs.Count; i++)
			{
				if (graphs[i] != null && graphs[i].graphName == aGraphName)
				{
					return graphs[i];
				}
			}
			return null;
		}

		public List<RAINNavigationGraph> GraphForPoint(Vector3 aPoint, float aMaxYOffset = 0f, GraphType graphTypes = GraphType.All, IList<string> aTags = null)
		{
			List<RAINNavigationGraph> list = new List<RAINNavigationGraph>();
			if ((int)(graphTypes & GraphType.Custom) > 0)
			{
				for (int i = 0; i < customGraphs.Count; i++)
				{
					if (customGraphs[i].IsPointOnGraph(aPoint, aMaxYOffset) && TagsOverlap(aTags, customGraphs[i].tags))
					{
						list.Add(customGraphs[i]);
					}
				}
			}
			if ((int)(graphTypes & GraphType.Navmesh) > 0)
			{
				for (int j = 0; j < _navMeshGraphs.Count; j++)
				{
					if (_navMeshGraphs[j].IsPointOnGraph(aPoint, aMaxYOffset) && TagsOverlap(aTags, _navMeshGraphs[j].tags))
					{
						list.Add(_navMeshGraphs[j]);
					}
				}
			}
			return list;
		}

		public List<RAINNavigationGraph> GraphsForPoints(Vector3 aPoint1, Vector3 aPoint2, float aMaxYOffset = 0f, GraphType aGraphTypes = GraphType.All, IList<string> aTags = null)
		{
			List<RAINNavigationGraph> list = new List<RAINNavigationGraph>();
			if ((int)(aGraphTypes & GraphType.Custom) > 0)
			{
				for (int i = 0; i < customGraphs.Count; i++)
				{
					if (customGraphs[i].IsPointOnGraph(aPoint1, aMaxYOffset) && customGraphs[i].IsPointOnGraph(aPoint2, aMaxYOffset) && TagsOverlap(aTags, customGraphs[i].tags))
					{
						list.Add(customGraphs[i]);
					}
				}
			}
			if ((int)(aGraphTypes & GraphType.Navmesh) > 0)
			{
				for (int j = 0; j < _navMeshGraphs.Count; j++)
				{
					if (_navMeshGraphs[j].IsPointOnGraph(aPoint1, aMaxYOffset) && _navMeshGraphs[j].IsPointOnGraph(aPoint2, aMaxYOffset) && TagsOverlap(aTags, _navMeshGraphs[j].tags))
					{
						list.Add(_navMeshGraphs[j]);
					}
				}
			}
			return list;
		}

		public bool TagsOverlap(IList<string> aTagsToMatch, IList<string> aMatchList, bool emptyTagsMatchesAll = true)
		{
			if (aTagsToMatch == null || aTagsToMatch.Count == 0)
			{
				return emptyTagsMatchesAll;
			}
			if (aMatchList == null || aMatchList.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < aTagsToMatch.Count; i++)
			{
				for (int j = 0; j < aMatchList.Count; j++)
				{
					if (aTagsToMatch[i] == aMatchList[j])
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
