using RAIN.Action;
using RAIN.Core;
using UnityEngine;

namespace RAIN.BehaviorTrees.Actions
{
	public class AudioAction : RAINAction
	{
		public const string ACTIONNAME = "audio";

		public string audioSourceName;

		public AudioSource audioSource;

		public bool waitUntilDone;

		public bool forceAudioStopOnExit;

		public float delay;

		protected bool started;

		public override void Start(AI ai)
		{
			started = false;
			if (audioSourceName == null)
			{
				return;
			}
			audioSourceName = audioSourceName.Trim();
			if (audioSourceName.Length <= 0 || (!(audioSource == null) && !(audioSource.name != audioSourceName)))
			{
				return;
			}
			audioSource = null;
			AudioSource[] componentsInChildren = ai.Body.gameObject.GetComponentsInChildren<AudioSource>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].name == audioSourceName)
				{
					audioSource = componentsInChildren[i];
					break;
				}
			}
		}

		public override ActionResult Execute(AI ai)
		{
			if (audioSource == null)
			{
				return ActionResult.FAILURE;
			}
			if (audioSource.clip == null)
			{
				return ActionResult.SUCCESS;
			}
			if (!started && audioSource.enabled && !audioSource.isPlaying)
			{
				ulong num = (ulong)((float)audioSource.clip.frequency * Mathf.Max(delay, 0f));
				audioSource.Play(num);
				started = true;
				if (waitUntilDone)
				{
					return ActionResult.RUNNING;
				}
			}
			started = true;
			if (audioSource.loop)
			{
				return ActionResult.RUNNING;
			}
			if (waitUntilDone && audioSource.isPlaying)
			{
				return ActionResult.RUNNING;
			}
			return ActionResult.SUCCESS;
		}

		public override void Stop(AI ai)
		{
			if (started && audioSource != null && (audioSource.loop || forceAudioStopOnExit) && audioSource.isPlaying)
			{
				audioSource.Stop();
			}
			started = false;
		}
	}
}
