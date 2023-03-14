using System.Collections.Generic;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class SpanPoint
	{
		private Point3 _point;

		private ushort _connectRegionID;

		private bool _connectBorderRegion;

		private ushort _regionID;

		public Point3 Point
		{
			get
			{
				return _point;
			}
		}

		public ushort ConnectRegionID
		{
			get
			{
				return _connectRegionID;
			}
		}

		public bool ConnectBorderRegion
		{
			get
			{
				return _connectBorderRegion;
			}
		}

		public ushort RegionID
		{
			get
			{
				return _regionID;
			}
		}

		public SpanPoint(Point3 aPoint, ushort aRegionID)
		{
			_point = aPoint;
			_regionID = aRegionID;
		}

		public SpanPoint(Point3 aPoint, CompactSpan aNeighbor, ushort aRegionID)
		{
			_point = aPoint;
			_connectRegionID = aNeighbor.RegionID;
			_connectBorderRegion = aNeighbor.BorderRegion;
			_regionID = aRegionID;
		}

		public bool IsExternalPoint()
		{
			return _connectRegionID == 0;
		}

		public void RemapRegion(Dictionary<ushort, ushort> aRemapDictionary, ref ushort aCurrentRegionID)
		{
			if (_regionID > 0)
			{
				if (!aRemapDictionary.ContainsKey(_regionID))
				{
					aRemapDictionary.Add(_regionID, aCurrentRegionID++);
				}
				_regionID = aRemapDictionary[_regionID];
			}
			if (_connectRegionID > 0)
			{
				if (!aRemapDictionary.ContainsKey(_connectRegionID))
				{
					aRemapDictionary.Add(_connectRegionID, aCurrentRegionID++);
				}
				_connectRegionID = aRemapDictionary[_connectRegionID];
			}
		}

		public void AdjustRegion(SpanPoint aNeighborPoint)
		{
			_connectRegionID = aNeighborPoint._regionID;
			_connectBorderRegion = false;
		}

		public override string ToString()
		{
			return _point.ToString();
		}

		public override bool Equals(object aPoint)
		{
			if (aPoint == null || !(aPoint is SpanPoint))
			{
				return false;
			}
			SpanPoint spanPoint = (SpanPoint)aPoint;
			return _point.Equals(spanPoint._point);
		}

		public override int GetHashCode()
		{
			return _point.GetHashCode();
		}
	}
}
