using UnityEngine;

namespace RAIN.Utility
{
	public class BezierCurve
	{
		public static Vector3 PointOnCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			Vector3 vector = default(Vector3);
			float num = (1f - t) * (1f - t) * (1f - t);
			float num2 = 3f * (1f - t) * (1f - t) * t;
			float num3 = 3f * (1f - t) * t * t;
			float num4 = t * t * t;
			return p0 * num + p1 * num2 + p2 * num3 + p3 * num4;
		}

		public static Vector2 PointOn2DCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
		{
			Vector2 vector = default(Vector2);
			float num = (1f - t) * (1f - t) * (1f - t);
			float num2 = 3f * (1f - t) * (1f - t) * t;
			float num3 = 3f * (1f - t) * t * t;
			float num4 = t * t * t;
			return p0 * num + p1 * num2 + p2 * num3 + p3 * num4;
		}

		public static float GetY(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float x, float error)
		{
			if (error <= 0f || error >= 0f)
			{
				error = 0.001f;
			}
			float num = x - error;
			float num2 = x + error;
			float num3 = 0.25f;
			float num4 = 0.5f;
			int num5 = 20;
			Vector2 vector;
			do
			{
				vector = PointOn2DCurve(p0, p1, p2, p3, num4);
				if (vector.x < num)
				{
					num4 += num3;
					num3 /= 2f;
					continue;
				}
				if (vector.x > num2)
				{
					num4 -= num3;
					num3 /= 2f;
					continue;
				}
				return vector.y;
			}
			while (num5-- != 0);
			return vector.y;
		}
	}
}
