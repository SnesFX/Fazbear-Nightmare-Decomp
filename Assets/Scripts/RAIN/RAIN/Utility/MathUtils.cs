using System;
using RAIN.Core;
using UnityEngine;

namespace RAIN.Utility
{
	public static class MathUtils
	{
		public const float TWOPI = (float)Math.PI * 2f;

		public const float MINUSTWOPI = (float)Math.PI * -2f;

		public const float CIRCLE = 360f;

		public const float MINUSCIRCLE = -360f;

		public const float HALFCIRCLE = 180f;

		public const float MINUSHALFCIRCLE = -180f;

		public static float RotateTrigAngle(float aStartAngle, float aRotationDegrees)
		{
			float num = 90f - aStartAngle;
			float num2 = 0f - aRotationDegrees;
			return WrapAngle(num + num2);
		}

		public static Vector3 NegateAngles(Vector3 v1)
		{
			return new Vector3(v1.x - 180f, v1.y - 180f, v1.z - 180f);
		}

		public static float WrapAngle(float angle)
		{
			while (angle <= -180f)
			{
				angle += 360f;
			}
			while (angle >= 180f)
			{
				angle += -360f;
			}
			return angle;
		}

		public static Vector3 WrapAngles(Vector3 v1)
		{
			return new Vector3(WrapAngle(v1.x), WrapAngle(v1.y), WrapAngle(v1.z));
		}

		public static Vector3 GetAngles(Vector3 v1)
		{
			return new Vector3((float)(0.0 - Math.Atan2(v1.y, v1.z)) * 57.29578f, (float)(0.0 - Math.Atan2(0f - v1.x, v1.z)) * 57.29578f, (float)Math.Atan2(v1.y, v1.x) * 57.29578f);
		}

		public static Vector3 NegateVector(Vector3 v1)
		{
			return new Vector3(0f - v1.x, 0f - v1.y, 0f - v1.z);
		}

		public static Vector4 NegateAngleAxis(Vector4 v1)
		{
			return new Vector4(v1.x, v1.y, v1.z, WrapAngle(v1.w - 180f));
		}

		public static int RandomInt(int aMinValue, int aMaxValue)
		{
			return UnityEngine.Random.Range(aMinValue, aMaxValue);
		}

		public static float RandomFloat(float aMinValue, float aMaxValue)
		{
			return UnityEngine.Random.Range(aMinValue, aMaxValue);
		}

		public static float RandomFloat()
		{
			return UnityEngine.Random.value;
		}

		[Obsolete("Use GetLookAtAngles instead")]
		public static Vector3 GetLookAtVector(Vector3 aFromPosition, Vector3 aToPosition, Vector3 aDefaultVector)
		{
			return GetLookAtAngles(aFromPosition, aToPosition, aDefaultVector);
		}

		public static Vector3 GetLookAtAngles(Vector3 aFromPosition, Vector3 aToPosition, Vector3 aDefaultVector)
		{
			Vector3 vector = aToPosition - aFromPosition;
			if (Mathf.Approximately(vector.sqrMagnitude, 0f))
			{
				return aDefaultVector;
			}
			return WrapAngles(Quaternion.LookRotation(vector.normalized).eulerAngles);
		}

		[Obsolete("Use GetLookAtAngles instead")]
		public static Vector3 GetLookAtVector(Kinematic aFromKinematic, Kinematic aToKinematic)
		{
			return GetLookAtAngles(aFromKinematic, aToKinematic);
		}

		public static Vector3 GetLookAtAngles(Kinematic aFromKinematic, Kinematic aToKinematic)
		{
			Vector3 vector = aFromKinematic.Position - aToKinematic.Position;
			if (Mathf.Approximately(vector.sqrMagnitude, 0f))
			{
				return aFromKinematic.Orientation;
			}
			return WrapAngles(Quaternion.LookRotation(vector.normalized).eulerAngles);
		}

		public static Vector3 FindNearestPointOnLine(Vector3 pt, Vector3 start, Vector3 end)
		{
			Vector3 vector = end - start;
			Vector3 rhs = pt - start;
			float sqrMagnitude = vector.sqrMagnitude;
			if (Mathf.Approximately(sqrMagnitude, 0f))
			{
				return end;
			}
			float num = Vector3.Dot(vector, rhs) / sqrMagnitude;
			Vector3 vector2 = default(Vector3);
			if (num < 0f)
			{
				return start;
			}
			if (num > 1f)
			{
				return end;
			}
			return start + vector * num;
		}

		public static Vector3 FindNearestPointOnLineXZ(Vector3 pt, Vector3 start, Vector3 end)
		{
			pt.y = 0f;
			start.y = 0f;
			end.y = 0f;
			return FindNearestPointOnLine(pt, start, end);
		}

		public static float AngleDir(Vector3 fwd, Vector3 target, Vector3 up)
		{
			Vector3 lhs = Vector3.Cross(fwd, target);
			return Vector3.Dot(lhs, up);
		}

		public static Vector3 CalculateTriNormal(Vector3 v0, Vector3 v1, Vector3 v2)
		{
			Vector3 lhs = v1 - v0;
			Vector3 rhs = v2 - v0;
			Vector3 result = Vector3.Cross(lhs, rhs);
			result.Normalize();
			return result;
		}

		public static bool OverlapBounds(Vector3 amin, Vector3 amax, Vector3 bmin, Vector3 bmax)
		{
			bool flag = true;
			flag = !(amin.x > bmax.x) && !(amax.x < bmin.x) && flag;
			flag = !(amin.y > bmax.y) && !(amax.y < bmin.y) && flag;
			return !(amin.z > bmax.z) && !(amax.z < bmin.z) && flag;
		}

		public static float Clamp(float value, float min, float max)
		{
			return Mathf.Clamp(value, min, max);
		}

		public static int Clamp(int value, int min, int max)
		{
			return Mathf.Clamp(value, min, max);
		}

		public static float QuickDot(float x1, float y1, float x2, float y2)
		{
			return x1 * x2 + y1 * y2;
		}

		public static float DirectionToTarget(Vector3 aForward, Vector3 aToTarget, Vector3 aUp)
		{
			Vector3 lhs = Vector3.Cross(aForward, aToTarget);
			float num = Vector3.Dot(lhs, aUp);
			if (num > 0f)
			{
				return 1f;
			}
			if (num < 0f)
			{
				return -1f;
			}
			return 0f;
		}

		public static float RandomFromNormalDistribution(float min, float max)
		{
			float num = (max + min) / 2f;
			float num2 = (max - num) / 3f;
			if (min >= max)
			{
				return UnityEngine.Random.Range(max, min);
			}
			float num3;
			do
			{
				num3 = NormalRandom() * num2 + num;
			}
			while (num3 < min || num3 > max);
			return num3;
		}

		public static float NormalRandom()
		{
			float num;
			float num3;
			do
			{
				num = 2f * UnityEngine.Random.value - 1f;
				float num2 = 2f * UnityEngine.Random.value - 1f;
				num3 = num * num + num2 * num2;
			}
			while (num3 > 1f || num3 == 0f);
			return num * Mathf.Sqrt(-2f * Mathf.Log(num3) / num3);
		}

		public static float ArraySlope(float[] aValues)
		{
			if (aValues == null || aValues.Length == 0)
			{
				return 0f;
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			int num5 = aValues.Length;
			for (int i = 0; i < num5; i++)
			{
				num += (float)i;
				num2 += aValues[i];
			}
			for (int j = 0; j < num5; j++)
			{
				float num6 = (float)j - num / (float)num5;
				num3 += num6 * num6;
				num4 += num6 * aValues[j];
			}
			return num4 / num3;
		}

		public static void FitLineToPoints(Vector2[] points, out float slope, out float yintercept)
		{
			slope = 0f;
			yintercept = 0f;
			if (points != null && points.Length != 0)
			{
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				int num5 = points.Length;
				for (int i = 0; i < num5; i++)
				{
					num += points[i].x;
					num2 += points[i].y;
				}
				for (int j = 0; j < num5; j++)
				{
					float num6 = points[j].x - num / (float)num5;
					num3 += num6 * num6;
					num4 += num6 * points[j].y;
				}
				if (Mathf.Approximately(num3, 0f))
				{
					slope = float.NaN;
					yintercept = float.NaN;
				}
				else
				{
					slope = num4 / num3;
					yintercept = (num2 - num * slope) / (float)num5;
				}
			}
		}

		public static Vector3 CastToCollider(Vector3 aFromPos, Vector3 aForward, float aDefaultDistance, float aMaxDistance)
		{
			RaycastHit[] array = Physics.RaycastAll(aFromPos, aForward, aMaxDistance);
			for (int i = 0; i < array.Length; i++)
			{
				if (!array[i].collider.isTrigger)
				{
					return array[i].point;
				}
			}
			return aFromPos + aForward.normalized * aDefaultDistance;
		}

		public static bool IsLineLeftXZ(Vector3 line, Vector3 line2)
		{
			return line.x * line2.z - line.z * line2.x > 0f;
		}

		public static bool IsLineColinearXZ(Vector3 line, Vector3 line2)
		{
			return line.x * line2.z - line.z * line2.x == 0f;
		}

		public static float DistanceXZ(Vector3 aFromPos, Vector3 aToPos)
		{
			return Mathf.Sqrt((aFromPos.x - aToPos.x) * (aFromPos.x - aToPos.x) + (aFromPos.z - aToPos.z) * (aFromPos.z - aToPos.z));
		}

		public static float SqrDistanceXZ(Vector3 aFromPos, Vector3 aToPos)
		{
			return (aFromPos.x - aToPos.x) * (aFromPos.x - aToPos.x) + (aFromPos.z - aToPos.z) * (aFromPos.z - aToPos.z);
		}

		public static bool IsOnXZ(Vector3 aFrom, Vector3 aTo, Vector3 aPoint)
		{
			return Area2D(aFrom, aTo, aPoint) == 0f;
		}

		public static bool IsLeftXZ(Vector3 aFrom, Vector3 aTo, Vector3 aPoint)
		{
			return Area2D(aFrom, aTo, aPoint) > 0f;
		}

		public static bool IsLeftOrOnXZ(Vector3 aFrom, Vector3 aTo, Vector3 aPoint)
		{
			return Area2D(aFrom, aTo, aPoint) >= 0f;
		}

		public static bool IsRightXZ(Vector3 aFrom, Vector3 aTo, Vector3 aPoint)
		{
			return Area2D(aFrom, aTo, aPoint) < 0f;
		}

		public static bool IsRightOrOnXZ(Vector3 aFrom, Vector3 aTo, Vector3 aPoint)
		{
			return Area2D(aFrom, aTo, aPoint) <= 0f;
		}

		public static float Area2D(Vector3 aPointA, Vector3 aPointB, Vector3 aPointC)
		{
			return (aPointB.x - aPointA.x) * (aPointC.z - aPointA.z) - (aPointC.x - aPointA.x) * (aPointB.z - aPointA.z);
		}

		public static int CantorPairing(int aOne, int aTwo)
		{
			return ((aOne + aTwo) * (aOne + aTwo + 1) >> 1) + aTwo;
		}

		public static int CommutativeCantorPairing(int aOne, int aTwo)
		{
			return ((aOne + aTwo) * (aOne + aTwo + 1) >> 1) + Math.Min(aOne, aTwo);
		}
	}
}
