using UnityEngine;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class Point3
	{
		private int _x;

		private int _y;

		private int _z;

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

		public int Z
		{
			get
			{
				return _z;
			}
			set
			{
				_z = value;
			}
		}

		public Point3(Point3 aPoint)
		{
			_x = aPoint._x;
			_y = aPoint._y;
			_z = aPoint._z;
			_hash = _x + _y + _z;
		}

		public Point3(int aX, int aY, int aZ)
		{
			_x = aX;
			_y = aY;
			_z = aZ;
			_hash = _x + _y + _z;
		}

		public float GetLength()
		{
			return Mathf.Sqrt(GetLengthSquared());
		}

		public int GetLengthSquared()
		{
			return _x * _x + _y * _y + _z * _z;
		}

		public static Point3 RoundToPoint(Vector3 aVector)
		{
			return new Point3(Mathf.RoundToInt(aVector.x), Mathf.RoundToInt(aVector.y), Mathf.RoundToInt(aVector.z));
		}

		public static float Dot(Point3 aPointA, Point3 aPointB)
		{
			return aPointA._x * aPointB._x + aPointA._y * aPointB._y + aPointA._z * aPointB._z;
		}

		public static Point3 Min(Point3 aPointA, Point3 aPointB)
		{
			return new Point3(Mathf.Min(aPointA._x, aPointB._x), Mathf.Min(aPointA._y, aPointB._y), Mathf.Min(aPointA._z, aPointB._z));
		}

		public static Point3 Max(Point3 aPointA, Point3 aPointB)
		{
			return new Point3(Mathf.Max(aPointA._x, aPointB._x), Mathf.Max(aPointA._y, aPointB._y), Mathf.Max(aPointA._z, aPointB._z));
		}

		public static explicit operator Vector3(Point3 aPoint)
		{
			return new Vector3(aPoint.X, aPoint.Y, aPoint.Z);
		}

		public static Point3 operator +(Point3 aPointA, Point3 aPointB)
		{
			return new Point3(aPointA._x + aPointB._x, aPointA._y + aPointB._y, aPointA._z + aPointB._z);
		}

		public static Point3 operator -(Point3 aPointA, Point3 aPointB)
		{
			return new Point3(aPointA._x - aPointB._x, aPointA._y - aPointB._y, aPointA._z - aPointB._z);
		}

		public static Point3 operator *(Point3 aPoint, int aScalar)
		{
			return new Point3(aPoint._x * aScalar, aPoint._y * aScalar, aPoint._z * aScalar);
		}

		public static Point3 operator /(Point3 aPoint, int aScalar)
		{
			return new Point3(aPoint._x / aScalar, aPoint._y / aScalar, aPoint._z / aScalar);
		}

		public static Point3 operator *(Point3 aPoint, float aScalar)
		{
			return new Point3(Mathf.RoundToInt((float)aPoint._x * aScalar), Mathf.RoundToInt((float)aPoint._y * aScalar), Mathf.RoundToInt((float)aPoint._z * aScalar));
		}

		public static Point3 operator /(Point3 aPoint, float aScalar)
		{
			return new Point3(Mathf.RoundToInt((float)aPoint._x / aScalar), Mathf.RoundToInt((float)aPoint._y / aScalar), Mathf.RoundToInt((float)aPoint._z / aScalar));
		}

		public override int GetHashCode()
		{
			return _hash;
		}

		public override bool Equals(object aPoint)
		{
			if (aPoint == null || !(aPoint is Point3))
			{
				return false;
			}
			Point3 point = (Point3)aPoint;
			if (point._x == _x && point._y == _y)
			{
				return point._z == _z;
			}
			return false;
		}

		public override string ToString()
		{
			return _x + " " + _y + " " + _z;
		}
	}
}
