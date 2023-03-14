using RAIN.Core;
using RAIN.Representation;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Animation
{
	[RAINSerializableClass]
	public class MecanimParameterValue
	{
		public enum SupportedMecanimType
		{
			Boolean = 0,
			Integer = 1,
			Float = 2,
			Trigger = 3
		}

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The mecanim parameter name")]
		public string parameterName;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The data type of the parameter")]
		public SupportedMecanimType parameterType = SupportedMecanimType.Float;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The expression to evaluate and assign to the parameter")]
		public Expression parameterValue = new Expression();

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The damp time for the parameter")]
		public float dampTime;

		public void SetParameter(Animator aMecanimAnimator, AI ai)
		{
			if (string.IsNullOrEmpty(parameterName) || !parameterValue.IsValid)
			{
				return;
			}
			switch (parameterType)
			{
			case SupportedMecanimType.Boolean:
				aMecanimAnimator.SetBool(parameterName, parameterValue.Evaluate<bool>(ai.DeltaTime, ai.WorkingMemory));
				break;
			case SupportedMecanimType.Float:
				aMecanimAnimator.SetFloat(parameterName, parameterValue.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory), dampTime, ai.DeltaTime);
				break;
			case SupportedMecanimType.Integer:
				aMecanimAnimator.SetInteger(parameterName, parameterValue.Evaluate<int>(ai.DeltaTime, ai.WorkingMemory));
				break;
			case SupportedMecanimType.Trigger:
				if (parameterValue.Evaluate<bool>(ai.DeltaTime, ai.WorkingMemory))
				{
					aMecanimAnimator.SetTrigger(parameterName);
				}
				else
				{
					aMecanimAnimator.ResetTrigger(parameterName);
				}
				break;
			}
		}
	}
}
