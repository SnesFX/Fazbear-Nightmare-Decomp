using UnityEngine;

[FriendlyName("Get Game Time", "Gets the current time scale and delta time (fixed timestep) of the game.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Game_Time")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Time")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Gets the current time scale and delta time (fixed timestep) of the game.")]
public class uScriptAct_GetGameTime : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Time Scale", "The current global time scale.")] out float CurrentScale, [FriendlyName("Fixed Timestep", "The current global fixed timestep.")] out float FixedDelta, [FriendlyName("Max Allowed Timestep", "The current global allowed timestep.")] out float MaxAllowedTimestep)
	{
		CurrentScale = Time.timeScale;
		FixedDelta = Time.fixedDeltaTime;
		MaxAllowedTimestep = Time.maximumDeltaTime;
	}
}
