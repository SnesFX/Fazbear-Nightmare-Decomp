using UnityEngine;

namespace RAIN.Utility
{
	public class SplineCurve
	{
		public static Vector3 PointOnCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			Vector3 result = default(Vector3);
			float num = ((0f - t + 2f) * t - 1f) * t * 0.5f;
			float num2 = ((3f * t - 5f) * t * t + 2f) * 0.5f;
			float num3 = ((-3f * t + 4f) * t + 1f) * t * 0.5f;
			float num4 = (t - 1f) * t * t * 0.5f;
			result.x = p0.x * num + p1.x * num2 + p2.x * num3 + p3.x * num4;
			result.y = p0.y * num + p1.y * num2 + p2.y * num3 + p3.y * num4;
			result.z = p0.z * num + p1.z * num2 + p2.z * num3 + p3.z * num4;
			return result;
		}
	}
}
