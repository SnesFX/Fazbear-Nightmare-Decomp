using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Motion
{
	[RAINSerializableClass]
	public class BasicMotor : RAINMotor
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default speed of movement", OldFieldNames = new string[] { "defaultSpeed" })]
		private float _speed = 1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default speed of rotation deg/sec", OldFieldNames = new string[] { "defaultRotationSpeed" })]
		private float _rotationSpeed = 180f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default stopping distance", OldFieldNames = new string[] { "defaultCloseEnoughDistance" })]
		private float _closeEnoughDistance = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default stopping angle", OldFieldNames = new string[] { "defaultCloseEnoughAngle" })]
		private float _closeEnoughAngle = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default facing distance before movement", OldFieldNames = new string[] { "defaultFaceBeforeMoveAngle" })]
		private float _faceBeforeMoveAngle = 90f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default step up height", OldFieldNames = new string[] { "defaultStepUpHeight" })]
		private float _stepUpHeight = 0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Allow 3D movement.  Disables pathing.")]
		private bool _allow3DMovement;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Allow 3D rotation.")]
		private bool _allow3DRotation;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Allow the AI to move off a navigation graph", OldFieldNames = new string[] { "_validPathRequired" })]
		private bool _allowOffGraphMovement = true;

		private MoveLookTarget testTarget = new MoveLookTarget();

		private MoveLookTarget cachedMoveTarget = new MoveLookTarget();

		private MoveLookTarget _goodTarget = new MoveLookTarget();

		private bool priorFaceSucceeded;

		public override float DefaultSpeed
		{
			get
			{
				return _speed;
			}
			set
			{
				_speed = value;
			}
		}

		public override float DefaultRotationSpeed
		{
			get
			{
				return _rotationSpeed;
			}
			set
			{
				_rotationSpeed = value;
			}
		}

		public override float DefaultCloseEnoughDistance
		{
			get
			{
				return _closeEnoughDistance;
			}
			set
			{
				_closeEnoughDistance = value;
			}
		}

		public override float DefaultCloseEnoughAngle
		{
			get
			{
				return _closeEnoughAngle;
			}
			set
			{
				_closeEnoughAngle = value;
			}
		}

		public override float DefaultFaceBeforeMoveAngle
		{
			get
			{
				return _faceBeforeMoveAngle;
			}
			set
			{
				_faceBeforeMoveAngle = value;
			}
		}

		public override float DefaultStepUpHeight
		{
			get
			{
				return _stepUpHeight;
			}
			set
			{
				_stepUpHeight = value;
			}
		}

		public override bool Allow3DMovement
		{
			get
			{
				return _allow3DMovement;
			}
			set
			{
				_allow3DMovement = value;
			}
		}

		public override bool Allow3DRotation
		{
			get
			{
				return _allow3DRotation;
			}
			set
			{
				_allow3DRotation = value;
			}
		}

		public override bool AllowOffGraphMovement
		{
			get
			{
				return _allowOffGraphMovement;
			}
			set
			{
				_allowOffGraphMovement = value;
			}
		}

		public virtual MoveLookTarget GoodTarget
		{
			get
			{
				return _goodTarget;
			}
		}

		public override void AIInit()
		{
			base.AIInit();
			Speed = DefaultSpeed;
			RotationSpeed = DefaultRotationSpeed;
			CloseEnoughDistance = DefaultCloseEnoughDistance;
			CloseEnoughAngle = DefaultCloseEnoughAngle;
			FaceBeforeMoveAngle = DefaultFaceBeforeMoveAngle;
			StepUpHeight = DefaultStepUpHeight;
			cachedMoveTarget.CloseEnoughDistance = CloseEnoughDistance;
		}

		public override void BodyInit()
		{
			base.BodyInit();
			if (AI.Body != null)
			{
				GoodTarget.VectorTarget = AI.Body.transform.position;
				GoodTarget.CloseEnoughDistance = CloseEnoughDistance;
			}
		}

		public override void UpdateMotionTransforms()
		{
			AI.Kinematic.Position = AI.Body.transform.position;
			AI.Kinematic.Orientation = AI.Body.transform.rotation.eulerAngles;
			AI.Kinematic.ResetVelocities();
		}

		public override void ApplyMotionTransforms()
		{
			AI.Kinematic.UpdateTransformData(AI.DeltaTime);
			AI.Body.transform.position = AI.Kinematic.Position;
			AI.Body.transform.rotation = Quaternion.Euler(AI.Kinematic.Orientation);
		}

		public override bool IsAt(MoveLookTarget aTarget)
		{
			if (aTarget == null || !aTarget.IsValid)
			{
				return false;
			}
			Vector3 position = aTarget.Position;
			Vector3 vector = AI.Kinematic.Position - position;
			if (!_allow3DMovement)
			{
				vector.y = 0f;
			}
			float magnitude = vector.magnitude;
			float num = Mathf.Max(aTarget.CloseEnoughDistance, CloseEnoughDistance);
			if (magnitude <= num && ((AllowOffGraphMovement && AI.Navigator.CurrentPath == null) || AI.Navigator.IsAt(aTarget)))
			{
				AI.Kinematic.Velocity = Vector3.zero;
				AI.Kinematic.Rotation = Vector3.zero;
				return true;
			}
			return false;
		}

		public override bool IsAt(Vector3 aPosition)
		{
			testTarget.VectorTarget = aPosition;
			testTarget.CloseEnoughDistance = 0f;
			return IsAt(testTarget);
		}

		public override bool IsFacing(MoveLookTarget aTarget)
		{
			if (aTarget == null || !aTarget.IsValid)
			{
				return false;
			}
			Vector3 position = aTarget.Position;
			if (!_allow3DRotation)
			{
				position.y = AI.Kinematic.Position.y;
			}
			Vector3 vector = position - AI.Kinematic.Position;
			float num = Mathf.Abs(MathUtils.WrapAngle(Vector3.Angle(AI.Kinematic.Forward, vector.normalized)));
			if (num <= CloseEnoughAngle)
			{
				return true;
			}
			return false;
		}

		public override bool IsFacing(Vector3 aPosition)
		{
			testTarget.VectorTarget = aPosition;
			testTarget.CloseEnoughDistance = 0f;
			return IsFacing(testTarget);
		}

		public override bool Move()
		{
			AI.Navigator.pathTarget = MoveTarget;
			if (IsAt(MoveTarget))
			{
				return true;
			}
			cachedMoveTarget = AI.Navigator.GetNextPathWaypoint(Allow3DMovement, AllowOffGraphMovement, cachedMoveTarget);
			if (!AllowOffGraphMovement && !AI.Navigator.IsPathfinding)
			{
				if (AI.Navigator.CurrentPath == null || !AI.Navigator.CurrentPath.IsValid)
				{
					return true;
				}
				if (!AI.Navigator.CurrentPath.IsPartial && IsAt(AI.Navigator.CurrentPath.GetWaypointPosition(AI.Navigator.CurrentPath.WaypointCount - 1)))
				{
					return true;
				}
			}
			if (cachedMoveTarget.IsValid)
			{
				GoodTarget.VectorTarget = cachedMoveTarget.Position;
				SimpleSteering.DoDirectMovement(AI, cachedMoveTarget.Position, CloseEnoughDistance, CloseEnoughAngle, FaceBeforeMoveAngle, Allow3DMovement, Allow3DRotation);
			}
			else if (!AllowOffGraphMovement)
			{
				SimpleSteering.DoDirectMovement(AI, GoodTarget.Position, Mathf.Min(0.001f, CloseEnoughDistance), CloseEnoughAngle, FaceBeforeMoveAngle, Allow3DMovement, Allow3DRotation);
			}
			else
			{
				SimpleSteering.DoDirectMovement(AI, MoveTarget.Position, Mathf.Min(0.001f, CloseEnoughDistance), CloseEnoughAngle, FaceBeforeMoveAngle, Allow3DMovement, Allow3DRotation);
			}
			return false;
		}

		public override bool Face()
		{
			if (FaceTarget == null || !FaceTarget.IsValid)
			{
				return false;
			}
			return SimpleSteering.DoFaceTarget(AI, FaceTarget.Position, CloseEnoughAngle, Allow3DRotation, ref priorFaceSucceeded);
		}

		public override void Stop()
		{
			AI.Kinematic.Velocity = Vector3.zero;
			AI.Kinematic.Rotation = Vector3.zero;
		}
	}
}
