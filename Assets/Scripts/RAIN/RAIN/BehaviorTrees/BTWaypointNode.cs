using System;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Navigation.Graph;
using RAIN.Navigation.Pathfinding;
using RAIN.Navigation.Targets;
using RAIN.Navigation.Waypoints;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees
{
	public class BTWaypointNode : BTNode
	{
		public enum WaypointActionType
		{
			PATROL = 0,
			PATH = 1,
			CUSTOM = 2
		}

		public enum WaypointTraverseType
		{
			PINGPONG = 0,
			LOOP = 1,
			ONEWAY = 2
		}

		public enum WaypointTraverseOrder
		{
			FORWARD = 0,
			REVERSE = 1
		}

		public const string ACTIONNAME = "waypoint";

		public const string PATROLACTIONNAME = "waypointpatrol";

		public const string PATHACTIONNAME = "waypointpath";

		public const string CUSTOMACTIONNAME = "waypointcustom";

		public const string NODETYPE = "waypoint";

		public WaypointActionType waypointActionType;

		public WaypointTraverseType traverseType;

		public WaypointTraverseOrder traverseOrder;

		public Expression waypointSetVariable;

		public Expression pathTargetExpression;

		public string moveTargetVariable;

		protected WaypointSet waypointSet;

		protected int startWaypoint = -1;

		protected int endWaypoint = -1;

		protected int currentWaypoint = -1;

		protected ActionResult waypointResult = ActionResult.NONE;

		protected RAINPath waypointPath;

		protected AStarPathFinder pathFinder = new AStarPathFinder();

		protected MoveLookTarget moveMoveLookTarget = new MoveLookTarget();

		protected MoveLookTarget pathMoveLookTarget = new MoveLookTarget();

		protected int lastRunning = -1;

		protected bool firstLoop;

		public override void Start(AI ai)
		{
			lastRunning = -1;
			waypointResult = ActionResult.FAILURE;
			WaypointSet waypointSet = this.waypointSet;
			this.waypointSet = null;
			if (waypointSetVariable != null && waypointSetVariable.IsValid)
			{
				if (waypointSetVariable.IsVariable)
				{
					string variableName = waypointSetVariable.VariableName;
					if (ai.WorkingMemory.ItemExists(variableName))
					{
						Type itemType = ai.WorkingMemory.GetItemType(variableName);
						if (itemType == typeof(WaypointRig) || itemType.IsSubclassOf(typeof(WaypointRig)))
						{
							WaypointRig item = ai.WorkingMemory.GetItem<WaypointRig>(variableName);
							if (item != null)
							{
								this.waypointSet = item.WaypointSet;
							}
						}
						else if (itemType == typeof(WaypointSet) || itemType.IsSubclassOf(typeof(WaypointSet)))
						{
							this.waypointSet = ai.WorkingMemory.GetItem<WaypointSet>(variableName);
						}
						else if (itemType == typeof(GameObject))
						{
							GameObject item2 = ai.WorkingMemory.GetItem<GameObject>(variableName);
							if (item2 != null)
							{
								WaypointRig componentInChildren = item2.GetComponentInChildren<WaypointRig>();
								if (componentInChildren != null)
								{
									this.waypointSet = componentInChildren.WaypointSet;
								}
							}
						}
						else
						{
							string item3 = ai.WorkingMemory.GetItem<string>(variableName);
							if (!string.IsNullOrEmpty(item3))
							{
								this.waypointSet = NavigationManager.Instance.GetWaypointSet(item3);
							}
						}
					}
					else if (!string.IsNullOrEmpty(variableName))
					{
						this.waypointSet = NavigationManager.Instance.GetWaypointSet(variableName);
					}
				}
				else if (waypointSetVariable.IsConstant)
				{
					this.waypointSet = NavigationManager.Instance.GetWaypointSet(waypointSetVariable.Evaluate<string>(0f, ai.WorkingMemory));
				}
			}
			firstLoop = true;
			if (this.waypointSet != null)
			{
				if (this.waypointSet.SetType == WaypointSet.WaypointSetType.Network)
				{
					Restart(ai);
				}
				else if (waypointSet != this.waypointSet || currentWaypoint < 0)
				{
					Restart(ai);
				}
			}
			base.Start(ai);
		}

		public override ActionResult Execute(AI ai)
		{
			if (_children.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			if (waypointSet == null)
			{
				return ActionResult.FAILURE;
			}
			if (waypointSet.Waypoints.Count == 0)
			{
				return ActionResult.SUCCESS;
			}
			ActionResult actionResult = ActionResult.SUCCESS;
			for (int i = 0; i < waypointSet.Waypoints.Count; i++)
			{
				if (lastRunning < 0)
				{
					ResetChildren();
					if (waypointActionType == WaypointActionType.PATROL)
					{
						waypointResult = DoPatrol(ai);
					}
					else if (waypointActionType == WaypointActionType.PATH)
					{
						waypointResult = DoPath(ai);
					}
					else if (waypointActionType == WaypointActionType.CUSTOM)
					{
						waypointResult = DoCustom(ai);
					}
					else
					{
						waypointResult = ActionResult.FAILURE;
					}
					if (waypointResult != ActionResult.RUNNING)
					{
						return waypointResult;
					}
					firstLoop = false;
					lastRunning = 0;
				}
				actionResult = ActionResult.FAILURE;
				while (lastRunning < _children.Count)
				{
					actionResult = _children[lastRunning].Run(ai);
					if (actionResult != 0)
					{
						break;
					}
					lastRunning++;
				}
				if (actionResult != 0)
				{
					break;
				}
				lastRunning = -1;
				actionResult = ActionResult.RUNNING;
			}
			return actionResult;
		}

		protected virtual ActionResult DoPatrol(AI ai)
		{
			if (currentWaypoint >= waypointSet.Waypoints.Count)
			{
				currentWaypoint = waypointSet.Waypoints.Count - 1;
			}
			if (currentWaypoint < 0)
			{
				currentWaypoint = 0;
			}
			if (traverseOrder == WaypointTraverseOrder.FORWARD)
			{
				if (!firstLoop)
				{
					currentWaypoint++;
				}
				if (currentWaypoint >= waypointSet.Waypoints.Count)
				{
					if (traverseType == WaypointTraverseType.ONEWAY)
					{
						currentWaypoint = -1;
						return ActionResult.SUCCESS;
					}
					if (traverseType == WaypointTraverseType.PINGPONG)
					{
						traverseOrder = WaypointTraverseOrder.REVERSE;
						currentWaypoint = Mathf.Max(currentWaypoint - 2, 0);
					}
					else if (traverseType == WaypointTraverseType.LOOP)
					{
						currentWaypoint = 0;
					}
				}
			}
			else
			{
				if (!firstLoop)
				{
					currentWaypoint--;
				}
				if (currentWaypoint < 0)
				{
					if (traverseType == WaypointTraverseType.ONEWAY)
					{
						currentWaypoint = -1;
						return ActionResult.SUCCESS;
					}
					if (traverseType == WaypointTraverseType.PINGPONG)
					{
						traverseOrder = WaypointTraverseOrder.FORWARD;
						currentWaypoint = Mathf.Min(1, waypointSet.Waypoints.Count - 1);
					}
					else if (traverseType == WaypointTraverseType.LOOP)
					{
						currentWaypoint = waypointSet.Waypoints.Count - 1;
					}
				}
			}
			moveMoveLookTarget.VectorTarget = waypointSet.Waypoints[currentWaypoint].position;
			moveMoveLookTarget.CloseEnoughDistance = Mathf.Max(waypointSet.Waypoints[currentWaypoint].range, ai.Motor.CloseEnoughDistance);
			ai.WorkingMemory.SetItem(moveTargetVariable, moveMoveLookTarget);
			return ActionResult.RUNNING;
		}

		protected virtual ActionResult DoPath(AI ai)
		{
			if (waypointPath == null)
			{
				if (!pathMoveLookTarget.IsValid)
				{
					return ActionResult.FAILURE;
				}
				List<NavigationGraphNode> aNodes;
				List<Vector3> aPositions;
				float aCost;
				if (pathFinder.InProgress)
				{
					if (!pathFinder.ContinuePath(out aNodes, out aPositions, out aCost))
					{
						return ActionResult.RUNNING;
					}
				}
				else
				{
					NavigationGraphNode aFromNode = waypointSet.Graph.QuantizeToNode(ai.Kinematic.Position, ai.Motor.StepUpHeight);
					NavigationGraphNode aToNode = waypointSet.Graph.QuantizeToNode(pathMoveLookTarget.Position, ai.Motor.StepUpHeight);
					if (!pathFinder.FindPath(aFromNode, aToNode, 100, out aNodes, out aPositions, out aCost))
					{
						return ActionResult.RUNNING;
					}
				}
				if (aNodes.Count == 0)
				{
					return ActionResult.FAILURE;
				}
				waypointPath = new BasicPath(waypointSet.Graph, aCost, aNodes.ToArray(), aPositions.ToArray());
				currentWaypoint = 0;
				if (waypointPath.IsValid && waypointPath.Nodes.Length <= 2 && (waypointPath.Nodes.Length < 2 || waypointPath.Nodes[0] == waypointPath.Nodes[1]))
				{
					currentWaypoint = waypointPath.Nodes.Length;
				}
			}
			else if (!firstLoop)
			{
				currentWaypoint++;
			}
			if (!waypointPath.IsValid)
			{
				return ActionResult.FAILURE;
			}
			if (currentWaypoint < waypointPath.WaypointCount)
			{
				VectorPathNode vectorPathNode = waypointPath.GetGraphNodeOfWaypoint(currentWaypoint) as VectorPathNode;
				moveMoveLookTarget.VectorTarget = waypointSet.Waypoints[vectorPathNode.NodeIndex].position;
				moveMoveLookTarget.CloseEnoughDistance = Mathf.Max(waypointSet.Waypoints[vectorPathNode.NodeIndex].range, ai.Motor.CloseEnoughDistance);
			}
			if (currentWaypoint < waypointPath.WaypointCount)
			{
				ai.WorkingMemory.SetItem(moveTargetVariable, moveMoveLookTarget);
			}
			else
			{
				if (currentWaypoint != waypointPath.WaypointCount)
				{
					return ActionResult.SUCCESS;
				}
				ai.WorkingMemory.SetItem(moveTargetVariable, pathMoveLookTarget);
			}
			return ActionResult.RUNNING;
		}

		protected virtual ActionResult DoCustom(AI ai)
		{
			return ActionResult.FAILURE;
		}

		protected virtual void Restart(AI ai)
		{
			startWaypoint = -1;
			waypointPath = null;
			firstLoop = true;
			if (waypointSet == null)
			{
				return;
			}
			if (waypointActionType == WaypointActionType.PATROL)
			{
				if (waypointSet.Waypoints.Count <= 0)
				{
					return;
				}
				startWaypoint = waypointSet.GetNextSequentialWaypointIndex(ai.Kinematic.Position, traverseType == WaypointTraverseType.LOOP);
				if (traverseOrder == WaypointTraverseOrder.REVERSE)
				{
					if (startWaypoint > 0)
					{
						startWaypoint--;
					}
					else if (traverseType == WaypointTraverseType.LOOP)
					{
						startWaypoint = waypointSet.Waypoints.Count - 1;
					}
				}
				currentWaypoint = startWaypoint;
			}
			else
			{
				if (waypointActionType != WaypointActionType.PATH)
				{
					return;
				}
				waypointPath = null;
				if (pathTargetExpression.IsValid)
				{
					if (pathTargetExpression.IsVariable)
					{
						pathMoveLookTarget.SetVariableTarget(pathTargetExpression.VariableName, ai.WorkingMemory);
						return;
					}
					object obj = pathTargetExpression.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
					if (obj is Vector3)
					{
						pathMoveLookTarget.VectorTarget = (Vector3)obj;
					}
					else if (obj is string)
					{
						string text = (string)obj;
						NavigationTarget navigationTarget = NavigationManager.Instance.GetNavigationTarget(text);
						if (navigationTarget == null)
						{
							throw new Exception("'" + text + "' does not match the name of any navigation target");
						}
						pathMoveLookTarget.NavigationTarget = navigationTarget;
					}
					else if (obj != null)
					{
						pathMoveLookTarget.ObjectTarget = obj;
					}
					else
					{
						pathMoveLookTarget.NavigationTarget = null;
					}
				}
				else
				{
					pathMoveLookTarget.NavigationTarget = null;
				}
			}
		}
	}
}
