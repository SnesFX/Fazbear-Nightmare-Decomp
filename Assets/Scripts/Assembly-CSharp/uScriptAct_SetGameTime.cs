using UnityEngine;

[NodeToolTip("Sets the Time Manager values of the game.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Set_Game_Time")]
[FriendlyName("Set Game Time", "Sets the Time Manager values of the game.")]
[NodePath("Actions/Time")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_SetGameTime : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([DefaultValue(1f)][FriendlyName("Time Scale", "The speed at which time progress. Change this value to simulate bullet-time effects. A value of 1 means real-time. A value of 0.5 means half speed; a value of 2 is double speed.")] float TimeScale, [DefaultValue(0.02f)][FriendlyName("Fixed Timestep", "A framerate-independent interval that dictates when physics calculations and FixedUpdate() events are performed.")] float FixedTimestep, [DefaultValue(0.3333333f)][FriendlyName("Max Allowed Timestep", "A framerate-independent interval that caps the worst case scenario when frame-rate is low. Physics calculations and FixedUpdate() events will not be performed for longer time than specified.")] float MaxAllowedTimestep)
	{
		if (TimeScale < 0f)
		{
			TimeScale = 0f;
		}
		Time.timeScale = TimeScale;
		if (FixedTimestep < 0.0001f)
		{
			FixedTimestep = 0.0001f;
		}
		Time.fixedDeltaTime = FixedTimestep;
		if (MaxAllowedTimestep < 0.001f)
		{
			MaxAllowedTimestep = 0.001f;
		}
		Time.maximumDeltaTime = MaxAllowedTimestep;
	}
}
