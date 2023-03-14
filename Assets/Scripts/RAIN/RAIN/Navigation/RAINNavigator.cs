using RAIN.Core;
using RAIN.Motion;
using RAIN.Navigation.Graph;
using RAIN.Navigation.Pathfinding;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Navigation
{
	[RAINSerializableClass]
	public abstract class RAINNavigator : RAINAIElement
	{
		[RAINNonSerializableField]
		public MoveLookTarget pathTarget = new MoveLookTarget();

		public abstract bool IsPathfinding { get; }

		public abstract RAINNavigationGraph CurrentGraph { get; set; }

		public abstract RAINPath CurrentPath { get; set; }

		public abstract int NextWaypoint { get; set; }

		public abstract bool OnGraph(Vector3 aPosition, float aMaxYOffset);

		public abstract Vector3 ClosestPointOnGraph(Vector3 aPosition, float aMaxYOffset);

		public abstract bool IsAt(MoveLookTarget aPosition);

		public abstract bool GetPathToMoveTarget(bool allowOffGraphMovement, out RAINPath path);

		public abstract bool GetPathTo(Vector3 position, int maxPathfindSteps, bool allowOffGraphMovement, out RAINPath path);

		public abstract MoveLookTarget GetNextPathWaypoint(bool allow3DMovement, bool allowOffGraphMovement, MoveLookTarget cachedMoveLookTarget = null);

		public virtual float PathDistanceToTarget()
		{
			if (!pathTarget.IsValid)
			{
				return 0f;
			}
			return (AI.Kinematic.Position - pathTarget.Position).magnitude;
		}

		public abstract void RestartPathfindingSearch();
	}
}
