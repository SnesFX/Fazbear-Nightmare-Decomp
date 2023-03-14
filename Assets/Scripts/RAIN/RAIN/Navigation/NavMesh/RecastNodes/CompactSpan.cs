using System;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public struct CompactSpan
	{
		public const int TopDirection = 0;

		public const int RightDirection = 1;

		public const int BottomDirection = 2;

		public const int LeftDirection = 3;

		private static int[] DirectionXOffset = new int[4] { 0, 1, 0, -1 };

		private static int[] DirectionYOffset = new int[4] { 1, 0, -1, 0 };

		private int _x;

		private int _z;

		private int _minHeight;

		private int _maxHeight;

		private ushort _distance;

		private ushort _regionID;

		private ushort _contourID;

		private ushort _floodPriority;

		private bool _visited;

		private byte _connections;

		private int[] _neighbors;

		public int X
		{
			get
			{
				return _x;
			}
		}

		public int Z
		{
			get
			{
				return _z;
			}
		}

		public int MinHeight
		{
			get
			{
				return _minHeight;
			}
		}

		public int MaxHeight
		{
			get
			{
				return _maxHeight;
			}
		}

		public ushort Distance
		{
			get
			{
				return _distance;
			}
		}

		public bool BorderRegion
		{
			get
			{
				if (_regionID > 0)
				{
					return _regionID < 5;
				}
				return false;
			}
		}

		public ushort RegionID
		{
			get
			{
				return _regionID;
			}
		}

		public ushort ContourID
		{
			get
			{
				return _contourID;
			}
		}

		public ushort FloodPriority
		{
			get
			{
				return _floodPriority;
			}
		}

		public bool Visited
		{
			get
			{
				return _visited;
			}
		}

		public CompactSpan(Span aSpan)
		{
			_x = aSpan.X;
			_z = aSpan.Z;
			_minHeight = aSpan.MinHeight;
			_maxHeight = aSpan.MaxHeight;
			_distance = 0;
			_regionID = 0;
			_contourID = 0;
			_floodPriority = 0;
			_visited = false;
			_connections = 0;
			_neighbors = new int[4] { -1, -1, -1, -1 };
		}

		public static int RotateClockwise(int aDirection)
		{
			return (aDirection + 1) & 3;
		}

		public static int RotateCounterClockwise(int aDirection)
		{
			return (aDirection + 3) & 3;
		}

		public static int GetOppositeDirection(int aDirection)
		{
			return (aDirection + 2) & 3;
		}

		public static int GetXOffset(int aDirection)
		{
			return DirectionXOffset[aDirection];
		}

		public static int GetZOffset(int aDirection)
		{
			return DirectionYOffset[aDirection];
		}

		public void SetDistance(ushort aDistance)
		{
			_distance = aDistance;
		}

		public void SetRegion(ushort aRegionID)
		{
			_regionID = aRegionID;
		}

		public void ClearRegion()
		{
			_regionID = 0;
		}

		public void SetContourID(ushort aContourID)
		{
			_contourID = aContourID;
		}

		public void SetVisited()
		{
			if (_visited)
			{
				throw new Exception("Problem setting visted");
			}
			_visited = true;
		}

		public void ClearVisited()
		{
			_visited = false;
		}

		public void SetFloodPriority(ushort aPriority)
		{
			_floodPriority = aPriority;
		}

		public bool HasNeighbor(int aDirection)
		{
			return (_connections & (1 << aDirection)) != 0;
		}

		public void SetNeighbor(int aNeighborSpan, int aDirection)
		{
			if (HasNeighbor(aDirection))
			{
				throw new Exception("Neighbor is already set: " + aDirection);
			}
			_connections |= (byte)(1 << aDirection);
			_neighbors[aDirection] = aNeighborSpan;
		}

		public int GetNeighbor(int aDirection)
		{
			return _neighbors[aDirection];
		}

		public void RemoveNeighbor(int aDirection)
		{
			if (!HasNeighbor(aDirection))
			{
				throw new Exception("Neighbor is not set: " + aDirection);
			}
			_connections &= (byte)(~(1 << aDirection));
		}

		public bool HasExternalEdge()
		{
			return _connections != 15;
		}

		public override bool Equals(object aSpan)
		{
			CompactSpan compactSpan = (CompactSpan)aSpan;
			if (_x == compactSpan._x && _z == compactSpan._z && _minHeight == compactSpan._minHeight)
			{
				return _maxHeight == compactSpan._maxHeight;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _x + _z;
		}
	}
}
