using System.Collections.Generic;
using RAIN.Motion;
using RAIN.Navigation.Graph;
using RAIN.Navigation.NavMesh;
using RAIN.Navigation.Pathfinding;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation
{
	[RAINSerializableClass]
	public class BasicNavigator : RAINNavigator
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Draw the current path in Scene View")]
		private bool _drawPaths;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "The color of the path")]
		private Color _pathColor = Color.green;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization outline color offset")]
		private float _outlineColorOffset = -0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Maximum path finding calculations per update.", OldFieldNames = new string[] { "maxPathfindingStepsPerFrame" })]
		private int _maxPathfindingSteps = 100;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Maximum path length, or 0 for infinite.")]
		private float _maxPathLength;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Tags help match AI to available graphs")]
		private List<string> _graphTags = new List<string>();

		private RAINNavigationGraph _pathGraph;

		private RAINPath _path;

		private HeuristicPathFinder _pathfinder = new HeuristicPathFinder();

		private int _nextWaypoint;

		private Vector3 _lastTargetPosition = Vector3.zero;

		private NavigationGraphNode _lastTargetQuantize;

		public int MaxPathfindingSteps
		{
			get
			{
				return _maxPathfindingSteps;
			}
			set
			{
				_maxPathfindingSteps = value;
			}
		}

		public float MaxPathLength
		{
			get
			{
				return _maxPathLength;
			}
			set
			{
				_maxPathLength = value;
			}
		}

		public bool DrawPaths
		{
			get
			{
				return _drawPaths;
			}
			set
			{
				_drawPaths = value;
			}
		}

		public Color PathColor
		{
			get
			{
				return _pathColor;
			}
			set
			{
				_pathColor = value;
			}
		}

		public float OutlineColorOffset
		{
			get
			{
				return _outlineColorOffset;
			}
			set
			{
				_outlineColorOffset = value;
			}
		}

		public IList<string> GraphTags
		{
			get
			{
				return _graphTags.AsReadOnly();
			}
		}

		public override bool IsPathfinding
		{
			get
			{
				if (_pathfinder == null)
				{
					return false;
				}
				return _pathfinder.InProgress;
			}
		}

		public override RAINNavigationGraph CurrentGraph
		{
			get
			{
				return _pathGraph;
			}
			set
			{
				if (value != _pathGraph)
				{
					_pathGraph = value;
					_pathfinder.Reset();
					CurrentPath = null;
				}
			}
		}

		public override RAINPath CurrentPath
		{
			get
			{
				return _path;
			}
			set
			{
				if (_path != value)
				{
					_path = value;
					NextWaypoint = 0;
				}
			}
		}

		public override int NextWaypoint
		{
			get
			{
				return _nextWaypoint;
			}
			set
			{
				if (CurrentPath != null)
				{
					_nextWaypoint = Mathf.Clamp(value, 0, CurrentPath.GetLastWaypoint());
				}
			}
		}

		public override void BodyInit()
		{
			base.BodyInit();
			CurrentGraph = null;
		}

		public override void Start()
		{
			base.Start();
			SetCurrentPathGraphFromPositions(AI.Kinematic.Position, AI.Kinematic.Position);
		}

		public void AddGraphTag(string aTag)
		{
			if (!_graphTags.Contains(aTag))
			{
				_graphTags.Add(aTag);
			}
			if (CurrentGraph != null && !NavigationManager.Instance.TagsOverlap(_graphTags, CurrentGraph.tags))
			{
				_pathfinder.Reset();
				CurrentPath = null;
			}
		}

		public void RemoveGraphTag(string aTag)
		{
			_graphTags.Remove(aTag);
			if (CurrentGraph != null && !NavigationManager.Instance.TagsOverlap(_graphTags, CurrentGraph.tags))
			{
				_pathfinder.Reset();
				CurrentPath = null;
			}
		}

		public void RemoveAllGraphTags()
		{
			_graphTags.Clear();
		}

		public bool IsPathGraphValid(RAINNavigationGraph aGraph, Vector3 aPoint1, Vector3 aPoint2)
		{
			if (aGraph == null)
			{
				return false;
			}
			if (aGraph.QuantizeToNode(aPoint1, AI.Motor.StepUpHeight) == null)
			{
				return false;
			}
			if (aGraph.QuantizeToNode(aPoint2, AI.Motor.StepUpHeight) == null)
			{
				return false;
			}
			return true;
		}

		public virtual void SetCurrentPathGraphFromPositions(Vector3 aPoint1, Vector3 aPoint2)
		{
			CurrentGraph = GetPathGraphFromPositions(aPoint1, aPoint2);
		}

		public virtual RAINNavigationGraph GetPathGraphFromPositions(Vector3 aPoint1, Vector3 aPoint2)
		{
			if (CurrentGraph != null && IsPathGraphValid(CurrentGraph, aPoint1, aPoint2))
			{
				return CurrentGraph;
			}
			List<RAINNavigationGraph> list = NavigationManager.Instance.GraphsForPoints(aPoint1, aPoint2, AI.Motor.StepUpHeight, NavigationManager.GraphType.All, _graphTags);
			if (list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		public override bool OnGraph(Vector3 aPosition, float aMaxYOffset = 0f)
		{
			if (CurrentGraph == null)
			{
				return true;
			}
			return CurrentGraph.IsPointOnGraph(aPosition, aMaxYOffset);
		}

		public override Vector3 ClosestPointOnGraph(Vector3 aPosition, float aMaxYOffset = 0f)
		{
			if (CurrentGraph == null)
			{
				return aPosition;
			}
			return CurrentGraph.ClosestPointOnGraph(aPosition, aMaxYOffset);
		}

		public override bool IsAt(MoveLookTarget aPosition)
		{
			float num = Mathf.Max(aPosition.CloseEnoughDistance, AI.Motor.CloseEnoughDistance);
			if (AI.Motor.Allow3DMovement)
			{
				if ((AI.Kinematic.Position - aPosition.Position).magnitude > num)
				{
					return false;
				}
				return true;
			}
			Vector3 position = aPosition.Position;
			position.y = AI.Kinematic.Position.y;
			if ((AI.Kinematic.Position - position).magnitude > num)
			{
				return false;
			}
			MoveLookTarget nextPathWaypoint = GetNextPathWaypoint(AI.Motor.Allow3DMovement, AI.Motor.AllowOffGraphMovement);
			if (nextPathWaypoint == null || !nextPathWaypoint.IsValid)
			{
				return false;
			}
			if (CurrentGraph == null)
			{
				return true;
			}
			if (CurrentPath == null || !CurrentPath.IsValid)
			{
				if (Mathf.Abs(AI.Kinematic.Position.y - aPosition.Position.y) < AI.Motor.StepUpHeight)
				{
					return true;
				}
				return false;
			}
			NavigationGraphNode navigationGraphNode = CurrentGraph.QuantizeToNode(aPosition.Position, AI.Motor.StepUpHeight);
			NavigationGraphNode navigationGraphNode2 = CurrentGraph.QuantizeToNode(CurrentPath.GetWaypointPosition(CurrentPath.WaypointCount - 1), AI.Motor.StepUpHeight);
			if (navigationGraphNode != navigationGraphNode2)
			{
				return false;
			}
			if (CurrentPath.GetDistanceRemaining(AI.Kinematic.Position, NextWaypoint) > num)
			{
				return false;
			}
			return true;
		}

		public override bool GetPathToMoveTarget(bool allowOffGraphMovement, out RAINPath path)
		{
			if (pathTarget == null || !pathTarget.IsValid)
			{
				path = null;
				return true;
			}
			return GetPathToMoveTarget(_pathfinder, CurrentGraph, pathTarget, _maxPathfindingSteps, allowOffGraphMovement, out path);
		}

		public override bool GetPathTo(Vector3 position, int maxPathfindSteps, bool allowOffGraphMovement, out RAINPath path)
		{
			path = null;
			RAINNavigationGraph pathGraphFromPositions = GetPathGraphFromPositions(AI.Kinematic.Position, position);
			if (pathGraphFromPositions == null)
			{
				return false;
			}
			HeuristicPathFinder aFinder = new HeuristicPathFinder();
			MoveLookTarget moveLookTarget = new MoveLookTarget();
			moveLookTarget.VectorTarget = position;
			if (GetPathToMoveTarget(aFinder, pathGraphFromPositions, moveLookTarget, maxPathfindSteps, allowOffGraphMovement, out path) && path != null)
			{
				return path.IsValid;
			}
			return false;
		}

		public override MoveLookTarget GetNextPathWaypoint(bool allow3DMovement, bool allowOffGraphMovement, MoveLookTarget cachedMoveLookTarget = null)
		{
			bool flag = false;
			MoveLookTarget moveLookTarget = cachedMoveLookTarget;
			if (moveLookTarget == null)
			{
				moveLookTarget = new MoveLookTarget();
			}
			if (pathTarget == null || !pathTarget.IsValid)
			{
				moveLookTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
				return moveLookTarget;
			}
			Vector3 position = pathTarget.Position;
			if (CurrentPath != null && CurrentPath.IsPartial)
			{
				List<RAINNavigationGraph> list = NavigationManager.Instance.GraphsForPoints(AI.Kinematic.Position, AI.Kinematic.Position, AI.Motor.StepUpHeight, NavigationManager.GraphType.All, _graphTags);
				if (list.Count > 0 && !list.Contains(CurrentPath.Graph))
				{
					CurrentGraph = null;
				}
			}
			bool flag2 = false;
			if (CurrentGraph == null || (CurrentPath != null && CurrentGraph != CurrentPath.Graph) || !Mathf.Approximately((_lastTargetPosition - position).magnitude, 0f))
			{
				RAINNavigationGraph pathGraphFromPositions = GetPathGraphFromPositions(AI.Kinematic.Position, position);
				if (pathGraphFromPositions == null)
				{
					pathGraphFromPositions = GetPathGraphFromPositions(AI.Kinematic.Position, AI.Kinematic.Position);
				}
				if (pathGraphFromPositions == null)
				{
					pathGraphFromPositions = GetPathGraphFromPositions(position, position);
				}
				CurrentGraph = pathGraphFromPositions;
				flag2 = true;
			}
			if (CurrentPath == null || _pathfinder.InProgress)
			{
				flag2 = true;
			}
			if (CurrentGraph != null && flag2)
			{
				RAINPath path = null;
				bool flag3 = false;
				if (!_pathfinder.InProgress)
				{
					NavigationGraphNode navigationGraphNode = CurrentGraph.QuantizeToNode(position, AI.Motor.StepUpHeight);
					if (navigationGraphNode != null)
					{
						if (CurrentPath != null && navigationGraphNode == _lastTargetQuantize)
						{
							NavigationGraphNode graphNodeOfWaypoint = CurrentPath.GetGraphNodeOfWaypoint(NextWaypoint);
							CurrentPath.Points[CurrentPath.Points.Length - 1] = position;
							CurrentPath = new RAIN.Navigation.Pathfinding.NavMeshPath(CurrentPath.Graph, CurrentPath.GetTotalCost(), CurrentPath.Nodes, CurrentPath.Points);
							if (graphNodeOfWaypoint == null)
							{
								NextWaypoint = CurrentPath.GetLastWaypoint();
							}
							else
							{
								NextWaypoint = CurrentPath.GetWaypointOfGraphNode(graphNodeOfWaypoint);
							}
						}
						else
						{
							_lastTargetQuantize = navigationGraphNode;
							flag3 = GetPathToMoveTarget(allowOffGraphMovement, out path);
						}
					}
					else
					{
						_lastTargetQuantize = null;
						flag3 = GetPathToMoveTarget(allowOffGraphMovement, out path);
					}
					_lastTargetPosition = position;
				}
				else
				{
					flag3 = GetPathToMoveTarget(allowOffGraphMovement, out path);
				}
				if (flag3)
				{
					CurrentPath = path;
					flag = CurrentPath == null || !CurrentPath.IsValid;
				}
			}
			else
			{
				_lastTargetPosition = position;
				if (_pathfinder.InProgress)
				{
					_pathfinder.Reset();
				}
			}
			if (!flag && CurrentPath != null)
			{
				CurrentPath.Allow3DMovement = allow3DMovement;
				NextWaypoint = CurrentPath.GetNextWaypoint(AI.Kinematic.Position, AI.Motor.CloseEnoughDistance, Mathf.Max(NextWaypoint - 1, 0));
				moveLookTarget.VectorTarget = CurrentPath.GetWaypointPosition(NextWaypoint);
			}
			else
			{
				moveLookTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
			}
			return moveLookTarget;
		}

		public override float PathDistanceToTarget()
		{
			if (CurrentPath == null || !CurrentPath.IsValid)
			{
				return base.PathDistanceToTarget();
			}
			return CurrentPath.GetDistanceRemaining(AI.Kinematic.Position, NextWaypoint);
		}

		public override void RestartPathfindingSearch()
		{
			if (_pathfinder != null)
			{
				_pathfinder.Reset();
			}
		}

		private bool GetPathToMoveTarget(HeuristicPathFinder aFinder, RAINNavigationGraph aGraph, MoveLookTarget aTarget, int aMaxIterations, bool aAllowOffGraphMovement, out RAINPath aPath)
		{
			bool flag;
			List<NavigationGraphNode> nodes;
			List<Vector3> positions;
			float cost;
			if (aFinder.InProgress)
			{
				if (aFinder.StartNode != null && aFinder.StartNode is NavMeshPoly)
				{
					((NavMeshPoly)aFinder.StartNode).AddConnectedPoly(aFinder.StartPosition);
				}
				if (aFinder.GoalNode != null && aFinder.GoalNode is NavMeshPoly)
				{
					((NavMeshPoly)aFinder.GoalNode).AddConnectedPoly(aFinder.GoalPosition);
				}
				flag = aFinder.ComputePath(aMaxIterations, out nodes, out positions, out cost);
				if (aFinder.StartNode != null && aFinder.StartNode is NavMeshPoly)
				{
					((NavMeshPoly)aFinder.StartNode).RemoveConnectedPoly();
				}
				if (aFinder.GoalNode != null && aFinder.GoalNode is NavMeshPoly)
				{
					((NavMeshPoly)aFinder.GoalNode).RemoveConnectedPoly();
				}
			}
			else
			{
				NavigationGraphNode navigationGraphNode = aGraph.QuantizeToNode(AI.Kinematic.Position, AI.Motor.StepUpHeight);
				NavigationGraphNode navigationGraphNode2 = aGraph.QuantizeToNode(aTarget.Position, AI.Motor.StepUpHeight);
				if (navigationGraphNode == null && navigationGraphNode2 == null)
				{
					aPath = null;
					return true;
				}
				if (navigationGraphNode != null && navigationGraphNode is NavMeshPoly)
				{
					((NavMeshPoly)navigationGraphNode).AddConnectedPoly(AI.Kinematic.Position);
				}
				if (navigationGraphNode2 != null && navigationGraphNode2 is NavMeshPoly)
				{
					((NavMeshPoly)navigationGraphNode2).AddConnectedPoly(aTarget.Position);
				}
				if (navigationGraphNode == null)
				{
					aFinder.InitializePathfinding(aGraph, navigationGraphNode2, navigationGraphNode, aTarget.Position, AI.Kinematic.Position, _maxPathLength, true);
				}
				else
				{
					aFinder.InitializePathfinding(aGraph, navigationGraphNode, navigationGraphNode2, AI.Kinematic.Position, aTarget.Position, _maxPathLength, false);
				}
				flag = aFinder.ComputePath(aMaxIterations, out nodes, out positions, out cost);
				if (navigationGraphNode != null && navigationGraphNode is NavMeshPoly)
				{
					((NavMeshPoly)navigationGraphNode).RemoveConnectedPoly();
				}
				if (navigationGraphNode2 != null && navigationGraphNode2 is NavMeshPoly)
				{
					((NavMeshPoly)navigationGraphNode2).RemoveConnectedPoly();
				}
			}
			if (flag && nodes != null && positions != null && positions.Count > 0)
			{
				if (!aFinder.LastPathSuccessful)
				{
					if (aFinder.ReversePath)
					{
						positions[positions.Count - 1] = aTarget.Position;
						positions.Insert(0, AI.Kinematic.Position);
						nodes.Insert(0, null);
					}
					else if (!aAllowOffGraphMovement)
					{
						positions[0] = AI.Kinematic.Position;
					}
					else
					{
						positions[0] = AI.Kinematic.Position;
						positions.Add(aTarget.Position);
						nodes.Add(null);
					}
				}
				else
				{
					positions[0] = AI.Kinematic.Position;
					positions[positions.Count - 1] = aTarget.Position;
				}
				aPath = new RAIN.Navigation.Pathfinding.NavMeshPath(aGraph, cost, nodes.ToArray(), positions.ToArray());
				aPath.IsPartial = !aFinder.LastPathSuccessful;
			}
			else
			{
				aPath = null;
			}
			return flag;
		}
	}
}
