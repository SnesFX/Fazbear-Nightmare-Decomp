using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Motion
{
	[RAINSerializableClass]
	public class CharacterControllerMotor : BasicMotor
	{
		private CharacterController _characterController;

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

		public override void BodyInit()
		{
			base.BodyInit();
			if (AI.Body == null)
			{
				UnityCharacterController = null;
				return;
			}
			UnityCharacterController = AI.Body.GetComponent<CharacterController>();
			if (UnityCharacterController == null)
			{
				Debug.LogWarning("CharacterControllerMotor: No Character Controller present on AI Body (adding a temporary one)", AI.Body);
				UnityCharacterController = AI.Body.AddComponent<CharacterController>();
			}
		}

		public override void ApplyMotionTransforms()
		{
			AI.Kinematic.UpdateTransformData(AI.DeltaTime);
			UnityCharacterController.SimpleMove(AI.Kinematic.Velocity);
			AI.Body.transform.rotation = Quaternion.Euler(AI.Kinematic.Orientation);
		}
	}
}
