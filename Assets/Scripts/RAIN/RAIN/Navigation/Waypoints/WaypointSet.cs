using System.Collections.Generic;
using RAIN.Core;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.Waypoints
{
	[RAINSerializableClass]
	public class WaypointSet : RAINElement
	{
		public enum WaypointSetType
		{
			Route = 0,
			Network = 1,
			Custom = 2
		}

		[RAINSerializableClass]
		public class WaypointConnection
		{
			[RAINSerializableField(Visibility = FieldVisibility.Hide)]
			public int wpOne;

			[RAINSerializableField(Visibility = FieldVisibility.Hide)]
			public int wpTwo;

			[RAINSerializableField(Visibility = FieldVisibility.Hide)]
			public bool twoWay = true;
		}

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private string _setName = "";

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private WaypointSetType _setType;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization color")]
		private Color _networkColor = Color.green;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization outline color offset")]
		private float _outlineColorOffset = -0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization selected color offset")]
		private float _selectionColorOffset = 0.4f;

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<Waypoint> _waypoints = new List<Waypoint>();

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<WaypointConnection> _connections = new List<WaypointConnection>();

		[RAINNonSerializableField]
		private WaypointPathGraph _graph;

		public virtual string Name
		{
			get
			{
				return _setName;
			}
			set
			{
				_setName = value;
			}
		}

		public virtual WaypointSetType SetType
		{
			get
			{
				return _setType;
			}
			set
			{
				_setType = value;
			}
		}

		public IList<Waypoint> Waypoints
		{
			get
			{
				return _waypoints.AsReadOnly();
			}
		}

		public IList<WaypointConnection> Connections
		{
			get
			{
				return _connections.AsReadOnly();
			}
		}

		public Color NetworkColor
		{
			get
			{
				return _networkColor;
			}
			set
			{
				_networkColor = value;
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

		public float SelectionColorOffset
		{
			get
			{
				return _selectionColorOffset;
			}
			set
			{
				_selectionColorOffset = value;
			}
		}

		public WaypointPathGraph Graph
		{
			get
			{
				if (_graph == null)
				{
					GenerateWaypointGraph();
				}
				return _graph;
			}
		}

		public void AddConnection(int aWaypointIndexOne, int aWaypointIndexTwo, bool aAddTwoWay = true)
		{
			if (aWaypointIndexOne == aWaypointIndexTwo || aWaypointIndexOne < 0 || aWaypointIndexOne >= _waypoints.Count || aWaypointIndexTwo < 0 || aWaypointIndexTwo >= _waypoints.Count)
			{
				return;
			}
			for (int i = 0; i < _connections.Count; i++)
			{
				if ((aWaypointIndexOne == _connections[i].wpOne && aWaypointIndexTwo == _connections[i].wpTwo) || (aWaypointIndexTwo == _connections[i].wpOne && aWaypointIndexOne == _connections[i].wpTwo))
				{
					if (!_connections[i].twoWay && aWaypointIndexTwo == _connections[i].wpOne)
					{
						_connections[i].twoWay = true;
					}
					return;
				}
			}
			_graph = null;
			WaypointConnection waypointConnection = new WaypointConnection();
			waypointConnection.wpOne = aWaypointIndexOne;
			waypointConnection.wpTwo = aWaypointIndexTwo;
			waypointConnection.twoWay = aAddTwoWay;
			WaypointConnection item = waypointConnection;
			_connections.Add(item);
		}

		public void RemoveConnection(int aWaypointIndexOne, int aWaypointIndexTwo, bool aRemoveTwoWay = true)
		{
			for (int i = 0; i < _connections.Count; i++)
			{
				if ((aWaypointIndexOne == _connections[i].wpOne && aWaypointIndexTwo == _connections[i].wpTwo) || (aWaypointIndexTwo == _connections[i].wpOne && aWaypointIndexOne == _connections[i].wpTwo))
				{
					if (aRemoveTwoWay || aWaypointIndexOne == _connections[i].wpOne)
					{
						_connections.RemoveAt(i);
					}
					break;
				}
			}
		}

		public int AddWaypointAtLocation(Vector3 aLocation, string aWaypointName = null, int aInsertIndex = -1)
		{
			_graph = null;
			Waypoint item = new Waypoint(aLocation, aWaypointName);
			if (aInsertIndex < 0 || aInsertIndex >= _waypoints.Count)
			{
				_waypoints.Add(item);
				aInsertIndex = _waypoints.Count - 1;
			}
			else
			{
				_waypoints.Insert(aInsertIndex, item);
			}
			return aInsertIndex;
		}

		public void RemoveAllWaypoints()
		{
			_connections.Clear();
			_waypoints.Clear();
			_graph = null;
		}

		public void RemoveWaypoint(Waypoint aWaypoint)
		{
			int num = FindWaypointIndex(aWaypoint);
			if (num >= 0)
			{
				RemoveWaypoint(num);
			}
		}

		public void RemoveWaypoint(int aWaypointIndex)
		{
			for (int num = _connections.Count - 1; num >= 0; num--)
			{
				if (aWaypointIndex == _connections[num].wpOne || aWaypointIndex == _connections[num].wpTwo)
				{
					_connections.RemoveAt(num);
				}
				else
				{
					if (_connections[num].wpOne > aWaypointIndex)
					{
						_connections[num].wpOne--;
					}
					if (_connections[num].wpTwo > aWaypointIndex)
					{
						_connections[num].wpTwo--;
					}
				}
			}
			_waypoints.RemoveAt(aWaypointIndex);
			_graph = null;
		}

		public int FindWaypointIndex(Waypoint aWaypoint)
		{
			if (aWaypoint == null)
			{
				return -1;
			}
			for (int i = 0; i < _waypoints.Count; i++)
			{
				if (_waypoints[i] == aWaypoint)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetClosestWaypointIndex(Vector3 position)
		{
			int result = -1;
			float num = float.MaxValue;
			for (int i = 0; i < _waypoints.Count; i++)
			{
				float sqrMagnitude = (_waypoints[i].position - position).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					result = i;
					num = sqrMagnitude;
				}
			}
			return result;
		}

		public int GetNextSequentialWaypointIndex(Vector3 aPosition, bool aWaypointsLoop = false)
		{
			float num = -1f;
			int result = -1;
			if (_waypoints.Count > 0)
			{
				Vector3 start = _waypoints[0].position;
				for (int i = 0; i < _waypoints.Count; i++)
				{
					Vector3 position = _waypoints[i].position;
					Vector3 vector = MathUtils.FindNearestPointOnLine(aPosition, start, position);
					float magnitude = (vector - aPosition).magnitude;
					if (num < 0f || magnitude < num)
					{
						result = i;
						num = magnitude;
					}
					start = position;
				}
				if (aWaypointsLoop)
				{
					Vector3 position2 = _waypoints[0].position;
					Vector3 vector2 = MathUtils.FindNearestPointOnLine(aPosition, start, position2);
					float magnitude2 = (vector2 - aPosition).magnitude;
					if (num < 0f || magnitude2 < num)
					{
						result = 0;
						num = magnitude2;
					}
				}
			}
			return result;
		}

		public void GenerateWaypointGraph()
		{
			_graph = new WaypointPathGraph(this);
		}
	}
}
