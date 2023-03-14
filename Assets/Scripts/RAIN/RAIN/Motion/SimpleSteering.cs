using RAIN.Core;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Motion
{
	public static class SimpleSteering
	{
		public static Vector3 ComputeVelocity(Vector3 aFromPosition, Vector3 aToPosition, float aMaxSpeed, float aCloseEnoughDistance, float aDeltaTime)
		{
			Vector3 vector = aToPosition - aFromPosition;
			float magnitude = vector.magnitude;
			if (magnitude < aCloseEnoughDistance)
			{
				return Vector3.zero;
			}
			if (aDeltaTime > 0f && magnitude <= aMaxSpeed * aDeltaTime)
			{
				return vector / aDeltaTime;
			}
			return vector.normalized * aMaxSpeed;
		}

		public static Vector3 Face(Vector3 aFromPosition, Vector3 aToPosition, Vector3 aFromDirection, float aMaxRotation, float aDeltaTime)
		{
			Vector3 lookAtAngles = MathUtils.GetLookAtAngles(aFromPosition, aToPosition, aFromDirection);
			return Align(aFromDirection, lookAtAngles, aMaxRotation, aDeltaTime);
		}

		public static Vector3 Align(Vector3 aFromDirection, Vector3 aToDirection, float aMaxRotation, float aDeltaTime)
		{
			Vector3 v = aToDirection - aFromDirection;
			v = MathUtils.WrapAngles(v);
			Vector3 result = default(Vector3);
			float num = aMaxRotation * aDeltaTime;
			float num2 = Mathf.Abs(v.x);
			if (aDeltaTime > 0f && num2 <= num)
			{
				result.x = v.x / aDeltaTime;
			}
			else
			{
				result.x = aMaxRotation * Mathf.Sign(v.x);
			}
			num2 = Mathf.Abs(v.y);
			if (aDeltaTime > 0f && num2 <= num)
			{
				result.y = v.y / aDeltaTime;
			}
			else
			{
				result.y = aMaxRotation * Mathf.Sign(v.y);
			}
			num2 = Mathf.Abs(v.z);
			if (aDeltaTime > 0f && num2 <= aMaxRotation)
			{
				result.z = v.z / aDeltaTime;
			}
			else
			{
				result.z = aMaxRotation * Mathf.Sign(v.z);
			}
			return result;
		}

		public static bool DoFaceTarget(AI ai, Kinematic aFaceTarget, float aCloseEnoughAngle, bool aAllow3DRotation, ref bool aPriorSucceeded)
		{
			return DoFaceTarget(ai, aFaceTarget.Position, aCloseEnoughAngle, aAllow3DRotation, ref aPriorSucceeded);
		}

		public static bool DoFaceTarget(AI ai, Vector3 aPosition, float aCloseEnoughAngle, bool aAllow3DRotation, ref bool aPriorSucceeded)
		{
			Vector3 vector = aPosition;
			if (!aAllow3DRotation)
			{
				vector.y = ai.Kinematic.Position.y;
			}
			float num = 0f;
			Vector3 vector2 = vector - ai.Kinematic.Position;
			num = Mathf.Abs(MathUtils.WrapAngle(Vector3.Angle(ai.Kinematic.Forward, vector2.normalized)));
			if (aPriorSucceeded && num <= aCloseEnoughAngle)
			{
				ai.Kinematic.Rotation = Vector3.zero;
				return true;
			}
			Vector3 rotation = Face(ai.Kinematic.Position, vector, ai.Kinematic.Orientation, ai.Motor.RotationSpeed, ai.DeltaTime);
			if (rotation.sqrMagnitude > 0f && num < aCloseEnoughAngle)
			{
				if ((rotation.y > 0f && ai.Kinematic.PriorRotation.y <= 0f) || (rotation.y < 0f && ai.Kinematic.PriorRotation.y >= 0f))
				{
					rotation.y = 0f;
				}
				if ((rotation.x > 0f && ai.Kinematic.PriorRotation.x <= 0f) || (rotation.x < 0f && ai.Kinematic.PriorRotation.x >= 0f))
				{
					rotation.x = 0f;
				}
				if ((rotation.z > 0f && ai.Kinematic.PriorRotation.z <= 0f) || (rotation.z < 0f && ai.Kinematic.PriorRotation.z >= 0f))
				{
					rotation.z = 0f;
				}
			}
			if (!aAllow3DRotation)
			{
				rotation.x = 0f;
				rotation.z = 0f;
			}
			ai.Kinematic.Rotation = rotation;
			float num2 = ai.Motor.RotationSpeed * ai.DeltaTime;
			if (Mathf.Abs(rotation.x) <= num2 && Mathf.Abs(rotation.y) <= num2 && Mathf.Abs(rotation.z) <= num2)
			{
				aPriorSucceeded = true;
			}
			else
			{
				aPriorSucceeded = false;
			}
			if (rotation.sqrMagnitude > 0f)
			{
				return false;
			}
			return true;
		}

		public static bool DoDirectMovement(AI ai, Kinematic aMoveToTarget, float aCloseEnoughDistance, float aCloseEnoughAngle, float aFaceBeforeMoveAngle, bool aAllow3DMovement, bool aAllow3DRotation)
		{
			return DoDirectMovement(ai, aMoveToTarget.Position, aCloseEnoughDistance, aCloseEnoughAngle, aFaceBeforeMoveAngle, aAllow3DMovement, aAllow3DRotation);
		}

		public static bool DoDirectMovement(AI ai, Vector3 aPosition, float aCloseEnoughDistance, float aCloseEnoughAngle, float aFaceBeforeMoveAngle, bool aAllow3DMovement, bool aAllow3DRotation)
		{
			bool result = true;
			Vector3 vector = aPosition;
			if (!aAllow3DRotation)
			{
				vector.y = ai.Kinematic.Position.y;
			}
			float num = 0f;
			float num2 = 0f;
			Vector3 vector2 = vector - ai.Kinematic.Position;
			num2 = vector2.magnitude;
			num = Mathf.Abs(MathUtils.WrapAngle(Vector3.Angle(ai.Kinematic.Forward, vector2.normalized)));
			vector = aPosition;
			if (!aAllow3DMovement)
			{
				vector.y = ai.Kinematic.Position.y;
			}
			Vector3 zero = Vector3.zero;
			if (num2 > aCloseEnoughDistance)
			{
				result = false;
				if (num <= aFaceBeforeMoveAngle)
				{
					zero = ComputeVelocity(ai.Kinematic.Position, vector, ai.Motor.Speed, aCloseEnoughDistance, ai.DeltaTime);
					ai.Kinematic.Velocity = zero;
				}
			}
			if (!Mathf.Approximately(num2, 0f))
			{
				Vector3 zero2 = Vector3.zero;
				zero2 = Face(ai.Kinematic.Position, vector, ai.Kinematic.Orientation, ai.Motor.RotationSpeed, ai.DeltaTime);
				if (zero2.sqrMagnitude > 0f && num < aCloseEnoughAngle)
				{
					if ((zero2.y > 0f && ai.Kinematic.PriorRotation.y <= 0f) || (zero2.y < 0f && ai.Kinematic.PriorRotation.y >= 0f))
					{
						zero2.y = 0f;
					}
					if ((zero2.x > 0f && ai.Kinematic.PriorRotation.x <= 0f) || (zero2.x < 0f && ai.Kinematic.PriorRotation.x >= 0f))
					{
						zero2.x = 0f;
					}
					if ((zero2.z > 0f && ai.Kinematic.PriorRotation.z <= 0f) || (zero2.z < 0f && ai.Kinematic.PriorRotation.z >= 0f))
					{
						zero2.z = 0f;
					}
				}
				if (!aAllow3DRotation)
				{
					zero2.x = 0f;
					zero2.z = 0f;
				}
				ai.Kinematic.Rotation = zero2;
			}
			return result;
		}
	}
}
