using System;

[NodeToolTip("Returns the system's current time.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Game_Time")]
[FriendlyName("Get System Time", "Returns the system's current time.")]
[NodePath("Actions/Time")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_GetSystemTime : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Time", "Outputs the current time as hh:mm:ss.")] out string FullTime, [FriendlyName("UTC Time", "Outputs the current time in UTC format.")][SocketState(false, false)] out string FullTimeUTC, [FriendlyName("Hour", "Outputs the hour value.")][SocketState(false, false)] out int Hour, [FriendlyName("Minute", "Outputs the minute value.")][SocketState(false, false)] out int Minute, [SocketState(false, false)][FriendlyName("Second", "Outputs the second value.")] out int Second, [SocketState(false, false)][FriendlyName("Millisecond", "Outputs the millisecond value.")] out int Millisecond)
	{
		FullTime = DateTime.Now.ToString("hh:mm:ss");
		FullTimeUTC = DateTime.UtcNow.ToString("hh:mm:ss");
		Hour = DateTime.Now.Hour;
		Minute = DateTime.Now.Minute;
		Second = DateTime.Now.Second;
		Millisecond = DateTime.Now.Millisecond;
	}
}
