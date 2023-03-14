using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Core
{
	[RAINSerializableClass]
	public class Kinematic
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Position")]
		private Vector3 _position;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Orientation")]
		private Vector3 _orientation;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Velocity")]
		private Vector3 _velocity;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Angular velocity per axis")]
		private Vector3 _rotation;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Acceleration")]
		private Vector3 _acceleration;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Angular acceleration per axis")]
		private Vector3 _angularAcceleration;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Velocity last frame")]
		private Vector3 _priorVelocity;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Angular velocity last frame")]
		private Vector3 _priorRotation;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Acceleration last frame")]
		private Vector3 _priorAcceleration;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Angular acceleration last frame")]
		private Vector3 _priorAngularAcceleration;

		public Vector3 Position
		{
			get
			{
				return _position;
			}
			set
			{
				_position = value;
			}
		}

		public Vector3 Orientation
		{
			get
			{
				return _orientation;
			}
			set
			{
				_orientation = MathUtils.WrapAngles(value);
			}
		}

		public Vector3 Forward
		{
			get
			{
				return Quaternion.Euler(_orientation) * Vector3.forward;
			}
		}

		public Vector3 PriorVelocity
		{
			get
			{
				return _priorVelocity;
			}
			set
			{
				_priorVelocity = value;
			}
		}

		public float PriorSpeed
		{
			get
			{
				return _priorVelocity.magnitude;
			}
		}

		public Vector3 PriorRotation
		{
			get
			{
				return _priorRotation;
			}
			set
			{
				_priorRotation = value;
			}
		}

		public Vector3 PriorAcceleration
		{
			get
			{
				return _priorAcceleration;
			}
			set
			{
				_priorAcceleration = value;
			}
		}

		public Vector3 PriorAngularAcceleration
		{
			get
			{
				return _priorAngularAcceleration;
			}
			set
			{
				_priorAngularAcceleration = value;
			}
		}

		public Vector3 Velocity
		{
			get
			{
				return _velocity;
			}
			set
			{
				_velocity = value;
			}
		}

		public float Speed
		{
			get
			{
				return _velocity.magnitude;
			}
		}

		public Vector3 Rotation
		{
			get
			{
				return _rotation;
			}
			set
			{
				_rotation = value;
			}
		}

		public Vector3 Acceleration
		{
			get
			{
				return _acceleration;
			}
			set
			{
				_acceleration = value;
			}
		}

		public Vector3 AngularAcceleration
		{
			get
			{
				return _angularAcceleration;
			}
			set
			{
				_angularAcceleration = value;
			}
		}

		public Kinematic()
		{
			Zero();
		}

		public Kinematic(Kinematic kcopy)
		{
			Copy(kcopy);
		}

		public void Copy(Kinematic kcopy)
		{
			_position = kcopy._position;
			_orientation = kcopy._orientation;
			_priorVelocity = kcopy._priorVelocity;
			_priorRotation = kcopy._priorRotation;
			_priorAcceleration = kcopy._priorAcceleration;
			_priorAngularAcceleration = kcopy._priorAngularAcceleration;
			_acceleration = kcopy._acceleration;
			_angularAcceleration = kcopy._angularAcceleration;
			_rotation = kcopy._rotation;
			_velocity = kcopy._velocity;
		}

		public void Zero()
		{
			_position = Vector3.zero;
			_orientation = Vector3.zero;
			_priorVelocity = Vector3.zero;
			_priorRotation = Vector3.zero;
			_priorAcceleration = Vector3.zero;
			_priorAngularAcceleration = Vector3.zero;
			_acceleration = Vector3.zero;
			_angularAcceleration = Vector3.zero;
			_velocity = Vector3.zero;
			_rotation = Vector3.zero;
		}

		public void UpdateTransformData(float deltaTime)
		{
			Position = Position + Velocity * deltaTime + Acceleration * (0.5f * deltaTime * deltaTime);
			Orientation = Orientation + Rotation * deltaTime + AngularAcceleration * (0.5f * deltaTime * deltaTime);
		}

		public void ResetVelocities()
		{
			_priorVelocity = _velocity;
			_priorRotation = _rotation;
			_priorAcceleration = _acceleration;
			_priorAngularAcceleration = _angularAcceleration;
			_rotation = Vector3.zero;
			_velocity = Vector3.zero;
			_acceleration = Vector3.zero;
			_angularAcceleration = Vector3.zero;
		}
	}
}
