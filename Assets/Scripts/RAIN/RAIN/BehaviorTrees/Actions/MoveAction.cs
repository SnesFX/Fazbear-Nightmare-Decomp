using System;
using RAIN.Action;
using RAIN.Core;
using RAIN.Motion;
using RAIN.Navigation;
using RAIN.Navigation.Targets;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class MoveAction : RAINAction
	{
		public const string ACTIONNAME = "move";

		public Expression moveTargetExpression = new Expression();

		public Expression faceTargetExpression = new Expression();

		public MoveLookTarget moveTarget = new MoveLookTarget();

		public MoveLookTarget faceTarget = new MoveLookTarget();

		public Expression moveSpeed;

		public Expression turnSpeed;

		public Expression closeEnoughDistance;

		public Expression closeEnoughAngle;

		public override void Start(AI ai)
		{
			base.Start(ai);
			moveTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
			moveTarget.CloseEnoughDistance = 0f;
			faceTarget.TargetType = MoveLookTarget.MoveLookTargetType.None;
			faceTarget.CloseEnoughDistance = 0f;
			if (moveTargetExpression.IsValid)
			{
				if (moveTargetExpression.IsVariable)
				{
					moveTarget.SetVariableTarget(moveTargetExpression.VariableName, ai.WorkingMemory);
				}
				else
				{
					object obj = moveTargetExpression.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
					if (obj is Vector3)
					{
						moveTarget.VectorTarget = (Vector3)obj;
					}
					else if (obj is string)
					{
						string text = (string)obj;
						NavigationTarget navigationTarget = NavigationManager.Instance.GetNavigationTarget(text);
						if (navigationTarget == null)
						{
							throw new Exception("'" + text + "' does not match the name of any navigation target");
						}
						moveTarget.NavigationTarget = navigationTarget;
					}
					else if (obj != null)
					{
						moveTarget.ObjectTarget = obj;
					}
					else
					{
						moveTarget.NavigationTarget = null;
					}
				}
			}
			if (!faceTargetExpression.IsValid)
			{
				return;
			}
			if (faceTargetExpression.IsVariable)
			{
				faceTarget.SetVariableTarget(faceTargetExpression.VariableName, ai.WorkingMemory);
				return;
			}
			object obj2 = faceTargetExpression.Evaluate<object>(ai.DeltaTime, ai.WorkingMemory);
			if (obj2 is Vector3)
			{
				faceTarget.VectorTarget = (Vector3)obj2;
			}
			else if (obj2 is string)
			{
				string text2 = (string)obj2;
				NavigationTarget navigationTarget2 = NavigationManager.Instance.GetNavigationTarget(text2);
				if (navigationTarget2 == null)
				{
					throw new Exception("'" + text2 + "' does not match the name of any navigation target");
				}
				faceTarget.NavigationTarget = navigationTarget2;
			}
			else if (obj2 != null)
			{
				faceTarget.ObjectTarget = obj2;
			}
			else
			{
				faceTarget.NavigationTarget = null;
			}
		}

		public override ActionResult Execute(AI ai)
		{
			bool flag = true;
			bool flag2 = false;
			if (turnSpeed.IsValid)
			{
				ai.Motor.RotationSpeed = turnSpeed.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
			}
			else
			{
				ai.Motor.RotationSpeed = ai.Motor.DefaultRotationSpeed;
			}
			if (moveSpeed.IsValid)
			{
				ai.Motor.Speed = moveSpeed.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
			}
			else
			{
				ai.Motor.Speed = ai.Motor.DefaultSpeed;
			}
			if (closeEnoughDistance.IsValid)
			{
				moveTarget.CloseEnoughDistance = Mathf.Max(moveTarget.CloseEnoughDistance, closeEnoughDistance.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory));
			}
			else
			{
				moveTarget.CloseEnoughDistance = Mathf.Max(moveTarget.CloseEnoughDistance, ai.Motor.DefaultCloseEnoughDistance);
			}
			float num = ai.Motor.CloseEnoughAngle;
			if (closeEnoughAngle.IsValid)
			{
				ai.Motor.CloseEnoughAngle = closeEnoughAngle.Evaluate<float>(ai.DeltaTime, ai.WorkingMemory);
			}
			else
			{
				ai.Motor.CloseEnoughAngle = ai.Motor.DefaultCloseEnoughAngle;
			}
			if (moveTarget != null && moveTarget.IsValid)
			{
				ai.Motor.MoveTarget = moveTarget;
				flag = ai.Motor.Move();
				if (faceTarget != null && faceTarget.IsValid)
				{
					ai.Motor.FaceTarget = faceTarget;
					ai.Motor.Face();
				}
				if (flag && !ai.Motor.AllowOffGraphMovement && !ai.Motor.IsAt(moveTarget))
				{
					flag2 = true;
				}
			}
			else if (faceTarget != null && faceTarget.IsValid)
			{
				ai.Motor.FaceTarget = faceTarget;
				flag = ai.Motor.Face();
			}
			ai.Motor.CloseEnoughAngle = num;
			if (flag)
			{
				ai.Navigator.CurrentPath = null;
				if (flag2)
				{
					return ActionResult.FAILURE;
				}
				return ActionResult.SUCCESS;
			}
			return ActionResult.RUNNING;
		}
	}
}
