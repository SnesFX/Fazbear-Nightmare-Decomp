using System.Collections.Generic;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Motion
{
	[RAINSerializableClass]
	public class MecanimMotor : RAINMotor
	{
		public enum MotorField
		{
			Speed = 0,
			RotationSpeed = 1,
			TurnAngle = 2,
			TurnBeforeMoveAngle = 3,
			VelocityX = 4,
			VelocityY = 5,
			VelocityZ = 6,
			RotationX = 7,
			RotationY = 8,
			RotationZ = 9,
			Count = 10
		}

		[RAINSerializableClass]
		public class MotorParameter
		{
			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The mecanim parameter name")]
			public string parameterName;

			[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "The mecanim parameter name")]
			public MotorField motorField;

			[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The damp time for the parameter")]
			public float dampTime;
		}

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default speed of movement", OldFieldNames = new string[] { "defaultSpeed" })]
		private float _speed = 1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default speed of rotation deg/sec", OldFieldNames = new string[] { "defaultRotationSpeed" })]
		private float _rotationSpeed = 180f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default stopping distance", OldFieldNames = new string[] { "defaultCloseEnoughDistance" })]
		private float _closeEnoughDistance = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default stopping angle", OldFieldNames = new string[] { "defaultCloseEnoughAngle" })]
		private float _closeEnoughAngle = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default facing angle before movement", OldFieldNames = new string[] { "defaultFaceBeforeMoveAngle" })]
		private float _faceBeforeMoveAngle = 90f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Default step up height", OldFieldNames = new string[] { "defaultStepUpHeight" })]
		private float _stepUpHeight = 0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "3D AI Movement")]
		private bool _allow3DMovement;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "3D AI Rotation")]
		private bool _allow3DRotation;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Allow the AI to move off a navigation graph", OldFieldNames = new string[] { "_validPathRequired" })]
		private bool _allowOffGraphMovement = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Toggle root motion")]
		private bool _useRootMotion = true;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Allow RAIN to override root motion rotation", OldFieldNames = new string[] { "overrideRootMotionRotation" })]
		private bool _overrideRootRotation;

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private List<MotorParameter> _forwardedParameters = new List<MotorParameter>();

		private CharacterController _characterController;

		private Animator _mecanimAnimator;

		private float _lastTurnAngle;

		private MoveLookTarget _testTarget = new MoveLookTarget();

		private MoveLookTarget _cachedMoveTarget = new MoveLookTarget();

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

		public virtual bool UseRootMotion
		{
			get
			{
				return _useRootMotion;
			}
			set
			{
				_useRootMotion = value;
			}
		}

		public virtual bool OverrideRootMotionRotation
		{
			get
			{
				return _overrideRootRotation;
			}
			set
			{
				_overrideRootRotation = value;
			}
		}

		public IList<MotorParameter> ForwardedParameters
		{
			get
			{
				return _forwardedParameters.AsReadOnly();
			}
		}

		public virtual Animator UnityAnimator
		{
			get
			{
				return _mecanimAnimator;
			}
			set
			{
				_mecanimAnimator = value;
			}
		}

		public virtual CharacterController UnityCharacterController
		{
			get
			{
				return _characterController;
			}
			set
			{
				_characterController = value;
			}
		}

		public virtual MoveLookTarget GoodTarget
		{
			get
			{
				return _goodTarget;
			}
		}

		public virtual float TurnAngle
		{
			get
			{
				return _lastTurnAngle;
			}
			set
			{
				_lastTurnAngle = value;
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
			_cachedMoveTarget.CloseEnoughDistance = CloseEnoughDistance;
		}

		public override void BodyInit()
		{
			base.BodyInit();
			if (AI.Body == null)
			{
				UnityAnimator = null;
				UnityCharacterController = null;
				return;
			}
			UnityAnimator = AI.Body.GetComponentInChildren<Animator>();
			if (UnityAnimator == null)
			{
				Debug.LogWarning("MecanimMotor: No Animator present on AI Body (adding a temporary one)", AI.Body);
				UnityAnimator = AI.Body.AddComponent<Animator>();
			}
			UnityCharacterController = AI.Body.GetComponent<CharacterController>();
			GoodTarget.VectorTarget = AI.Body.transform.position;
			GoodTarget.CloseEnoughDistance = CloseEnoughDistance;
		}

		public override void UpdateMotionTransforms()
		{
			AI.Kinematic.Position = AI.Body.transform.position;
			AI.Kinematic.Orientation = AI.Body.transform.rotation.eulerAngles;
			TurnAngle = 0f;
			AI.Kinematic.ResetVelocities();
		}

		public override void ApplyMotionTransforms()
		{
			AI.Kinematic.UpdateTransformData(AI.DeltaTime);
			UpdateMecanimParameters();
			if (!UseRootMotion)
			{
				if (UnityCharacterController == null)
				{
					AI.Body.transform.position = AI.Kinematic.Position;
					AI.Body.transform.rotation = Quaternion.Euler(AI.Kinematic.Orientation);
				}
				else
				{
					UnityCharacterController.SimpleMove(AI.Kinematic.Velocity);
					AI.Body.transform.rotation = Quaternion.Euler(AI.Kinematic.Orientation);
				}
			}
			else if (OverrideRootMotionRotation)
			{
				AI.Body.transform.rotation = Quaternion.Euler(AI.Kinematic.Orientation);
			}
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
			_testTarget.VectorTarget = aPosition;
			_testTarget.CloseEnoughDistance = CloseEnoughDistance;
			return IsAt(_testTarget);
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
			_testTarget.VectorTarget = aPosition;
			_testTarget.CloseEnoughDistance = CloseEnoughDistance;
			return IsFacing(_testTarget);
		}

		public override bool Move()
		{
			AI.Navigator.pathTarget = MoveTarget;
			if (IsAt(MoveTarget))
			{
				return true;
			}
			TurnAngle = 0f;
			_cachedMoveTarget = AI.Navigator.GetNextPathWaypoint(Allow3DMovement, AllowOffGraphMovement, _cachedMoveTarget);
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
			if (_cachedMoveTarget.IsValid)
			{
				GoodTarget.VectorTarget = _cachedMoveTarget.Position;
				TurnAngle = MathUtils.WrapAngle(MathUtils.GetLookAtAngles(AI.Kinematic.Position, _cachedMoveTarget.Position, AI.Kinematic.Orientation).y - AI.Kinematic.Orientation.y);
				SimpleSteering.DoDirectMovement(AI, _cachedMoveTarget.Position, CloseEnoughDistance, CloseEnoughAngle, FaceBeforeMoveAngle, Allow3DMovement, Allow3DRotation);
			}
			else if (!AllowOffGraphMovement)
			{
				TurnAngle = MathUtils.WrapAngle(MathUtils.GetLookAtAngles(AI.Kinematic.Position, GoodTarget.Position, AI.Kinematic.Orientation).y - AI.Kinematic.Orientation.y);
				SimpleSteering.DoDirectMovement(AI, GoodTarget.Position, 0f, CloseEnoughAngle, FaceBeforeMoveAngle, Allow3DMovement, Allow3DRotation);
			}
			else
			{
				TurnAngle = MathUtils.WrapAngle(MathUtils.GetLookAtAngles(AI.Kinematic.Position, MoveTarget.Position, AI.Kinematic.Orientation).y - AI.Kinematic.Orientation.y);
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
			TurnAngle = MathUtils.WrapAngle(MathUtils.GetLookAtAngles(AI.Kinematic.Position, FaceTarget.Position, AI.Kinematic.Orientation).y - AI.Kinematic.Orientation.y);
			return SimpleSteering.DoFaceTarget(AI, FaceTarget.Position, CloseEnoughAngle, Allow3DRotation, ref priorFaceSucceeded);
		}

		public override void Stop()
		{
			AI.Kinematic.Velocity = Vector3.zero;
			AI.Kinematic.Rotation = Vector3.zero;
		}

		public void AddForwardedParameter(MotorParameter aParameter)
		{
			_forwardedParameters.Add(aParameter);
		}

		public void RemoveForwardedParameter(MotorParameter aParameter)
		{
			_forwardedParameters.Remove(aParameter);
		}

		public void RemoveForwardedParameter(string aParameterName)
		{
			for (int num = _forwardedParameters.Count - 1; num >= 0; num--)
			{
				MotorParameter motorParameter = _forwardedParameters[num];
				if (motorParameter == null || motorParameter.parameterName == aParameterName)
				{
					_forwardedParameters.RemoveAt(num);
				}
			}
		}

		public void ClearForwardedParameters()
		{
			_forwardedParameters.Clear();
		}

		public virtual void UpdateMecanimParameters()
		{
			Vector3 vector = Quaternion.Inverse(Quaternion.Euler(AI.Kinematic.Orientation)) * AI.Kinematic.Velocity;
			for (int i = 0; i < _forwardedParameters.Count; i++)
			{
				switch (_forwardedParameters[i].motorField)
				{
				case MotorField.Speed:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, vector.magnitude, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.RotationSpeed:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, AI.Kinematic.Rotation.y, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.TurnAngle:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, TurnAngle, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.TurnBeforeMoveAngle:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, Mathf.Max(0f, TurnAngle - FaceBeforeMoveAngle), _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.VelocityX:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, vector.x, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.VelocityY:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, vector.y, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.VelocityZ:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, vector.z, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.RotationX:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, AI.Kinematic.Rotation.x, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.RotationY:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, AI.Kinematic.Rotation.y, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				case MotorField.RotationZ:
					UnityAnimator.SetFloat(_forwardedParameters[i].parameterName, AI.Kinematic.Rotation.z, _forwardedParameters[i].dampTime, AI.DeltaTime);
					break;
				}
			}
		}
	}
}
