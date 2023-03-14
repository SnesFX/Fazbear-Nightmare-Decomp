using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	public class PolyBuckets
	{
		private Vector3 _origin = Vector3.zero;

		private Vector3 _step = Vector3.zero;

		private Vector3 _count = Vector3.zero;

		private List<NavMeshPoly>[] _buckets = new List<NavMeshPoly>[0];

		public PolyBuckets(Vector3 gridOrigin, Vector3 stepSize, Vector3 gridCount)
		{
			_origin = gridOrigin;
			_step = new Vector3(Mathf.Max(Mathf.Ceil(stepSize.x), 1f), 0f, Mathf.Max(Mathf.Ceil(stepSize.z), 1f));
			_count = new Vector3(Mathf.Max(Mathf.Ceil(gridCount.x), 1f), 0f, Mathf.Max(Mathf.Ceil(gridCount.z), 1f));
			_buckets = new List<NavMeshPoly>[(int)(_count.x * _count.z)];
		}

		public PolyBuckets(Bounds bounds, Vector3 stepSize)
		{
			_origin = bounds.min;
			_step = new Vector3(Mathf.Max(Mathf.Ceil(stepSize.x), 1f), 0f, Mathf.Max(Mathf.Ceil(stepSize.z), 1f));
			_count = new Vector3(Mathf.Ceil(bounds.size.x / _step.x), 0f, Mathf.Ceil(bounds.size.z / _step.z));
			_buckets = new List<NavMeshPoly>[(int)(_count.x * _count.z)];
		}

		public void AddPoly(NavMeshPoly aPoly)
		{
			int num = Mathf.Max(0, Mathf.FloorToInt((aPoly.PolyBounds.min.x - _origin.x) / _step.x));
			int num2 = Mathf.Max(0, Mathf.FloorToInt((aPoly.PolyBounds.min.z - _origin.z) / _step.z));
			int num3 = Mathf.Min((int)_count.x, Mathf.CeilToInt((aPoly.PolyBounds.max.x - _origin.x) / _step.x));
			int num4 = Mathf.Min((int)_count.z, Mathf.CeilToInt((aPoly.PolyBounds.max.z - _origin.z) / _step.z));
			for (int i = num2; i < num4; i++)
			{
				for (int j = num; j < num3; j++)
				{
					int bucket = (int)((float)i * _count.x + (float)j);
					Vector3[] array = new Vector3[4]
					{
						_origin + new Vector3(_step.x * (float)j, 0f, _step.z * (float)i),
						_origin + new Vector3(_step.x * ((float)j + 1f), 0f, _step.z * (float)i),
						_origin + new Vector3(_step.x * ((float)j + 1f), 0f, _step.z * ((float)i + 1f)),
						_origin + new Vector3(_step.x * (float)j, 0f, _step.z * ((float)i + 1f))
					};
					AddToBucket(aPoly, bucket);
				}
			}
		}

		protected void AddToBucket(NavMeshPoly poly, int bucket)
		{
			if (_buckets[bucket] == null)
			{
				_buckets[bucket] = new List<NavMeshPoly>();
			}
			_buckets[bucket].Add(poly);
		}

		public void Clear()
		{
			_buckets = new List<NavMeshPoly>[(int)(_count.x * _count.z)];
		}

		public NavMeshPoly PolyForPoint(Vector3 point, float aMaxYOffset = 0f)
		{
			Vector3 vector = point - _origin;
			if (vector.x < 0f || vector.z < 0f)
			{
				return null;
			}
			int num = Mathf.FloorToInt(vector.x / _step.x);
			int num2 = Mathf.FloorToInt(vector.z / _step.z);
			if ((float)num >= _count.x || (float)num2 >= _count.z)
			{
				return null;
			}
			int num3 = num + (int)_count.x * num2;
			List<NavMeshPoly> list = _buckets[num3];
			if (list == null)
			{
				return null;
			}
			NavMeshPoly result = null;
			float num4 = float.MaxValue;
			foreach (NavMeshPoly item in list)
			{
				Vector3 intercept;
				if (item.ContainsPoint(point) && item.GetYInterceptPoint(point, out intercept) && !(intercept.y > point.y + aMaxYOffset))
				{
					float num5 = Mathf.Abs(point.y - intercept.y);
					if (num5 < num4)
					{
						num4 = num5;
						result = item;
					}
				}
			}
			return result;
		}

		public List<NavMeshPoly> PolysForPoint(Vector3 aPoint, float aSize = 0f)
		{
			Vector3 vector = aPoint - _origin;
			int num = Mathf.Max(0, Mathf.FloorToInt((vector.x - aSize / 2f) / _step.x));
			int num2 = Mathf.Max(0, Mathf.FloorToInt((vector.z - aSize / 2f) / _step.z));
			int num3 = Mathf.Min((int)_count.x, Mathf.CeilToInt((vector.x + aSize / 2f) / _step.x));
			int num4 = Mathf.Min((int)_count.z, Mathf.CeilToInt((vector.z + aSize / 2f) / _step.z));
			Dictionary<NavMeshPoly, NavMeshPoly> dictionary = new Dictionary<NavMeshPoly, NavMeshPoly>();
			if (num3 - num == 0 && num4 - num == 0)
			{
				if (num >= 0 && (float)num < _count.x && num2 >= 0 && (float)num4 < _count.z)
				{
					int num5 = (int)((float)num2 * _count.x + (float)num);
					if (_buckets[num5] != null)
					{
						for (int i = 0; i < _buckets[num5].Count; i++)
						{
							dictionary[_buckets[num5][i]] = _buckets[num5][i];
						}
					}
				}
			}
			else
			{
				for (float num6 = num2; num6 < (float)num4; num6 += 1f)
				{
					for (float num7 = num; num7 < (float)num3; num7 += 1f)
					{
						int num8 = (int)(num6 * _count.x + num7);
						if (_buckets[num8] != null)
						{
							for (int j = 0; j < _buckets[num8].Count; j++)
							{
								dictionary[_buckets[num8][j]] = _buckets[num8][j];
							}
						}
					}
				}
			}
			return new List<NavMeshPoly>(dictionary.Keys);
		}
	}
}
