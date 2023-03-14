using System;
using RAIN.Action;
using RAIN.Animation;
using RAIN.Core;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Navigation.Targets;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class MecanimIKAction : RAINAction
	{
		public const string ACTIONNAME = "mecik";

		public Expression ikPositionTargetExpression = new Expression();

		public Expression ikRotationTargetExpression = new Expression();

		public MoveLookTarget ikPositionTarget = new MoveLookTarget();

		public MoveLookTarget ikRotationTarget = new MoveLookTarget();

		public MecanimAnimator.SupportedMecanimIKType ikType = MecanimAnimator.SupportedMecanimIKType.LookAt;

		public Expression ikPositionWeightExpression;

		public Expression ikRotationWeightExpression;

		public Expression ikLookAtWeightExpression;

		public Expression ikLookAtBodyWeightExpression;

		public Expression ikLookAtHeadWeightExpression;

		public Expression ikLookAtEyesWeightExpression;

		public Expression ikLookAtClampWeightExpression;

		public Expression ikMaxTurnRateExpression;

		protected MecanimAnimator mecanimAnimator;

		protected MecanimAnimator.IKLookAt ikLookAt = new MecanimAnimator.IKLookAt();

		protected MecanimAnimator.IKTarget ikTarget = new MecanimAnimator.IKTarget();

		public override void Start(AI ai)
		{
			base.Start(ai);
			mecanimAnimator = ai.Animator as MecanimAnimator;
			if (mecanimAnimator == null)
			{
				Debug.LogWarning("MecanimIKAction: No MecanimAnimator present in the AI", ai.Body);
			}
			ikPositionTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
			ikRotationTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
			if (ikPositionTargetExpression.IsValid)
			{
				if (ikPositionTargetExpression.IsVariable)
				{
					ikPositionTarget.SetVariableTarget(ikPositionTargetExpression.VariableName, ai.WorkingMemory);
				}
				else
				{
					object obj = ikPositionTargetExpression.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
					if (obj is Vector3)
					{
						ikPositionTarget.VectorTarget = (Vector3)obj;
					}
					else if (obj is string)
					{
						string text = (string)obj;
						NavigationTarget navigationTarget = NavigationManager.Instance.GetNavigationTarget(text);
						if (navigationTarget == null)
						{
							throw new Exception("'" + text + "' does not match the name of any navigation target");
						}
						ikPositionTarget.NavigationTarget = navigationTarget;
					}
					else
					{
						ikPositionTarget.NavigationTarget = null;
					}
				}
			}
			if (!ikRotationTargetExpression.IsValid)
			{
				return;
			}
			if (ikRotationTargetExpression.IsVariable)
			{
				ikRotationTarget.SetVariableTarget(ikRotationTargetExpression.VariableName, ai.WorkingMemory);
				return;
			}
			object obj2 = ikRotationTargetExpression.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
			if (obj2 is Vector3)
			{
				ikRotationTarget.VectorTarget = (Vector3)obj2;
			}
			else if (obj2 is string)
			{
				string text2 = (string)obj2;
				NavigationTarget navigationTarget2 = NavigationManager.Instance.GetNavigationTarget(text2);
				if (navigationTarget2 == null)
				{
					throw new Exception("'" + text2 + "' does not match the name of any navigation target");
				}
				ikRotationTarget.NavigationTarget = navigationTarget2;
			}
			else
			{
				ikRotationTarget.NavigationTarget = null;
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (mecanimAnimator == null)
			{
				return ActionResult.RUNNING;
			}
			if (ikType == MecanimAnimator.SupportedMecanimIKType.LookAt)
			{
				ikLookAt.isActive = false;
				if (ikPositionTarget.IsValid)
				{
					ikLookAt.isActive = true;
					ikLookAt.positionVector = ikPositionTarget.Position;
					if (ikLookAtWeightExpression != null && ikLookAtWeightExpression.IsValid)
					{
						ikLookAt.lookAtWeight = Mathf.Clamp01(ikLookAtWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
					}
					else
					{
						ikLookAt.lookAtWeight = 0f;
					}
					if (ikLookAtBodyWeightExpression != null && ikLookAtBodyWeightExpression.IsValid)
					{
						ikLookAt.lookAtBodyWeight = Mathf.Clamp01(ikLookAtBodyWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
					}
					else
					{
						ikLookAt.lookAtBodyWeight = 0f;
					}
					if (ikLookAtHeadWeightExpression != null && ikLookAtHeadWeightExpression.IsValid)
					{
						ikLookAt.lookAtHeadWeight = Mathf.Clamp01(ikLookAtHeadWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
					}
					else
					{
						ikLookAt.lookAtHeadWeight = 1f;
					}
					if (ikLookAtEyesWeightExpression != null && ikLookAtEyesWeightExpression.IsValid)
					{
						ikLookAt.lookAtEyesWeight = Mathf.Clamp01(ikLookAtEyesWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
					}
					else
					{
						ikLookAt.lookAtEyesWeight = 0f;
					}
					if (ikLookAtClampWeightExpression != null && ikLookAtClampWeightExpression.IsValid)
					{
						ikLookAt.lookAtClampWeight = Mathf.Clamp01(ikLookAtClampWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
					}
					else
					{
						ikLookAt.lookAtClampWeight = 0.5f;
					}
					if (ikMaxTurnRateExpression != null && ikMaxTurnRateExpression.IsValid)
					{
						ikLookAt.maxTurnRate = ikMaxTurnRateExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
					}
					else
					{
						ikLookAt.maxTurnRate = 0f;
					}
				}
				mecanimAnimator.SetState("IKLookAt", ikLookAt);
			}
			else
			{
				ikTarget.isActive = true;
				if (ikPositionTarget.IsValid && ikPositionWeightExpression != null)
				{
					ikTarget.positionVector = ikPositionTarget.Position;
					ikTarget.positionWeight = Mathf.Clamp01(ikPositionWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
				}
				else
				{
					ikTarget.positionWeight = 0f;
				}
				if (ikRotationTarget.IsValid && ikRotationWeightExpression != null)
				{
					ikTarget.rotationVector = ikRotationTarget.Orientation;
					ikTarget.rotationWeight = Mathf.Clamp01(ikRotationWeightExpression.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
				}
				else
				{
					ikTarget.rotationWeight = 0f;
				}
				mecanimAnimator.SetState(ikType, ikTarget);
			}
			return ActionResult.RUNNING;
		}

		public override void Stop(AI ai)
		{
			ikLookAt.isActive = false;
			ikTarget.isActive = false;
			if (ikType == MecanimAnimator.SupportedMecanimIKType.LookAt)
			{
				mecanimAnimator.SetState(ikType, ikLookAt);
			}
			else
			{
				mecanimAnimator.SetState(ikType, ikTarget);
			}
			base.Stop(ai);
		}
	}
}
