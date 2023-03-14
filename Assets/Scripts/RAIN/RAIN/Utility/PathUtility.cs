using UnityEngine;

namespace RAIN.Utility
{
	public static class PathUtility
	{
		public static bool CalculateTurnParameters(Vector3 aPivotPoint, Vector3 aForwardDirection, Vector3 aDestination, float aTurnRadius, out Vector3 aTurnCenter, out Vector3 aExitPoint, out float aTurnAngle, out float aActualTurnRadius)
		{
			bool result = true;
			Vector3 vector = new Vector3(aPivotPoint.x, 0f, aPivotPoint.z);
			Vector3 vector2 = new Vector3(aDestination.x, 0f, aDestination.z);
			Vector3 normalized = new Vector3(aForwardDirection.x, 0f, aForwardDirection.z).normalized;
			Vector3 aToTarget = vector2 - vector;
			aTurnCenter = vector;
			aExitPoint = vector;
			aTurnAngle = 0f;
			aActualTurnRadius = aTurnRadius;
			if (aActualTurnRadius > aToTarget.magnitude)
			{
				aActualTurnRadius = aToTarget.magnitude;
				result = false;
			}
			if (aActualTurnRadius <= 0f)
			{
				aActualTurnRadius = 0f;
				aTurnCenter.y = aPivotPoint.y;
				aExitPoint.y = aPivotPoint.y;
				return false;
			}
			float num = MathUtils.DirectionToTarget(normalized, aToTarget.normalized, Vector3.up);
			if (num < 0f)
			{
				Vector3 vector3 = new Vector3(0f - aForwardDirection.z, 0f, aForwardDirection.x);
				aTurnCenter = vector + aActualTurnRadius * vector3.normalized;
			}
			else
			{
				Vector3 vector4 = new Vector3(aForwardDirection.z, 0f, 0f - aForwardDirection.x);
				aTurnCenter = vector + aActualTurnRadius * vector4.normalized;
			}
			float magnitude = (vector2 - aTurnCenter).magnitude;
			if (magnitude < aActualTurnRadius)
			{
				aActualTurnRadius = 0f;
				aTurnCenter.y = aPivotPoint.y;
				aExitPoint.y = aPivotPoint.y;
				return false;
			}
			Mathf.Sqrt(magnitude * magnitude - aActualTurnRadius * aActualTurnRadius);
			float num2 = Mathf.Acos(aActualTurnRadius / magnitude) * 57.29578f;
			float y = 0f - num2;
			if (num < 0f)
			{
				y = num2;
			}
			Vector3 vector5 = Quaternion.Euler(new Vector3(0f, y, 0f)) * (vector2 - aTurnCenter).normalized;
			aExitPoint = aTurnCenter + vector5 * aActualTurnRadius;
			Vector3 aToTarget2 = aExitPoint - vector;
			Vector3 aForward = aTurnCenter - vector;
			float num3 = aToTarget2.magnitude / 2f;
			aTurnAngle = Mathf.Asin(num3 / aActualTurnRadius) * 57.29578f * 2f;
			float num4 = MathUtils.DirectionToTarget(aForward, aToTarget2, Vector3.up);
			float num5 = MathUtils.DirectionToTarget(aForward, aToTarget, Vector3.up);
			if (num == num4 && num == num5)
			{
				aTurnAngle = 360f - aTurnAngle;
			}
			aTurnCenter.y = aPivotPoint.y;
			aExitPoint.y = aPivotPoint.y;
			return result;
		}
	}
}
