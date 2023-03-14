using UnityEngine;

[NodeToolTip("Pauses the game and spits out the game time and an optional text string to Unity's console.")]
[FriendlyName("Break", "Pauses the game and spits out the game time and an optional text string to Unity's console. Restart the game by pressing the Play button in the Unity editor.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodePath("Actions/Editor Only")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_Break : uScriptLogic
{
	private bool m_DelayedOut;

	[FriendlyName("Out")]
	public bool AfterDelay
	{
		get
		{
			return m_DelayedOut;
		}
	}

	public void In([FriendlyName("Data", "Optional output for the Unity console when the break is triggered. Good for passing a vairable value or string at the time of the break.")] object LogOuput, [FriendlyName("Break Time", "The time when the break was triggered (Time.time).")] out float breakTime)
	{
		m_DelayedOut = false;
		float time = Time.time;
		Debug.Log(string.Concat("uScript BREAK (Time: ", time.ToString(), ") (Data: ", LogOuput, ")"));
		breakTime = time;
		Debug.Break();
		m_DelayedOut = true;
	}
}
