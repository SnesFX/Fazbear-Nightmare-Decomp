using RAIN.Action;
using RAIN.Animation;
using RAIN.Core;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class MecanimParameterAction : RAINAction
	{
		public const string ACTIONNAME = "mecparam";

		private MecanimParameterValue parameterValue = new MecanimParameterValue();

		private Animator mecanimAnimator;

		public string ParameterName
		{
			get
			{
				return parameterValue.parameterName;
			}
			set
			{
				parameterValue.parameterName = value;
			}
		}

		public Expression ValueExpression
		{
			get
			{
				return parameterValue.parameterValue;
			}
			set
			{
				parameterValue.parameterValue = value;
			}
		}

		public float DampTime
		{
			get
			{
				return parameterValue.dampTime;
			}
			set
			{
				parameterValue.dampTime = value;
			}
		}

		public MecanimParameterValue.SupportedMecanimType ParameterType
		{
			get
			{
				return parameterValue.parameterType;
			}
			set
			{
				parameterValue.parameterType = value;
			}
		}

		public override void Start(AI ai)
		{
			if (ai.Animator is MecanimAnimator)
			{
				mecanimAnimator = ((MecanimAnimator)ai.Animator).UnityAnimator;
			}
			if (mecanimAnimator == null)
			{
				mecanimAnimator = ai.Body.GetComponentInChildren<Animator>();
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (mecanimAnimator == null)
			{
				return ActionResult.FAILURE;
			}
			parameterValue.SetParameter(mecanimAnimator, ai);
			return ActionResult.SUCCESS;
		}
	}
}
