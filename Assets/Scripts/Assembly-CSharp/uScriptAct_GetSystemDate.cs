using System;

[NodePath("Actions/Time")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Get System Date", "Returns the system's current date information.")]
[NodeToolTip("Returns the system's current date information.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Get_Game_Time")]
public class uScriptAct_GetSystemDate : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Date", "Outputs the current date.")] out string FullDate, [FriendlyName("Day", "Outputs the current day of the week.")][SocketState(false, false)] out string Day, [FriendlyName("Day of Month", "Outputs the current day value.")][SocketState(false, false)] out int DayOfMonth, [FriendlyName("Month", "Outputs the current month value.")][SocketState(false, false)] out int Month, [SocketState(false, false)][FriendlyName("Year", "Outputs the current year value.")] out int Year)
	{
		FullDate = DateTime.Today.ToString("d");
		Day = DateTime.Today.DayOfWeek.ToString();
		DayOfMonth = DateTime.Today.Day;
		Month = DateTime.Today.Month;
		Year = DateTime.Today.Year;
	}
}
