using RAIN.Core;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Motion
{
	[RAINSerializableClass]
	public abstract class RAINMotor : RAINAIElement
	{
		private MoveLookTarget _moveTarget = new MoveLookTarget();

		private MoveLookTarget _faceTarget = new MoveLookTarget();

		public virtual float DefaultSpeed { get; set; }

		public virtual float DefaultRotationSpeed { get; set; }

		public virtual float DefaultCloseEnoughDistance { get; set; }

		public virtual float DefaultCloseEnoughAngle { get; set; }

		public virtual float DefaultFaceBeforeMoveAngle { get; set; }

		public virtual float DefaultStepUpHeight { get; set; }

		public virtual float Speed { get; set; }

		public virtual float RotationSpeed { get; set; }

		public virtual float CloseEnoughDistance { get; set; }

		public virtual float CloseEnoughAngle { get; set; }

		public virtual float FaceBeforeMoveAngle { get; set; }

		public virtual float StepUpHeight { get; set; }

		public virtual bool Allow3DMovement { get; set; }

		public virtual bool AllowOffGraphMovement { get; set; }

		public virtual bool Allow3DRotation { get; set; }

		public virtual bool IsAtMoveTarget
		{
			get
			{
				return IsAt(_moveTarget);
			}
		}

		public virtual bool IsFacingFaceTarget
		{
			get
			{
				return IsFacing(_faceTarget);
			}
		}

		public virtual MoveLookTarget MoveTarget
		{
			get
			{
				return _moveTarget;
			}
			set
			{
				_moveTarget = value;
			}
		}

		public virtual MoveLookTarget FaceTarget
		{
			get
			{
				return _faceTarget;
			}
			set
			{
				_faceTarget = value;
			}
		}

		public abstract void UpdateMotionTransforms();

		public abstract void ApplyMotionTransforms();

		public virtual void UpdateAnimation()
		{
		}

		public virtual void UpdateRootMotion()
		{
		}

		public virtual void UpdateIK(int aLayerIndex)
		{
		}

		public virtual bool MoveTo(Vector3 position)
		{
			MoveTarget.VectorTarget = position;
			return Move();
		}

		public abstract bool Move();

		public virtual bool FaceAt(Vector3 position)
		{
			_faceTarget.VectorTarget = position;
			return Face();
		}

		public abstract bool Face();

		public abstract void Stop();

		public abstract bool IsAt(MoveLookTarget aTarget);

		public abstract bool IsAt(Vector3 aPosition);

		public abstract bool IsFacing(MoveLookTarget aTarget);

		public abstract bool IsFacing(Vector3 aPosition);
	}
}
