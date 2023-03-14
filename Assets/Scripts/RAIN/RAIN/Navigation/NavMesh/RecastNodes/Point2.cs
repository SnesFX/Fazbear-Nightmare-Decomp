using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class Point2
	{
		private int _x;

		private int _y;

		private int _hash;

		public int X
		{
			get
			{
				return _x;
			}
			set
			{
				_x = value;
			}
		}

		public int Y
		{
			get
			{
				return _y;
			}
			set
			{
				_y = value;
			}
		}

		public Point2(int aX, int aY)
		{
			_x = aX;
			_y = aY;
			_hash = _x + _y;
		}

		public float GetLength()
		{
			return Mathf.Sqrt(GetLengthSquared());
		}

		public int GetLengthSquared()
		{
			return _x * _x + _y * _y;
		}

		public static Point2 RoundToPoint(Vector2 aVector)
		{
			return new Point2(Mathf.RoundToInt(aVector.x), Mathf.RoundToInt(aVector.y));
		}

		public static explicit operator Vector2(Point2 aPoint)
		{
			return new Vector2(aPoint.X, aPoint.Y);
		}

		public override int GetHashCode()
		{
			return _hash;
		}

		public override bool Equals(object aPoint)
		{
			if (aPoint == null || !(aPoint is Point2))
			{
				return false;
			}
			Point2 point = (Point2)aPoint;
			if (point._x == _x)
			{
				return point._y == _y;
			}
			return false;
		}

		public override string ToString()
		{
			return _x + " " + _y;
		}

		public static Point2 operator +(Point2 aPointA, Point2 aPointB)
		{
			return new Point2(aPointA._x + aPointB._x, aPointA._y + aPointB._y);
		}

		public static Point2 operator -(Point2 aPointA, Point2 aPointB)
		{
			return new Point2(aPointA._x - aPointB._x, aPointA._y - aPointB._y);
		}
	}
}
