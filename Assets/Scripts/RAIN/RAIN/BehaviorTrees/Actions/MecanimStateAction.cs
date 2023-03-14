using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class MecanimStateAction : RAINAction
	{
		public const string ACTIONNAME = "mecstate";

		public Expression stateExpression = new Expression();

		public Expression layerExpression = new Expression();

		private Animator mecanimAnimator;

		public override void Start(AI ai)
		{
			mecanimAnimator = ai.Body.GetComponentInChildren<Animator>();
		}

		public override ActionResult Execute(AI ai)
		{
			if (mecanimAnimator == null)
			{
				return ActionResult.FAILURE;
			}
			if (stateExpression == null || !stateExpression.IsValid)
			{
				return ActionResult.FAILURE;
			}
			if (layerExpression == null || !layerExpression.IsValid)
			{
				return ActionResult.FAILURE;
			}
			string name = stateExpression.Evaluate<string>(ai.DeltaTime, ai.WorkingMemory);
			int layerIndex = layerExpression.Evaluate<int>(ai.DeltaTime, ai.WorkingMemory);
			int num = Animator.StringToHash(name);
			AnimatorStateInfo currentAnimatorStateInfo = mecanimAnimator.GetCurrentAnimatorStateInfo(layerIndex);
			if (currentAnimatorStateInfo.nameHash == num || currentAnimatorStateInfo.tagHash == num)
			{
				return ActionResult.SUCCESS;
			}
			return ActionResult.FAILURE;
		}
	}
}
