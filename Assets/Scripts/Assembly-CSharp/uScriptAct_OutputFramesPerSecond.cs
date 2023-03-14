using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Output FPS", "Outputs the current frames per second.")]
[NodeToolTip("Outputs the current frames per second.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Utilities")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_OutputFramesPerSecond : uScriptLogic
{
	private float m_FPS;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("FPS", "Returns the current frames per second.")] out float FPS)
	{
		FPS = m_FPS;
	}

	public void Update()
	{
		m_FPS = 1f / Time.deltaTime / Time.timeScale;
	}
}
