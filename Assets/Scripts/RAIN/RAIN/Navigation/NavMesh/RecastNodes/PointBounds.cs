namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class PointBounds
	{
		private Point3 _center;

		private Point3 _extents;

		private int _hash;

		public Point3 Center
		{
			get
			{
				return _center;
			}
			set
			{
				_center = value;
			}
		}

		public Point3 Extents
		{
			get
			{
				return _extents;
			}
			set
			{
				_extents = value;
			}
		}

		public Point3 Size
		{
			get
			{
				return _extents * 2;
			}
			set
			{
				_extents = value * 0.5f;
			}
		}

		public Point3 Min
		{
			get
			{
				return _center - _extents;
			}
		}

		public Point3 Max
		{
			get
			{
				return _center + _extents;
			}
		}

		public PointBounds(Point3 aCenter, Point3 aExtents)
		{
			_center = aCenter;
			_extents = aExtents;
			_hash = _center.GetHashCode() + _extents.GetHashCode();
		}

		public bool Intersects(PointBounds aBounds)
		{
			Point3 max = Max;
			Point3 min = Min;
			Point3 max2 = aBounds.Max;
			Point3 min2 = aBounds.Min;
			if (max.X >= min2.X && max.Y >= min2.Y && max.Z >= min2.Z && min.X <= max2.X && min.Y <= max2.Y)
			{
				return min.Z <= max2.Z;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _hash;
		}

		public override bool Equals(object aPointBounds)
		{
			PointBounds pointBounds = aPointBounds as PointBounds;
			if (pointBounds == null)
			{
				return false;
			}
			if (pointBounds._center == _center)
			{
				return pointBounds._extents == _extents;
			}
			return false;
		}

		public override string ToString()
		{
			return _center.ToString() + ", " + _extents.ToString();
		}
	}
}
