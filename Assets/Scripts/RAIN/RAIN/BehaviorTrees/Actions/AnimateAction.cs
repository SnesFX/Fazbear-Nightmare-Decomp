using RAIN.Action;
using RAIN.Animation;
using RAIN.Core;

namespace RAIN.BehaviorTrees.Actions
{
	public class AnimateAction : RAINAction
	{
		public const string ACTIONNAME = "animate";

		public string animationState;

		private bool _animationStarted;

		private bool _pauseForTransitions = true;

		public override void Start(AI ai)
		{
			base.Start(ai);
			_animationStarted = false;
		}

		public override ActionResult Execute(AI ai)
		{
			if (ai.Animator == null)
			{
				return ActionResult.FAILURE;
			}
			if (string.IsNullOrEmpty(animationState))
			{
				return ActionResult.FAILURE;
			}
			if (!_animationStarted && ai.Animator is MecanimAnimator && ((MecanimAnimator)ai.Animator).IsInTransition(animationState))
			{
				if (_pauseForTransitions)
				{
					return ActionResult.RUNNING;
				}
				return ActionResult.FAILURE;
			}
			if (!_animationStarted && ai.Animator.StartState(animationState))
			{
				_animationStarted = true;
				return ActionResult.RUNNING;
			}
			if (_animationStarted)
			{
				if (ai.Animator.IsStatePlaying(animationState))
				{
					return ActionResult.RUNNING;
				}
				ai.Animator.StopState(animationState);
				_animationStarted = false;
				return ActionResult.SUCCESS;
			}
			return ActionResult.FAILURE;
		}

		public override void Stop(AI ai)
		{
			if (_animationStarted)
			{
				ai.Animator.StopState(animationState);
				_animationStarted = false;
			}
			base.Stop(ai);
		}
	}
}
